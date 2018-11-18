CREATE TABLE [dbo].[Log_Assign] (
    [IdUser]             INT          NOT NULL,
    [Benefit]            VARCHAR (50) NOT NULL,
    [TCS]                INT          NOT NULL,
    [CS]                 INT          NOT NULL,
    [DateAssign]               DATE         NOT NULL,
    [ExchangeRateToDate] FLOAT (53)   NOT NULL
);

