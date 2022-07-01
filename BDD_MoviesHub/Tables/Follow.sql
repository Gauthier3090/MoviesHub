CREATE TABLE [dbo].[Follow]
(
	[Target] INT NOT NULL,
	[Follower] INT NOT NULL,
	CONSTRAINT [FK_User] FOREIGN KEY ([Target])
		REFERENCES [User](Id),
	CONSTRAINT [FK_FollowUser] FOREIGN KEY ([Follower])
		REFERENCES [User](Id)
)
