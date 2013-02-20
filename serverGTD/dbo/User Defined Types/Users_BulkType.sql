CREATE TYPE [dbo].[Users_BulkType] AS TABLE (
    [Id]                         INT          NOT NULL,
    [Username]                   VARCHAR (50) NULL,
    [Password]                   VARCHAR (50) NULL,
    [Mail]                       VARCHAR (50) NULL,
    [sync_update_peer_timestamp] BIGINT       NULL,
    [sync_update_peer_key]       INT          NULL,
    [sync_create_peer_timestamp] BIGINT       NULL,
    [sync_create_peer_key]       INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC));

