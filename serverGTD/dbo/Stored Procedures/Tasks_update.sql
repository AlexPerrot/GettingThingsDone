﻿CREATE PROCEDURE [Tasks_update]
	@P_1 Int,
	@P_2 VarChar(50),
	@P_3 VarChar(50),
	@P_4 DateTimeOffset,
	@P_5 DateTimeOffset,
	@P_6 Int,
	@P_7 Bit,
	@sync_force_write Int,
	@sync_min_timestamp BigInt,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; UPDATE [Tasks] SET [Title] = @P_2, [Description] = @P_3, [DueDate] = @P_4, [CreationDate] = @P_5, [Done] = @P_7 FROM [Tasks] [base] JOIN [Tasks_tracking] [side] ON [base].[Id] = [side].[Id] AND [base].[Owner] = [side].[Owner] WHERE ([side].[local_update_peer_timestamp] <= @sync_min_timestamp OR @sync_force_write = 1) AND ([base].[Id] = @P_1 AND [base].[Owner] = @P_6); SET @sync_row_count = @@ROWCOUNT;
END