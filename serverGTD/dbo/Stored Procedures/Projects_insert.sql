CREATE PROCEDURE [Projects_insert]
	@P_1 Int,
	@P_2 VarChar(50),
	@P_3 VarChar(50),
	@P_4 Int,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; IF NOT EXISTS (SELECT * FROM [Projects_tracking] WHERE [Id] = @P_1) BEGIN INSERT INTO [Projects]([Id], [Title], [Description], [Owner]) VALUES (@P_1, @P_2, @P_3, @P_4);  SET @sync_row_count = @@rowcount;  END 
END