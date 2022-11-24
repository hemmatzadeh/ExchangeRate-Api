CREATE TABLE [dbo].[RateInfo]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Provider] TINYINT NOT NULL,
    [BaseCurrency] VARCHAR(3) NOT NULL, 
    [ToCurrency] NCHAR(10) NOT NULL, 
    [Rate] DECIMAL(19, 9) NOT NULL, 
    [Created] DATETIME NOT NULL
)