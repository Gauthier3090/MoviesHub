CREATE TABLE [dbo].[GroupActif]
(
	[GroupId] INT NOT NULL,
	[UserId] INT NOT NULL,
	CONSTRAINT [FK_GroupActif] FOREIGN KEY ([GroupId])
		REFERENCES [Group](Id),
	CONSTRAINT [FK_UserId] FOREIGN KEY ([UserId])
		REFERENCES [User](Id)
)
