# Desafio Desenvolvimento

## O Desafio

Tudo começa conhecendo a aplicação Web ASP.NET CORE “Desafio Desenvolvimento” – um sistema simples de registro de chamados. Essa aplicação é composta por telas CRUD e relatórios de listagem de dados. Como todo sistema, ela possui alguns erros (que foram deixados de propósito) e alguns módulos inacabados.

O desafio é conhecer a aplicação, identificar os erros e corrigi-los, construir novos módulos e fazer ajustes em módulos já existentes. Se possível, podem também serem implementadas melhorias no sistema.

## Conhecendo a Aplicação

![image](https://github.com/user-attachments/assets/32ec6d5e-91d9-4a7d-bec6-1320f4b4263c)

Para trabalhar com a aplicação, você irá utilizar o Visual Studio 2022 (pode ser utilizado também o 2019). Caso você não tenha esse ambiente de desenvolvimento instalado, você pode obter a versão Community Edition gratuitamente.

A aplicação é composta por alguns formulários para realização de CRUD para Chamados e Departamentos, relatórios de listagem, e uma classe para acesso aos dados. Ela utiliza como banco de dados o SQLite, e já possui alguns dados para que você possa visualizar e alterar. Ela utiliza algumas bibliotecas (fornecidas no pacote enviado) e também um pacote NuGet para o componente de relatórios Report Viewer. Esse é um componente gratuito e não necessita de instalações adicionais. Porém, para trabalhar com ele, você precisará instalar uma extensão do Visual Studio – o Microsoft RDLC Report Designer. Com essa extensão, você poderá criar e alterar relatórios Report Viewer.

![image](https://github.com/user-attachments/assets/94a197cb-8396-4b3b-ae35-1de51a57db43)

Ao abrir o projeto pela primeira vez no Visual Studio, certifique-se de restaurar os pacotes NuGet antes de compilar.

## O Que Esperamos?

1. O módulo de Departamentos está incompleto – deve ser implementado o CRUD.
2. O relatório de Departamentos não está sendo chamado através da interface – não foi implementado.
3. Existe(m) erro(s) no módulo de Chamados – deve(m) ser corrigido(s).
4. Telas de listagem de dados – implementar duplo-clique para editar.
5. Validação da entrada de dados do usuário – tamanho máximo de campos texto, campos numéricos permitir somente números, etc.

## Desafio Extra (Opcional)

- Validação de Regras de Negócio – não permitir a criação de chamados com data retroativa.
- Pesquisa de Solicitantes na criação de chamados - Autocomplete.
- Outras melhorias que você considerar pertinentes.

## Avaliação

O que vamos avaliar:

- Lógica
- Padronização de código
- Domínio da linguagem / tecnologia
- Uso de boas práticas

## Entrega

As respostas devem ser elaboradas no Visual Studio 2022 / 2019 e enviadas por e-mail (em anexo ou com o link para a plataforma de sua preferência – GitHub, SourceForge, etc).