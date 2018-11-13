CREATE TABLE [dbo].[Admin_SCM] (
    [Id]             INT    NOT NULL,
    [Department]     VARCHAR (80) NULL,
    [Admin]          BIT          NULL,	
    CONSTRAINT [FK_AdminECA_User] FOREIGN KEY ([Id]) REFERENCES [User]([Id])
);

