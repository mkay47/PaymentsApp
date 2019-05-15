
CREATE PROCEDURE [dbo].[LoadAccount]
	@Account varchar(6),
	@Amount int
AS
	
	UPDATE ServiceUser SET Balance = Balance + @Amount WHERE AccountNumber = @Account

RETURN 0

/*EXEC [MakePayment] "123456",-40,"Onions","Other"*/