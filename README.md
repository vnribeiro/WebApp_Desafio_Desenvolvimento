# Desafio Desenvolvimento

## Links
- [Desafio](DESAFIO.md)

## Descrição do Projeto
Este projeto foi migrado da versão .NET Core 2.1 para .NET 6.0 e agora inclui a implementação do Entity Framework Core (EF Core) junto com o padrão Repository. O EF Core facilita o gerenciamento de dados em bancos de dados relacionais, e o padrão Repository abstrai as operações de dados, fornecendo uma camada de acesso consistente. O banco de dados utilizado é o SQLite.

Além disso, foi adicionado um módulo de Departamento, que permite criar, atualizar, obter e deletar departamentos de maneira simplificada.

## Atualizações
- **Migração do Framework:** O projeto foi atualizado da versão .NET Core 2.1 para .NET 6.0.
- **Entity Framework Core:** Implementação do EF Core para gerenciamento de dados, junto com o padrão Repository.
- **Banco de Dados:** Utilização do SQLite como banco de dados.
- **Módulo de Departamento:** Adição de funcionalidades para criar, atualizar, obter e deletar departamentos.
- **Refatoração de Código:** Atualizações e melhorias no código para compatibilidade com .NET 6.0 e melhores práticas.
- **Dependências:** Atualização de todas as dependências para versões compatíveis com .NET 6.0.
- **Novos Recursos:** Aproveitamento de novos recursos e melhorias oferecidos pelo .NET 6.0.

## Módulo de Departamento
O módulo de Departamento agora inclui o CRUD completo:
- Criar um Departamento
- Atualizar um Departamento
- Deletar um Departamento
- Obter um Departamento

## Melhorias Implementadas
- **Correção no Módulo de Chamados:** Identificados e corrigidos erros no módulo de Chamados.
- **Listagem de Dados:** Implementado duplo-clique para edição nas telas de listagem de dados.
- **Validação da Entrada de Dados:** Adicionada validação para garantir o tamanho máximo de campos texto e permitir apenas números em campos numéricos.
- **Validação de Regras de Negócio:** Implementada validação para não permitir a criação de chamados com data retroativa.
- **Pesquisa de Solicitantes:** Adicionada funcionalidade de autocomplete para pesquisa de solicitantes na criação de chamados.
- **Ajustes no JavaScript:** Ajustado o código JavaScript para resolver warnings de funções depreciadas.
- **Outras Melhorias:** Diversas melhorias foram realizadas para otimizar a usabilidade e a eficiência do sistema.
