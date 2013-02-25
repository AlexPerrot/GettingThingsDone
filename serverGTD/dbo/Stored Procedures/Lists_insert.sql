CREATE PROCEDURE [Lists_insert]
	@P_1 Int,
	@P_2 NVarChar(50),
	@P_3 NVarChar(max),
	@P_4 Int,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; IF NOT EXISTS (SELECT * FROM [Lists_tracking] WHERE [Id] = @P_1 AND [Owner] = @P_4) BEGIN SET IDENTITY_INSERT [Lists] ON; INSERT INTO [Lists]([Id], [Title], [Description], [Owner]) VALUES (@P_1, @P_2, @P_3, @P_4);  SET @sync_row_count = @@rowcount; SET IDENTITY_INSERT [Lists] OFF; END 
END