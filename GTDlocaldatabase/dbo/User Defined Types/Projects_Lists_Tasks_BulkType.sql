CREATE TYPE [dbo].[Projects_Lists_Tasks_BulkType] AS TABLE (
    [Id]                         INT    NOT NULL,
    [Project_List_id]            INT    NULL,
    [Owner]                      INT    NOT NULL,
    [Task_id]                    INT    NULL,
    [sync_update_peer_timestamp] BIGINT NULL,
    [sync_update_peer_key]       INT    NULL,
    [sync_create_peer_timestamp] BIGINT NULL,
    [sync_create_peer_key]       INT    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC, [Owner] ASC));

