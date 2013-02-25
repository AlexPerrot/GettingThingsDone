CREATE PROCEDURE [Tasks_Tasks_update]
	@P_1 Int,
	@P_2 Int,
	@P_3 Int,
	@P_4 Int,
	@P_5 Int,
	@sync_force_write Int,
	@sync_min_timestamp BigInt,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; UPDATE [Tasks_Tasks] SET [Predecessor_Id] = @P_2, [Successor_Id] = @P_3, [Predecessor_Owner] = @P_4, [Successor_Owner] = @P_5 FROM [Tasks_Tasks] [base] JOIN [Tasks_Tasks_tracking] [side] ON [base].[Id] = [side].[Id] WHERE ([side].[local_update_peer_timestamp] <= @sync_min_timestamp OR @sync_force_write = 1) AND ([base].[Id] = @P_1); SET @sync_row_count = @@ROWCOUNT;
END