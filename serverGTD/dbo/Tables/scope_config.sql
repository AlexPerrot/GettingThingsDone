CREATE TABLE [dbo].[scope_config] (
    [config_id]    UNIQUEIDENTIFIER NOT NULL,
    [config_data]  XML              NOT NULL,
    [scope_status] CHAR (1)         NULL,
    CONSTRAINT [PK_scope_config] PRIMARY KEY CLUSTERED ([config_id] ASC)
);

