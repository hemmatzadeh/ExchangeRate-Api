CREATE PROCEDURE [dbo].AddUserTrade
	@userId int,
    @provider tinyint,
	@from varchar(3),
	@to varchar(3),
	@amount decimal(19,9),
	@rate decimal(19,9)
AS
	insert into UserExchangeInfo
(
    [UserId]  
    ,[Provider]
    ,[BaseCurrency]    
    ,[ToCurrency]      
    ,[Amount]          
    ,[ExchangeRate]    
    ,[ConvertedAmount] 
    ,[Created]             
)
values
(
@userId,@provider,@from,@to,@amount,@rate,@amount*@rate,getdate()
)

RETURN @amount*@rate
