CREATE TABLE [dbo].[Banks] (
    [Id]                   INT         IDENTITY (1, 1) NOT NULL,
    [Name]                 NCHAR (200) NOT NULL,
    [Bic]                  NCHAR (9)   NOT NULL,
    [CorrespondingAccount] NCHAR (20)  NOT NULL,
    [City]                 NCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Наименование банка', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Banks', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'БИК', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Banks', @level2type = N'COLUMN', @level2name = N'Bic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Кор/счёт', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Banks', @level2type = N'COLUMN', @level2name = N'CorrespondingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Город банка', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Banks', @level2type = N'COLUMN', @level2name = N'City';

