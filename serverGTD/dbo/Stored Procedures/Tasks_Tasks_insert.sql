CREATE PROCEDURE [Tasks_Tasks_insert]
	@P_1 Int,
	@P_2 Int,
	@P_3 Int,
	@P_4 Int,
	@P_5 Int,
	@sync_row_count Int OUTPUT
AS
BEGIN
SET @sync_row_count = 0; IF NOT EXISTS (SELECT * FROM [Tasks_Tasks_tracking] WHERE [Id] = @P_1) BEGIN INSERT INTO [Tasks_Tasks]([Id], [Predecessor_Id], [Successor_Id], [Predecessor_Owner], [Successor_Owner]) VALUES (@P_1, @P_2, @P_3, @P_4, @P_5);  SET @sync_row_count = @@rowcount;  END 
END