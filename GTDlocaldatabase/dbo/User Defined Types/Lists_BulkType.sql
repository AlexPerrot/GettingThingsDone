CREATE TYPE [dbo].[Lists_BulkType] AS TABLE (
    [Id]                         INT            NOT NULL,
    [Title]                      NVARCHAR (50)  NULL,
    [Description]                NVARCHAR (MAX) NULL,
    [Owner]                      INT            NOT NULL,
    [sync_update_peer_timestamp] BIGINT         NULL,
    [sync_update_peer_key]       INT            NULL,
    [sync_create_peer_timestamp] BIGINT         NULL,
    [sync_create_peer_key]       INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC, [Owner] ASC));

