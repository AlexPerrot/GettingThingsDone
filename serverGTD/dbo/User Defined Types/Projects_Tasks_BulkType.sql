CREATE TYPE [dbo].[Projects_Tasks_BulkType] AS TABLE (
    [Id]                         INT    NOT NULL,
    [Project_id]                 INT    NULL,
    [Task_id]                    INT    NULL,
    [Owner]                      INT    NULL,
    [sync_update_peer_timestamp] BIGINT NULL,
    [sync_update_peer_key]       INT    NULL,
    [sync_create_peer_timestamp] BIGINT NULL,
    [sync_create_peer_key]       INT    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC));



