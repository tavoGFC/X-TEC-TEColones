CREATE TABLE [dbo].[Emails]
(
	[Id] INT NOT NULL FOREIGN KEY REFERENCES [Student](Id),
	[Email] varchar(100) NOT NULL
)
