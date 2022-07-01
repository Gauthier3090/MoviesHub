CREATE TABLE [dbo].[Comment]
(
	[Id] INT UNIQUE IDENTITY(1,1) NOT NULL,
	[Headline] NVARCHAR(250) NOT NULL,
	[Body] TEXT NOT NULL,
	[CreatedAt] DATETIME NOT NULL,
	[UpdatedAt] DATETIME NULL,
	[IsActive] BIT NOT NULL DEFAULT(1),
	[UserId] INT NOT NULL,
	[PublicationId] INT NOT NULL,
	CONSTRAINT [PK_Comment] PRIMARY KEY(Id),
	CONSTRAINT [FK_UserComment] FOREIGN KEY ([UserId])
		REFERENCES [User](Id),
	CONSTRAINT [FK_Publication] FOREIGN KEY ([PublicationId])
		REFERENCES [Publication](Id),
)
