CREATE TABLE [dbo].[Publication]
(
	[Id] INT NOT NULL,
	[Title] NVARCHAR(250) NOT NULL,
	[Description] TEXT NOT NULL,
	[Image] VARBINARY(MAX) NOT NULL,
	[CreatedAt] DATETIME NOT NULL,
	[UpdatedAt] DATETIME NULL,
	[IsActive] BIT NOT NULL DEFAULT(1),
	[Creator] INT NOT NULL,
	CONSTRAINT [PK_Publication] PRIMARY KEY(Id),
	CONSTRAINT [FK_UserPublication] FOREIGN KEY ([Creator])
		REFERENCES [User](Id),
)
