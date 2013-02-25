CREATE TYPE [dbo].[Tasks_BulkType] AS TABLE (
    [Id]                         INT                NOT NULL,
    [Title]                      VARCHAR (50)       NULL,
    [Description]                VARCHAR (50)       NULL,
    [DueDate]                    DATETIMEOFFSET (7) NULL,
    [CreationDate]               DATETIMEOFFSET (7) NULL,
    [Owner]                      INT                NOT NULL,
    [sync_update_peer_timestamp] BIGINT             NULL,
    [sync_update_peer_key]       INT                NULL,
    [sync_create_peer_timestamp] BIGINT             NULL,
    [sync_create_peer_key]       INT                NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC, [Owner] ASC));

