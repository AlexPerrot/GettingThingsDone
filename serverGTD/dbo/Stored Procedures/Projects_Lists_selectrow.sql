﻿CREATE PROCEDURE [Projects_Lists_selectrow]
	@P_1 Int,
	@P_4 Int,
	@sync_scope_local_id Int,
	@sync_scope_restore_count Int
AS
BEGIN
SELECT [side].[Id], [side].[Owner], [base].[Project_id], [base].[List_id], [side].[sync_row_is_tombstone], [side].[local_update_peer_timestamp] as sync_row_timestamp, case when ([side].[update_scope_local_id] is null or [side].[update_scope_local_id] <> @sync_scope_local_id) then COALESCE([side].[restore_timestamp], [side].[local_update_peer_timestamp]) else [side].[scope_update_peer_timestamp] end as sync_update_peer_timestamp, case when ([side].[update_scope_local_id] is null or [side].[update_scope_local_id] <> @sync_scope_local_id) then case when ([side].[local_update_peer_key] > @sync_scope_restore_count) then @sync_scope_restore_count else [side].[local_update_peer_key] end else [side].[scope_update_peer_key] end as sync_update_peer_key, case when ([side].[create_scope_local_id] is null or [side].[create_scope_local_id] <> @sync_scope_local_id) then [side].[local_create_peer_timestamp] else [side].[scope_create_peer_timestamp] end as sync_create_peer_timestamp, case when ([side].[create_scope_local_id] is null or [side].[create_scope_local_id] <> @sync_scope_local_id) then case when ([side].[local_create_peer_key] > @sync_scope_restore_count) then @sync_scope_restore_count else [side].[local_create_peer_key] end else [side].[scope_create_peer_key] end as sync_create_peer_key from [Projects_Lists] [base] right join [Projects_Lists_tracking] [side] on [base].[Id] = [side].[Id] and [base].[Owner] = [side].[Owner] WHERE [side].[Id] = @P_1 AND [side].[Owner] = @P_4
END