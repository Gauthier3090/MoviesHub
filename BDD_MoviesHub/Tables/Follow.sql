CREATE TABLE [dbo].[Follow]
(
    [Id] INT IDENTITY (1, 1) NOT NULL,
	[Target] INT NOT NULL,
	[Follower] INT NOT NULL,
	CONSTRAINT [FK_User] FOREIGN KEY ([Target])
		REFERENCES [User](Id),
	CONSTRAINT [FK_FollowUser] FOREIGN KEY ([Follower])
		REFERENCES [User](Id)
)
