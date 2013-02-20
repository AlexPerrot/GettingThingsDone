CREATE PROCEDURE [Projects_Tasks_insert]
	@P_1 Int,
	@P_2 Int,
	@P_3 Int,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; IF NOT EXISTS (SELECT * FROM [Projects_Tasks_tracking] WHERE [Id] = @P_1) BEGIN INSERT INTO [Projects_Tasks]([Id], [Project_id], [Task_id]) VALUES (@P_1, @P_2, @P_3);  SET @sync_row_count = @@rowcount;  END 
END