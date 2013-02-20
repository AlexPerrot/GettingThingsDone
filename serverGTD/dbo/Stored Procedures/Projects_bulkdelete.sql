CREATE PROCEDURE [Projects_bulkdelete]
	@sync_min_timestamp BigInt,
	@sync_scope_local_id Int,
	@changeTable [Projects_BulkType] READONLY
AS
BEGIN
-- use a temp table to store the list of PKs that successfully got updated/inserted
declare @changed TABLE ([Id] int, PRIMARY KEY ([Id]));
DELETE [Projects] 
OUTPUT DELETED.[Id] INTO @changed FROM [Projects] base JOIN
(SELECT p.*, t.update_scope_local_id, t.scope_update_peer_key, t.local_update_peer_timestamp FROM @changeTable p JOIN [Projects_tracking] t ON p.[Id] = t.[Id]) as changes ON changes.[Id] = base.[Id] WHERE (changes.update_scope_local_id = @sync_scope_local_id AND changes.scope_update_peer_key = changes.sync_update_peer_key) OR changes.local_update_peer_timestamp <= @sync_min_timestamp
UPDATE side SET
sync_row_is_tombstone = 1, 
update_scope_local_id = @sync_scope_local_id, 
scope_update_peer_key = changes.sync_update_peer_key, 
scope_update_peer_timestamp = changes.sync_update_peer_timestamp,
local_update_peer_key = 0
FROM 
[Projects_tracking] side JOIN 
(SELECT p.[Id], p.sync_update_peer_timestamp, p.sync_update_peer_key, p.sync_create_peer_key, p.sync_create_peer_timestamp FROM @changed t JOIN @changeTable p ON p.[Id] = t.[Id]) AS changes ON changes.[Id] = side.[Id]
SELECT [Id] FROM @changeTable t WHERE NOT EXISTS (SELECT [Id] from @changed i WHERE t.[Id] = i.[Id])
END