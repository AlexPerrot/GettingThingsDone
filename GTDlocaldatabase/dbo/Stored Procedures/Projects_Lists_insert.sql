CREATE PROCEDURE [Projects_Lists_insert]
	@P_1 Int,
	@P_2 Int,
	@P_3 Int,
	@P_4 Int,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; IF NOT EXISTS (SELECT * FROM [Projects_Lists_tracking] WHERE [Id] = @P_1 AND [Owner] = @P_4) BEGIN SET IDENTITY_INSERT [Projects_Lists] ON; INSERT INTO [Projects_Lists]([Id], [Project_id], [List_id], [Owner]) VALUES (@P_1, @P_2, @P_3, @P_4);  SET @sync_row_count = @@rowcount; SET IDENTITY_INSERT [Projects_Lists] OFF; END 
END