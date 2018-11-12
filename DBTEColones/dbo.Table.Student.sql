CREATE TABLE [dbo].[Student] (
    [Id]             INT       PRIMARY KEY      NOT NULL,
    [Identification] VARCHAR (50)  NOT NULL,
    [Description]    VARCHAR (300) NULL,
    [Skills]         VARCHAR (300) NOT NULL,
    UNIQUE NONCLUSTERED ([Identification] ASC),
    CONSTRAINT [FK_Student_User] FOREIGN KEY ([Id]) REFERENCES [User]([Id])
);

