﻿CREATE PROCEDURE [Tasks_Lists_selectchanges]
	@sync_min_timestamp BigInt,
	@sync_scope_local_id Int,
	@sync_scope_restore_count Int,
	@sync_update_peer_key Int
AS
BEGIN
SELECT [side].[Id], [base].[Task_id], [base].[List_id], [base].[Owner], [side].[sync_row_is_tombstone], [side].[local_update_peer_timestamp] as sync_row_timestamp, case when ([side].[update_scope_local_id] is null or [side].[update_scope_local_id] <> @sync_scope_local_id) then COALESCE([side].[restore_timestamp], [side].[local_update_peer_timestamp]) else [side].[scope_update_peer_timestamp] end as sync_update_peer_timestamp, case when ([side].[update_scope_local_id] is null or [side].[update_scope_local_id] <> @sync_scope_local_id) then case when ([side].[local_update_peer_key] > @sync_scope_restore_count) then @sync_scope_restore_count else [side].[local_update_peer_key] end else [side].[scope_update_peer_key] end as sync_update_peer_key, case when ([side].[create_scope_local_id] is null or [side].[create_scope_local_id] <> @sync_scope_local_id) then [side].[local_create_peer_timestamp] else [side].[scope_create_peer_timestamp] end as sync_create_peer_timestamp, case when ([side].[create_scope_local_id] is null or [side].[create_scope_local_id] <> @sync_scope_local_id) then case when ([side].[local_create_peer_key] > @sync_scope_restore_count) then @sync_scope_restore_count else [side].[local_create_peer_key] end else [side].[scope_create_peer_key] end as sync_create_peer_key FROM [Tasks_Lists] [base] RIGHT JOIN [Tasks_Lists_tracking] [side] ON [base].[Id] = [side].[Id] WHERE  ([side].[update_scope_local_id] IS NULL OR [side].[update_scope_local_id] <> @sync_scope_local_id OR ([side].[update_scope_local_id] = @sync_scope_local_id AND [side].[scope_update_peer_key] <> @sync_update_peer_key)) AND [side].[local_update_peer_timestamp] > @sync_min_timestamp
END