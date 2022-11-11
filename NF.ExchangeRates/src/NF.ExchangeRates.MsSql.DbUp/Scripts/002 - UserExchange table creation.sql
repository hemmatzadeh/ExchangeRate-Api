
CREATE TABLE [dbo].[UserExchangeInfo] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [UserId]          INT             NOT NULL,
    [BaseCurrency]    VARCHAR (3)     NOT NULL,
    [ToCurrency]      VARCHAR (3)     NOT NULL,
    [Amount]          DECIMAL (19, 9) NOT NULL,
    [ExchangeRate]    DECIMAL (19, 9) NOT NULL,
    [ConvertedAmount] DECIMAL (19, 9) NOT NULL,
    [Created]         DATETIME        NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

