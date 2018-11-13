CREATE TABLE [dbo].[Emails]
(
	[Id] INT NOT NULL,
	[Email] varchar(100) NOT NULL,
	CONSTRAINT [FK_Email_User] FOREIGN KEY ([Id]) REFERENCES [User]([Id])
);
