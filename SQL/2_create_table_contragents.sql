CREATE TABLE [dbo].[Contragents] (
    [Id]   INT         IDENTITY (1, 1) NOT NULL,
    [INN]  NCHAR (12)  NOT NULL,
    [KPP]  NCHAR (9)   NULL,
    [Name] NCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИНН', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contragents', @level2type = N'COLUMN', @level2name = N'INN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'КПП', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contragents', @level2type = N'COLUMN', @level2name = N'KPP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Наименование', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contragents', @level2type = N'COLUMN', @level2name = N'Name';

