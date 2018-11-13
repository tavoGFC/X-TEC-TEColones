CREATE TABLE [dbo].[PhoneNumber]
(
	[Id] INT NOT NULL,
	[PhoneNumber] int NOT NULL,
	CONSTRAINT [FK_PhoneNumber_User] FOREIGN KEY ([Id]) REFERENCES [User]([Id])
);
