CREATE TABLE [dbo].[Publication]
(
	[Id] INT NOT NULL,
	[Title] NVARCHAR(250) NOT NULL,
	[Description] TEXT NOT NULL,
	[Image] VARBINARY(MAX) NOT NULL,
	[Time_Created] DATETIME NOT NULL,
	[User] INT NOT NULL,
	CONSTRAINT [Check_Title] CHECK (DATALENGTH(Title) <= 250),
	CONSTRAINT [Unique_ID] UNIQUE(Id),
	CONSTRAINT [PK_Publication] PRIMARY KEY(Id),
	CONSTRAINT [FK_UserPublication] FOREIGN KEY ([User])
		REFERENCES [User](Id),
)
