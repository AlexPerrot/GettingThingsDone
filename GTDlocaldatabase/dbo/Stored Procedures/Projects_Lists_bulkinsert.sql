﻿CREATE PROCEDURE [Projects_Lists_bulkinsert]
	@sync_min_timestamp BigInt,
	@sync_scope_local_id Int,
	@changeTable [Projects_Lists_BulkType] READONLY
AS
BEGIN
-- use a temp table to store the list of PKs that successfully got updated/inserted
DECLARE @changed TABLE ([Id] int, PRIMARY KEY ([Id]));

-- update/insert into the base table
MERGE [Projects_Lists] AS base USING
-- join done here against the side table to get the local timestamp for concurrency check
(SELECT p.*, t.local_update_peer_timestamp FROM @changeTable p LEFT JOIN [Projects_Lists_tracking] t ON p.[Id] = t.[Id]) AS changes ON changes.[Id] = base.[Id]
WHEN NOT MATCHED BY TARGET AND changes.local_update_peer_timestamp <= @sync_min_timestamp OR changes.local_update_peer_timestamp IS NULL THEN
INSERT ([Id], [Project_id], [List_id], [Owner]) VALUES (changes.[Id], changes.[Project_id], changes.[List_id], changes.[Owner])
OUTPUT INSERTED.[Id] INTO @changed; -- populates the temp table with successful PKs

UPDATE side SET
update_scope_local_id = @sync_scope_local_id, 
scope_update_peer_key = changes.sync_update_peer_key, 
scope_update_peer_timestamp = changes.sync_update_peer_timestamp,
local_update_peer_key = 0,
create_scope_local_id = @sync_scope_local_id,
scope_create_peer_key = changes.sync_create_peer_key,
scope_create_peer_timestamp = changes.sync_create_peer_timestamp,
local_create_peer_key = 0
FROM 
[Projects_Lists_tracking] side JOIN 
(SELECT p.[Id], p.sync_update_peer_timestamp, p.sync_update_peer_key, p.sync_create_peer_key, p.sync_create_peer_timestamp FROM @changed t JOIN @changeTable p ON p.[Id] = t.[Id]) AS changes ON changes.[Id] = side.[Id]
SELECT [Id] FROM @changeTable t WHERE NOT EXISTS (SELECT [Id] from @changed i WHERE t.[Id] = i.[Id])
END