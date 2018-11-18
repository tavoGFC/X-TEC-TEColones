CREATE TABLE [dbo].[User] (
    [Id]          INT      NOT NULL,
    [FirstName]   VARCHAR (50)    NOT NULL,
    [LastName]    VARCHAR (100)   NOT NULL,
    [University]  VARCHAR (50)    NOT NULL,
    [Headquarter] VARCHAR (50)    NOT NULL,
    [Email]       VARCHAR (100)   NOT NULL,
    [Password]    VARBINARY (MAX) NOT NULL,
    [Photo]       VARBINARY (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

