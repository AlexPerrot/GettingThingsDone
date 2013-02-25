CREATE PROCEDURE [Users_insert]
	@P_1 Int,
	@P_2 NVarChar(50),
	@P_3 NVarChar(50),
	@P_4 NVarChar(50),
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; IF NOT EXISTS (SELECT * FROM [Users_tracking] WHERE [Id] = @P_1) BEGIN SET IDENTITY_INSERT [Users] ON; INSERT INTO [Users]([Id], [Username], [Password], [Mail]) VALUES (@P_1, @P_2, @P_3, @P_4);  SET @sync_row_count = @@rowcount; SET IDENTITY_INSERT [Users] OFF; END 
END