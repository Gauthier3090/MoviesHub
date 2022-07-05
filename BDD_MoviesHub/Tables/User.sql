﻿CREATE TABLE [dbo].[User]
(
	[Id] INT UNIQUE IDENTITY(1,1) NOT NULL,
	[Email] NVARCHAR(300) UNIQUE NOT NULL,
	[Firstname] NVARCHAR(150) NOT NULL,
	[Lastname] NVARCHAR(200) NOT NULL,
	[Password] NVARCHAR(200) NOT NULL,
	[Birthdate] DATE NOT NULL,
	[Image] NVARCHAR(MAX) NOT NULL,
	[CreatedAt] DATETIME NOT NULL,
	[UpdatedAt] DATETIME NULL,
	[IsActive] BIT DEFAULT(1),
	[LastLogin] DATETIME NULL,
	CONSTRAINT [PK_USER] PRIMARY KEY(Id),
)
