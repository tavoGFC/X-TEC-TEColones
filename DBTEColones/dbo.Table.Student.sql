CREATE TABLE [dbo].[Student] (
    [Id]             INT    NOT NULL,
    [Description]    VARCHAR (300) NULL,
    [TCS] INT,   
    CONSTRAINT [FK_Student_User] FOREIGN KEY ([Id]) REFERENCES [dbo].[User] ([Id])
);


