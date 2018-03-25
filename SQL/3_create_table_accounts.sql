CREATE TABLE [dbo].[Accounts] (
    [Id]           INT        IDENTITY (1, 1) NOT NULL,
    [Number]       NCHAR (20) NOT NULL,
    [ContragentId] INT        NULL,
    [BankId]       INT        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Accounts_Banks] FOREIGN KEY ([BankId]) REFERENCES [dbo].[Banks] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK_Accounts_Contragents] FOREIGN KEY ([ContragentId]) REFERENCES [dbo].[Contragents] ([Id]) ON DELETE SET NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Расчётный счёт', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Accounts', @level2type = N'COLUMN', @level2name = N'Number';

