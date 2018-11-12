CREATE TABLE [dbo].[User] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [FirstName]   VARCHAR (50)   NOT NULL,
    [LastName]    VARCHAR (100)     NOT NULL,
    [University]  VARCHAR (50)      NOT NULL,
    [Headquarter] VARCHAR (50)      NOT NULL,
    [Email]       VARCHAR (100)     NOT NULL,
    [Password]    VARBINARY (MAX) NOT NULL,
    [Photo]       IMAGE           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

