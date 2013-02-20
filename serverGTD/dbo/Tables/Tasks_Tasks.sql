﻿CREATE TABLE [dbo].[Tasks_Tasks] (
    [Id]          INT NOT NULL,
    [Predecessor] INT NOT NULL,
    [Successor]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tasks_Tasks_Tasks] FOREIGN KEY ([Predecessor]) REFERENCES [dbo].[Tasks] ([Id]),
    CONSTRAINT [FK_Tasks_Tasks_Tasks2] FOREIGN KEY ([Successor]) REFERENCES [dbo].[Tasks] ([Id])
);




GO
CREATE TRIGGER [Tasks_Tasks_update_trigger] ON [Tasks_Tasks] FOR UPDATE AS
UPDATE [side] SET [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Tasks_Tasks_tracking] [side] JOIN INSERTED AS [i] ON [side].[Id] = [i].[Id]
GO
CREATE TRIGGER [Tasks_Tasks_insert_trigger] ON [Tasks_Tasks] FOR INSERT AS
UPDATE [side] SET [sync_row_is_tombstone] = 0, [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Tasks_Tasks_tracking] [side] JOIN INSERTED AS [i] ON [side].[Id] = [i].[Id]
INSERT INTO [Tasks_Tasks_tracking] ([i].[Id], [create_scope_local_id], [local_create_peer_key], [local_create_peer_timestamp], [update_scope_local_id], [local_update_peer_key], [sync_row_is_tombstone], [last_change_datetime], [restore_timestamp]) SELECT [i].[Id], NULL, 0, @@DBTS+1, NULL, 0, 0, GETDATE() , NULL FROM INSERTED AS [i] LEFT JOIN [Tasks_Tasks_tracking] [side] ON [side].[Id] = [i].[Id] WHERE [side].[Id] IS NULL
GO
CREATE TRIGGER [Tasks_Tasks_delete_trigger] ON [Tasks_Tasks] FOR DELETE AS
UPDATE [side] SET [sync_row_is_tombstone] = 1, [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Tasks_Tasks_tracking] [side] JOIN DELETED AS [d] ON [side].[Id] = [d].[Id]