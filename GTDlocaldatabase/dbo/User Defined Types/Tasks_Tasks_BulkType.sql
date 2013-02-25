CREATE TYPE [dbo].[Tasks_Tasks_BulkType] AS TABLE (
    [Id]                         INT    NOT NULL,
    [Predecessor_Id]             INT    NULL,
    [Successor_Id]               INT    NULL,
    [Predecessor_Owner]          INT    NULL,
    [Successor_Owner]            INT    NULL,
    [sync_update_peer_timestamp] BIGINT NULL,
    [sync_update_peer_key]       INT    NULL,
    [sync_create_peer_timestamp] BIGINT NULL,
    [sync_create_peer_key]       INT    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC));

