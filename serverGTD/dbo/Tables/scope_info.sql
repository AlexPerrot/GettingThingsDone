CREATE TABLE [dbo].[scope_info] (
    [scope_local_id]                    INT              IDENTITY (1, 1) NOT NULL,
    [scope_id]                          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [sync_scope_name]                   NVARCHAR (100)   NOT NULL,
    [scope_sync_knowledge]              VARBINARY (MAX)  NULL,
    [scope_tombstone_cleanup_knowledge] VARBINARY (MAX)  NULL,
    [scope_timestamp]                   ROWVERSION       NULL,
    [scope_config_id]                   UNIQUEIDENTIFIER NULL,
    [scope_restore_count]               INT              DEFAULT ((0)) NOT NULL,
    [scope_user_comment]                NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_scope_info] PRIMARY KEY CLUSTERED ([sync_scope_name] ASC)
);

