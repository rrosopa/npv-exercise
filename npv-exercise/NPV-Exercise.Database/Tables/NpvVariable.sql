CREATE TABLE [dbo].[NpvVariable]
(
	[Id] INT IDENTITY(1,1), 
    [InitialValue] DECIMAL(16, 2) NULL, 
    [LowerBoundRate] DECIMAL(8, 4) NULL, 
    [UpperBoundRate] DECIMAL(8, 4) NULL, 
    [Increment] DECIMAL(8, 4) NULL,
	CONSTRAINT [PK_NpvVariable_Id] PRIMARY KEY([Id])
)
