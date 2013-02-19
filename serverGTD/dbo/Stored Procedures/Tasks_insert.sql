CREATE PROCEDURE [Tasks_insert]
	@P_1 Int,
	@P_2 VarChar(50),
	@P_3 VarChar(50),
	@P_4 DateTimeOffset,
	@P_5 DateTimeOffset,
	@P_6 Int,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; IF NOT EXISTS (SELECT * FROM [Tasks_tracking] WHERE [Id] = @P_1) BEGIN INSERT INTO [Tasks]([Id], [Title], [Description], [DueDate], [CreationDate], [Owner]) VALUES (@P_1, @P_2, @P_3, @P_4, @P_5, @P_6);  SET @sync_row_count = @@rowcount;  END 
END