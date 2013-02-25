CREATE PROCEDURE [Projects_bulkupdate]
	@sync_min_timestamp BigInt,
	@sync_scope_local_id Int,
	@changeTable [Projects_BulkType] READONLY
AS
BEGIN
-- use a temp table to store the list of PKs that successfully got updated
declare @changed TABLE ([Id] int, [Owner] int, PRIMARY KEY ([Id], [Owner]));

SET IDENTITY_INSERT [Projects] ON;
-- update the base table
MERGE [Projects] AS base USING
-- join done here against the side table to get the local timestamp for concurrency check
(SELECT p.*, t.update_scope_local_id, t.scope_update_peer_key, t.local_update_peer_timestamp FROM @changeTable p LEFT JOIN [Projects_tracking] t ON p.[Id] = t.[Id] AND p.[Owner] = t.[Owner]) as changes ON changes.[Id] = base.[Id] AND changes.[Owner] = base.[Owner]
WHEN MATCHED AND (changes.update_scope_local_id = @sync_scope_local_id AND changes.scope_update_peer_key = changes.sync_update_peer_key) OR changes.local_update_peer_timestamp <= @sync_min_timestamp THEN
UPDATE SET [Title] = changes.[Title], [Description] = changes.[Description]
OUTPUT INSERTED.[Id], INSERTED.[Owner] into @changed; -- populates the temp table with successful PKs

SET IDENTITY_INSERT [Projects] OFF;
UPDATE side SET
update_scope_local_id = @sync_scope_local_id, 
scope_update_peer_key = changes.sync_update_peer_key, 
scope_update_peer_timestamp = changes.sync_update_peer_timestamp,
local_update_peer_key = 0
FROM 
[Projects_tracking] side JOIN 
(SELECT p.[Id], p.[Owner], p.sync_update_peer_timestamp, p.sync_update_peer_key, p.sync_create_peer_key, p.sync_create_peer_timestamp FROM @changed t JOIN @changeTable p ON p.[Id] = t.[Id] AND p.[Owner] = t.[Owner]) as changes ON changes.[Id] = side.[Id] AND changes.[Owner] = side.[Owner]
SELECT [Id], [Owner] FROM @changeTable t WHERE NOT EXISTS (SELECT [Id], [Owner] from @changed i WHERE t.[Id] = i.[Id] AND t.[Owner] = i.[Owner])
END