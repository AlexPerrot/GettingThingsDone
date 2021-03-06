﻿CREATE PROCEDURE [Tasks_Tasks_deletemetadata]
	@P_1 Int,
	@P_4 Int,
	@sync_check_concurrency Int,
	@sync_row_timestamp BigInt,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; DELETE [side] FROM [Tasks_Tasks_tracking] [side] WHERE [Id] = @P_1 AND [Owner] = @P_4 AND (@sync_check_concurrency = 0 or [local_update_peer_timestamp] = @sync_row_timestamp);SET @sync_row_count = @@ROWCOUNT;
END