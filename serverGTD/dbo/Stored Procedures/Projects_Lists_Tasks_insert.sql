CREATE PROCEDURE [Projects_Lists_Tasks_insert]
	@P_1 Int,
	@P_2 Int,
	@P_3 Int,
	@P_4 Int,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; IF NOT EXISTS (SELECT * FROM [Projects_Lists_Tasks_tracking] WHERE [Id] = @P_1 AND [Owner] = @P_3) BEGIN SET IDENTITY_INSERT [Projects_Lists_Tasks] ON; INSERT INTO [Projects_Lists_Tasks]([Id], [Project_List_id], [Owner], [Task_id]) VALUES (@P_1, @P_2, @P_3, @P_4);  SET @sync_row_count = @@rowcount; SET IDENTITY_INSERT [Projects_Lists_Tasks] OFF; END 
END