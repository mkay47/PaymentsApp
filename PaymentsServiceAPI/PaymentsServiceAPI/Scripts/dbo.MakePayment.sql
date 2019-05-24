
CREATE PROCEDURE [dbo].[MakePayment]
	@Account varchar(6),
	@Merchant varchar(6),
	@Amount int,
	@TransactionDescription varchar(max),
	@Reference varchar(30)
AS
	
	UPDATE ServiceUser SET Balance = Balance - @Amount WHERE AccountNumber = @Account
	UPDATE ServiceUser SET Balance = Balance - @Amount WHERE AccountNumber = @Merchant
	declare @TransactionDate DATETIME = GETDATE()

	INSERT INTO [TransactionRecords] (Account,Amount,TransactionDescription,TransactionDate,Reference,Merchant)
	VALUES (@Account, @Amount, @TransactionDescription, @TransactionDate, @Reference,@Merchant)

RETURN 0

/*EXEC [MakePayment] "123456",-40,"Onions","Other"*/