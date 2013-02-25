CREATE TABLE [dbo].[Projects_Lists_Tasks] (
    [Id]              INT IDENTITY (1, 1) NOT NULL,
    [Project_List_id] INT NOT NULL,
    [Owner]           INT NOT NULL,
    [Task_id]         INT NOT NULL,
    CONSTRAINT [PK_Projects_Lists_Tasks] PRIMARY KEY CLUSTERED ([Id] ASC, [Owner] ASC),
    CONSTRAINT [FK_Projects_Lists_Tasks_Projects_Lists] FOREIGN KEY ([Project_List_id], [Owner]) REFERENCES [dbo].[Projects_Lists] ([Id], [Owner]),
    CONSTRAINT [FK_Projects_Lists_Tasks_Tasks] FOREIGN KEY ([Task_id], [Owner]) REFERENCES [dbo].[Tasks] ([Id], [Owner]),
    CONSTRAINT [FK_Projects_Lists_Tasks_Users] FOREIGN KEY ([Owner]) REFERENCES [dbo].[Users] ([Id])
);

