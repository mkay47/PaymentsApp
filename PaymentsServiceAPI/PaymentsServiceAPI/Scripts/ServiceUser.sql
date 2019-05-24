CREATE TABLE [dbo].[ServiceUser](
	[Username] VARCHAR(30) NOT NULL PRIMARY KEY,
	[Password] VARCHAR(MAX) NOT NULL,	
	[Name] VARCHAR(30) NOT NULL,
	[Surname] VARCHAR(30) NOT NULL,
	[Email] VARCHAR(MAX),
	[Phone] VARCHAR(12),
	[Balance] MONEY NOT NULL,
	[AccountNumber] VARCHAR(MAX) NOT NULL
	
)