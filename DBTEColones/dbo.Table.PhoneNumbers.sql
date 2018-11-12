CREATE TABLE [dbo].[PhoneNumbers]
(
	[Id] INT NOT NULL FOREIGN KEY REFERENCES [Student](Id),
	[PhoneNumber] int NOT NULL,
	CONSTRAINT [FK_Numbers_Student] FOREIGN KEY ([Id]) REFERENCES [Student]([Id])
)
