CREATE TABLE [dbo].[Projects] (
    [Id]          INT          NOT NULL,
    [Title]       VARCHAR (50) NOT NULL,
    [Description] VARCHAR (50) NULL,
    [Owner]       INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Projects_Users] FOREIGN KEY ([Owner]) REFERENCES [dbo].[Users] ([Id])
);




GO
CREATE TRIGGER [Projects_update_trigger] ON [Projects] FOR UPDATE AS
UPDATE [side] SET [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Projects_tracking] [side] JOIN INSERTED AS [i] ON [side].[Id] = [i].[Id]
GO
CREATE TRIGGER [Projects_insert_trigger] ON [Projects] FOR INSERT AS
UPDATE [side] SET [sync_row_is_tombstone] = 0, [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Projects_tracking] [side] JOIN INSERTED AS [i] ON [side].[Id] = [i].[Id]
INSERT INTO [Projects_tracking] ([i].[Id], [create_scope_local_id], [local_create_peer_key], [local_create_peer_timestamp], [update_scope_local_id], [local_update_peer_key], [sync_row_is_tombstone], [last_change_datetime], [restore_timestamp]) SELECT [i].[Id], NULL, 0, @@DBTS+1, NULL, 0, 0, GETDATE() , NULL FROM INSERTED AS [i] LEFT JOIN [Projects_tracking] [side] ON [side].[Id] = [i].[Id] WHERE [side].[Id] IS NULL
GO
CREATE TRIGGER [Projects_delete_trigger] ON [Projects] FOR DELETE AS
UPDATE [side] SET [sync_row_is_tombstone] = 1, [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Projects_tracking] [side] JOIN DELETED AS [d] ON [side].[Id] = [d].[Id]