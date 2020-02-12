CREATE TABLE [dbo].[NpvVariableCashflow]
(
	[Id] INT IDENTITY(1,1), 
    [NpvVariableId] INT NOT NULL, 
    [Cashflow] DECIMAL(16, 2) NOT NULL, 
    [Order] INT NOT NULL,
	CONSTRAINT [PK_NpvVariableCashflow_Id] PRIMARY KEY([Id]),
	CONSTRAINT [FK_NpvVariableCashflow_NpvVariableId] FOREIGN KEY([NpvVariableId]) REFERENCES [NpvVariable]([Id])
)
