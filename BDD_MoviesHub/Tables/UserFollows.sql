CREATE TABLE [dbo].[UserFollows]
(
	[User] INT NOT NULL,
	[Followuser] INT NOT NULL,
	CONSTRAINT [FK_User] FOREIGN KEY ([User])
		REFERENCES [User](Id),
	CONSTRAINT [FK_FollowUser] FOREIGN KEY ([Followuser])
		REFERENCES [User](Id)
)
