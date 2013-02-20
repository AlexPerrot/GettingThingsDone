﻿CREATE PROCEDURE [Lists_Tasks_update]
	@P_1 Int,
	@P_2 Int,
	@P_3 Int,
	@sync_force_write Int,
	@sync_min_timestamp BigInt,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; UPDATE [Lists_Tasks] SET [List_id] = @P_2, [Task_id] = @P_3 FROM [Lists_Tasks] [base] JOIN [Lists_Tasks_tracking] [side] ON [base].[Id] = [side].[Id] WHERE ([side].[local_update_peer_timestamp] <= @sync_min_timestamp OR @sync_force_write = 1) AND ([base].[Id] = @P_1); SET @sync_row_count = @@ROWCOUNT;
END