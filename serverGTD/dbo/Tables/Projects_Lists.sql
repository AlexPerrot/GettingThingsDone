CREATE TABLE [dbo].[Projects_Lists] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [Project_id] INT NOT NULL,
    [List_id]    INT NOT NULL,
    [Owner]      INT NOT NULL,
    CONSTRAINT [PK__Projects__3214EC07173876EA] PRIMARY KEY CLUSTERED ([Id] ASC, [Owner] ASC),
    CONSTRAINT [FK_Projects_Lists_Lists] FOREIGN KEY ([List_id], [Owner]) REFERENCES [dbo].[Lists] ([Id], [Owner]),
    CONSTRAINT [FK_Projects_Lists_Projects] FOREIGN KEY ([Project_id], [Owner]) REFERENCES [dbo].[Projects] ([Id], [Owner]),
    CONSTRAINT [FK_Projects_Lists_Users] FOREIGN KEY ([Owner]) REFERENCES [dbo].[Users] ([Id])
);










GO
CREATE TRIGGER [Projects_Lists_update_trigger] ON [Projects_Lists] FOR UPDATE AS
UPDATE [side] SET [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Projects_Lists_tracking] [side] JOIN INSERTED AS [i] ON [side].[Id] = [i].[Id] AND [side].[Owner] = [i].[Owner]
GO
CREATE TRIGGER [Projects_Lists_insert_trigger] ON [Projects_Lists] FOR INSERT AS
UPDATE [side] SET [sync_row_is_tombstone] = 0, [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Projects_Lists_tracking] [side] JOIN INSERTED AS [i] ON [side].[Id] = [i].[Id] AND [side].[Owner] = [i].[Owner]
INSERT INTO [Projects_Lists_tracking] ([i].[Id], [i].[Owner], [create_scope_local_id], [local_create_peer_key], [local_create_peer_timestamp], [update_scope_local_id], [local_update_peer_key], [sync_row_is_tombstone], [last_change_datetime], [restore_timestamp]) SELECT [i].[Id], [i].[Owner], NULL, 0, @@DBTS+1, NULL, 0, 0, GETDATE() , NULL FROM INSERTED AS [i] LEFT JOIN [Projects_Lists_tracking] [side] ON [side].[Id] = [i].[Id] AND [side].[Owner] = [i].[Owner] WHERE [side].[Id] IS NULL AND [side].[Owner] IS NULL
GO
CREATE TRIGGER [Projects_Lists_delete_trigger] ON [Projects_Lists] FOR DELETE AS
UPDATE [side] SET [sync_row_is_tombstone] = 1, [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Projects_Lists_tracking] [side] JOIN DELETED AS [d] ON [side].[Id] = [d].[Id] AND [side].[Owner] = [d].[Owner]