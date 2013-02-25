CREATE PROCEDURE [Lists_Tasks_insert]
	@P_1 Int,
	@P_2 Int,
	@P_3 Int,
	@P_4 Int,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; IF NOT EXISTS (SELECT * FROM [Lists_Tasks_tracking] WHERE [Id] = @P_1) BEGIN INSERT INTO [Lists_Tasks]([Id], [List_id], [Task_id], [Owner]) VALUES (@P_1, @P_2, @P_3, @P_4);  SET @sync_row_count = @@rowcount;  END 
END