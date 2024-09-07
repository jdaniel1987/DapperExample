CREATE TABLE [dbo].[Marks] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (MAX)   NULL,
    [Surname]      VARCHAR (MAX)   NULL,
    [Score]        NUMERIC (18, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
