﻿CREATE PROCEDURE [Lists_update]
	@P_1 Int,
	@P_2 NVarChar(50),
	@P_3 NVarChar(max),
	@P_4 Int,
	@sync_force_write Int,
	@sync_min_timestamp BigInt,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; UPDATE [Lists] SET [Title] = @P_2, [Description] = @P_3 FROM [Lists] [base] JOIN [Lists_tracking] [side] ON [base].[Id] = [side].[Id] AND [base].[Owner] = [side].[Owner] WHERE ([side].[local_update_peer_timestamp] <= @sync_min_timestamp OR @sync_force_write = 1) AND ([base].[Id] = @P_1 AND [base].[Owner] = @P_4); SET @sync_row_count = @@ROWCOUNT;
END