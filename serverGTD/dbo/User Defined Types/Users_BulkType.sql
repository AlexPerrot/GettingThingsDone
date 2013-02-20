CREATE TYPE [dbo].[Users_BulkType] AS TABLE (
    [Id]                         INT           NOT NULL,
    [Username]                   NVARCHAR (50) NULL,
    [Password]                   NVARCHAR (50) NULL,
    [Mail]                       NVARCHAR (50) NULL,
    [sync_update_peer_timestamp] BIGINT        NULL,
    [sync_update_peer_key]       INT           NULL,
    [sync_create_peer_timestamp] BIGINT        NULL,
    [sync_create_peer_key]       INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC));



