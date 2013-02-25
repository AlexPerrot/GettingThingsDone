CREATE TABLE [dbo].[Projects] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (50) NOT NULL,
    [Description] NVARCHAR (50) NULL,
    [Owner]       INT           NOT NULL,
    CONSTRAINT [PK__Projects__3214EC071367E606] PRIMARY KEY CLUSTERED ([Id] ASC, [Owner] ASC),
    CONSTRAINT [FK_Projects_Users] FOREIGN KEY ([Owner]) REFERENCES [dbo].[Users] ([Id])
);










GO
CREATE TRIGGER [Projects_update_trigger] ON [Projects] FOR UPDATE AS
UPDATE [side] SET [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Projects_tracking] [side] JOIN INSERTED AS [i] ON [side].[Id] = [i].[Id] AND [side].[Owner] = [i].[Owner]
GO
CREATE TRIGGER [Projects_insert_trigger] ON [Projects] FOR INSERT AS
UPDATE [side] SET [sync_row_is_tombstone] = 0, [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Projects_tracking] [side] JOIN INSERTED AS [i] ON [side].[Id] = [i].[Id] AND [side].[Owner] = [i].[Owner]
INSERT INTO [Projects_tracking] ([i].[Id], [i].[Owner], [create_scope_local_id], [local_create_peer_key], [local_create_peer_timestamp], [update_scope_local_id], [local_update_peer_key], [sync_row_is_tombstone], [last_change_datetime], [restore_timestamp]) SELECT [i].[Id], [i].[Owner], NULL, 0, @@DBTS+1, NULL, 0, 0, GETDATE() , NULL FROM INSERTED AS [i] LEFT JOIN [Projects_tracking] [side] ON [side].[Id] = [i].[Id] AND [side].[Owner] = [i].[Owner] WHERE [side].[Id] IS NULL AND [side].[Owner] IS NULL
GO
CREATE TRIGGER [Projects_delete_trigger] ON [Projects] FOR DELETE AS
UPDATE [side] SET [sync_row_is_tombstone] = 1, [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Projects_tracking] [side] JOIN DELETED AS [d] ON [side].[Id] = [d].[Id] AND [side].[Owner] = [d].[Owner]