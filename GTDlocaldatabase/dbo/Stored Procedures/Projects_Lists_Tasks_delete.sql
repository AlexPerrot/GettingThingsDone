﻿CREATE PROCEDURE [Projects_Lists_Tasks_delete]
	@P_1 Int,
	@P_3 Int,
	@sync_force_write Int,
	@sync_min_timestamp BigInt,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; DELETE [Projects_Lists_Tasks] FROM [Projects_Lists_Tasks] [base] JOIN [Projects_Lists_Tasks_tracking] [side] ON [base].[Id] = [side].[Id] AND [base].[Owner] = [side].[Owner] WHERE ([side].[local_update_peer_timestamp] <= @sync_min_timestamp OR @sync_force_write = 1) AND ([base].[Id] = @P_1 AND [base].[Owner] = @P_3); SET @sync_row_count = @@ROWCOUNT;
END