CREATE TABLE [dbo].[Transaction]
(
	[TransactionReference] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Amount] MONEY NOT NULL,
	[TransactionDescription] VARCHAR(MAX) NOT NULL,
	[TransactionDate] DATETIME NOT NULL,
	[Reference] varchar(30) NOT NULL,
	[Account] VARCHAR(6) NOT NULL,
	[Merchant] VARCHAR(6) NOT NULL
)