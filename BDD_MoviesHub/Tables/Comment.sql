CREATE TABLE [dbo].[Comment]
(
	[Id] INT NOT NULL,
	[Publication] INT NOT NULL,
	[User] INT NOT NULL,
	[Headline] NVARCHAR(250) NOT NULL,
	[Body] TEXT NOT NULL,
	[Time_Created] DATETIME NOT NULL,
	CONSTRAINT [Check_Headline] CHECK (DATALENGTH(Headline) <= 250),
	CONSTRAINT [Unique_Comment] UNIQUE(Id),
	CONSTRAINT [PK_Comment] PRIMARY KEY(Id),
	CONSTRAINT [FK_UserComment] FOREIGN KEY ([User])
		REFERENCES [User](Id),
)
