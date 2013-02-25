CREATE PROCEDURE [Projects_insertmetadata]
	@P_1 Int,
	@P_4 Int,
	@sync_scope_local_id Int,
	@sync_row_is_tombstone Int,
	@sync_create_peer_key Int,
	@sync_create_peer_timestamp BigInt,
	@sync_update_peer_key Int,
	@sync_update_peer_timestamp BigInt,
	@sync_check_concurrency Int,
	@sync_row_timestamp BigInt,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; UPDATE [Projects_tracking] SET [create_scope_local_id] = @sync_scope_local_id, [scope_create_peer_key] = @sync_create_peer_key, [scope_create_peer_timestamp] = @sync_create_peer_timestamp, [local_create_peer_key] = 0, [local_create_peer_timestamp] = @@DBTS+1, [update_scope_local_id] = @sync_scope_local_id, [scope_update_peer_key] = @sync_update_peer_key, [scope_update_peer_timestamp] = @sync_update_peer_timestamp, [local_update_peer_key] = 0, [restore_timestamp] = NULL, [sync_row_is_tombstone] = @sync_row_is_tombstone WHERE ([Id] = @P_1 AND [Owner] = @P_4) AND (@sync_check_concurrency = 0 or [local_update_peer_timestamp] = @sync_row_timestamp);SET @sync_row_count = @@ROWCOUNT;IF (@sync_row_count = 0) BEGIN INSERT INTO [Projects_tracking] ([Id], [Owner], [create_scope_local_id], [scope_create_peer_key], [scope_create_peer_timestamp], [local_create_peer_key], [local_create_peer_timestamp], [update_scope_local_id], [scope_update_peer_key], [scope_update_peer_timestamp], [local_update_peer_key], [restore_timestamp], [sync_row_is_tombstone], [last_change_datetime]) VALUES (@P_1, @P_4, @sync_scope_local_id, @sync_create_peer_key, @sync_create_peer_timestamp, 0, @@DBTS+1, @sync_scope_local_id, @sync_update_peer_key, @sync_update_peer_timestamp, 0, NULL, @sync_row_is_tombstone, GETDATE());SET @sync_row_count = @@ROWCOUNT; END;
END