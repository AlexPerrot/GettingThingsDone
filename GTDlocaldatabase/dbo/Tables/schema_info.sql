CREATE TABLE [dbo].[schema_info] (
    [schema_major_version] INT            NOT NULL,
    [schema_minor_version] INT            NOT NULL,
    [schema_extended_info] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_schema_info] PRIMARY KEY CLUSTERED ([schema_major_version] ASC, [schema_minor_version] ASC)
);

