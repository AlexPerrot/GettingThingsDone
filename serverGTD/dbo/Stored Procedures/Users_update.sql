CREATE PROCEDURE [Users_update]
	@P_1 Int,
	@P_2 NVarChar(50),
	@P_3 NVarChar(50),
	@P_4 NVarChar(50),
	@sync_force_write Int,
	@sync_min_timestamp BigInt,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; UPDATE [Users] SET [Username] = @P_2, [Password] = @P_3, [Mail] = @P_4 FROM [Users] [base] JOIN [Users_tracking] [side] ON [base].[Id] = [side].[Id] WHERE ([side].[local_update_peer_timestamp] <= @sync_min_timestamp OR @sync_force_write = 1) AND ([base].[Id] = @P_1); SET @sync_row_count = @@ROWCOUNT;
END