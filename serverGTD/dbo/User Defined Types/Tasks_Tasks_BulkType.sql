CREATE TYPE [dbo].[Tasks_Tasks_BulkType] AS TABLE (
    [Id]                         INT    NOT NULL,
    [Predecessor]                INT    NULL,
    [Successor]                  INT    NULL,
    [sync_update_peer_timestamp] BIGINT NULL,
    [sync_update_peer_key]       INT    NULL,
    [sync_create_peer_timestamp] BIGINT NULL,
    [sync_create_peer_key]       INT    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC));

