CREATE PROCEDURE [Tasks_Lists_insert]
	@P_1 Int,
	@P_2 Int,
	@P_3 Int,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; IF NOT EXISTS (SELECT * FROM [Tasks_Lists_tracking] WHERE [Id] = @P_1) BEGIN INSERT INTO [Tasks_Lists]([Id], [Task_id], [List_id]) VALUES (@P_1, @P_2, @P_3);  SET @sync_row_count = @@rowcount;  END 
END