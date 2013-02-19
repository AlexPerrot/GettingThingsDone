CREATE TABLE [dbo].[Users] (
    [Id]       INT          NOT NULL,
    [Username] VARCHAR (50) NOT NULL,
    [Password] VARCHAR (50) NOT NULL,
    [Mail]     VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO
CREATE TRIGGER [Users_update_trigger] ON [Users] FOR UPDATE AS
UPDATE [side] SET [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Users_tracking] [side] JOIN INSERTED AS [i] ON [side].[Id] = [i].[Id]
GO
CREATE TRIGGER [Users_insert_trigger] ON [Users] FOR INSERT AS
UPDATE [side] SET [sync_row_is_tombstone] = 0, [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Users_tracking] [side] JOIN INSERTED AS [i] ON [side].[Id] = [i].[Id]
INSERT INTO [Users_tracking] ([i].[Id], [create_scope_local_id], [local_create_peer_key], [local_create_peer_timestamp], [update_scope_local_id], [local_update_peer_key], [sync_row_is_tombstone], [last_change_datetime], [restore_timestamp]) SELECT [i].[Id], NULL, 0, @@DBTS+1, NULL, 0, 0, GETDATE() , NULL FROM INSERTED AS [i] LEFT JOIN [Users_tracking] [side] ON [side].[Id] = [i].[Id] WHERE [side].[Id] IS NULL
GO
CREATE TRIGGER [Users_delete_trigger] ON [Users] FOR DELETE AS
UPDATE [side] SET [sync_row_is_tombstone] = 1, [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Users_tracking] [side] JOIN DELETED AS [d] ON [side].[Id] = [d].[Id]