﻿CREATE TABLE [dbo].[Tasks_Tasks] (
    [Id]             INT IDENTITY (1, 1) NOT NULL,
    [Predecessor_Id] INT NOT NULL,
    [Successor_Id]   INT NOT NULL,
    [Owner]          INT NOT NULL,
    CONSTRAINT [PK__Tasks_Ta__3214EC07267ABA7A] PRIMARY KEY CLUSTERED ([Id] ASC, [Owner] ASC),
    CONSTRAINT [FK_Tasks_Tasks_S] FOREIGN KEY ([Successor_Id], [Owner]) REFERENCES [dbo].[Tasks] ([Id], [Owner]),
    CONSTRAINT [FK_Tasks_Tasks_Users] FOREIGN KEY ([Owner]) REFERENCES [dbo].[Users] ([Id])
);




GO
CREATE TRIGGER [Tasks_Tasks_delete_trigger] ON [Tasks_Tasks] FOR DELETE AS
UPDATE [side] SET [sync_row_is_tombstone] = 1, [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Tasks_Tasks_tracking] [side] JOIN DELETED AS [d] ON [side].[Id] = [d].[Id] AND [side].[Owner] = [d].[Owner]
GO
CREATE TRIGGER [Tasks_Tasks_insert_trigger] ON [Tasks_Tasks] FOR INSERT AS
UPDATE [side] SET [sync_row_is_tombstone] = 0, [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Tasks_Tasks_tracking] [side] JOIN INSERTED AS [i] ON [side].[Id] = [i].[Id] AND [side].[Owner] = [i].[Owner]
INSERT INTO [Tasks_Tasks_tracking] ([i].[Id], [i].[Owner], [create_scope_local_id], [local_create_peer_key], [local_create_peer_timestamp], [update_scope_local_id], [local_update_peer_key], [sync_row_is_tombstone], [last_change_datetime], [restore_timestamp]) SELECT [i].[Id], [i].[Owner], NULL, 0, @@DBTS+1, NULL, 0, 0, GETDATE() , NULL FROM INSERTED AS [i] LEFT JOIN [Tasks_Tasks_tracking] [side] ON [side].[Id] = [i].[Id] AND [side].[Owner] = [i].[Owner] WHERE [side].[Id] IS NULL AND [side].[Owner] IS NULL
GO
CREATE TRIGGER [Tasks_Tasks_update_trigger] ON [Tasks_Tasks] FOR UPDATE AS
UPDATE [side] SET [local_update_peer_key] = 0, [restore_timestamp] = NULL, [update_scope_local_id] = NULL, [last_change_datetime] = GETDATE() FROM [Tasks_Tasks_tracking] [side] JOIN INSERTED AS [i] ON [side].[Id] = [i].[Id] AND [side].[Owner] = [i].[Owner]