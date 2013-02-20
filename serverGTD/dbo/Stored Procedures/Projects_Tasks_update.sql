CREATE PROCEDURE [Projects_Tasks_update]
	@P_1 Int,
	@P_2 Int,
	@P_3 Int,
	@P_4 Int,
	@sync_force_write Int,
	@sync_min_timestamp BigInt,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; UPDATE [Projects_Tasks] SET [Project_id] = @P_2, [Task_id] = @P_3, [Owner] = @P_4 FROM [Projects_Tasks] [base] JOIN [Projects_Tasks_tracking] [side] ON [base].[Id] = [side].[Id] WHERE ([side].[local_update_peer_timestamp] <= @sync_min_timestamp OR @sync_force_write = 1) AND ([base].[Id] = @P_1); SET @sync_row_count = @@ROWCOUNT;
END