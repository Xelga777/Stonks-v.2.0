CREATE TABLE [dbo].[Accounts] (
    [OwnersName] NVARCHAR (50) NOT NULL,
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Value]       INT           NULL,
    CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED ([ID] ASC)
);

