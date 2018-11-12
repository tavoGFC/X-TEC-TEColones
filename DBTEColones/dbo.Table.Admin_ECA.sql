CREATE TABLE [dbo].[Admin_ECA] (
    [Id]             INT    PRIMARY KEY   NOT NULL,
    [Identification] VARCHAR (50) NULL,
    [Department]     VARCHAR (80) NULL,
    [Admin]          BIT          NULL,	
    CONSTRAINT [FK_AdminECA_User] FOREIGN KEY ([Id]) REFERENCES [User]([Id])
);

