# Changelog

Todas as mudanças importantes para este projeto serão documentadas neste arquivo.

## [Não Liberado]
### Adicionado
- **Refatoração**: Mudanças sugeridas após análise do SonarCloud

## [v1.0.3] - 2025-04-23
### Alterado
- **Versionamento**: Atualização da pipeline de publicação para incluir o versionamento semântico.
- **Campos Csproj**: Atualização dos campos `ProjectUrl` e `repositoryUrl` no arquivo `.csproj` para refletir o novo repositório.

## [v1.0.0] - 2025-04-23
### Adicionado
- **Validações de Identidade**: Estratégias de validação para CPF e CNPJ.
- **Validações Financeiras**: Validação de número de Cartão de Crédito.
- **Validações de Contato**: Estratégias de validação para Email, Telefone, IP e URL.
- **Validações de Localização**: Estratégias de validação para Código Postal, Código do País (ISO) e Timezone.
- **Validações de Data**: Validações para o formato ISO 8601, formatos de data personalizados, intervalo de datas e validação de idade.
  
### Alterado
- Primeira versão pública com uma arquitetura modular e limpa, utilizando Injeção de Dependência para maior flexibilidade.
- Introdução do `ValidatorFactory` para criação dinâmica dos validadores.

### Corrigido
- Refatoração interna para maior manutenibilidade.

## [v0.1.0] - 2025-04-09
### Adicionado
- Rascunho inicial das validações para CPF e CNPJ.

---

> **Nota**: Consulte sempre as [Versões do GitHub](https://github.com/LopesRebecca/ValidationsGeneral/releases) para notas completas de lançamentos e changelogs.
