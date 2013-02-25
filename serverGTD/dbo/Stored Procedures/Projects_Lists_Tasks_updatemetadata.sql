CREATE PROCEDURE [Projects_Lists_Tasks_updatemetadata]
	@P_1 Int,
	@P_3 Int,
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
SET @sync_row_count = 0; DECLARE @was_tombstone int; SELECT @was_tombstone = [sync_row_is_tombstone] FROM [Projects_Lists_Tasks_tracking] WHERE ([Id] = @P_1 AND [Owner] = @P_3);IF (@was_tombstone IS NOT NULL AND @was_tombstone = 1 AND @sync_row_is_tombstone = 0) BEGIN UPDATE [Projects_Lists_Tasks_tracking] SET [create_scope_local_id] = @sync_scope_local_id, [scope_create_peer_key] = @sync_create_peer_key, [scope_create_peer_timestamp] = @sync_create_peer_timestamp, [local_create_peer_key] = 0, [local_create_peer_timestamp] = @@DBTS+1, [update_scope_local_id] = @sync_scope_local_id, [scope_update_peer_key] = @sync_update_peer_key, [scope_update_peer_timestamp] = @sync_update_peer_timestamp, [local_update_peer_key] = 0, [restore_timestamp] = NULL, [sync_row_is_tombstone] = @sync_row_is_tombstone WHERE ([Id] = @P_1 AND [Owner] = @P_3) AND (@sync_check_concurrency = 0 or [local_update_peer_timestamp] = @sync_row_timestamp); END ELSE BEGIN UPDATE [Projects_Lists_Tasks_tracking] SET [update_scope_local_id] = @sync_scope_local_id, [scope_update_peer_key] = @sync_update_peer_key, [scope_update_peer_timestamp] = @sync_update_peer_timestamp, [local_update_peer_key] = 0, [restore_timestamp] = NULL, [sync_row_is_tombstone] = @sync_row_is_tombstone WHERE ([Id] = @P_1 AND [Owner] = @P_3) AND (@sync_check_concurrency = 0 or [local_update_peer_timestamp] = @sync_row_timestamp); END;SET @sync_row_count = @@ROWCOUNT;
END