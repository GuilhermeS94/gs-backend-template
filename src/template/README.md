# 💻 Sua Aplicacao 📝
Agora que temos uma base criada, vamos moldar a solucao conforme a necessidade, adicionando itens e complementos. 
<br/>

## Estrutura do projeto:
- [GS.Backend.Dominios] Representa os dominios de negocio da aplicao, tanto entradas e saidas, quanto os idiomas, excecoes, middlewares, notificacoes, validacoes, processadores e contratos de servicos externos. Aqui ficam os objetos bases para o projeto.
- [GS.Backend.Infra] Configura os servicos que irao compor aplicacao, associacao de command pattern, contratos com servicos externos, idiomas, headers, tudo que deve ser adicionado pela startup do projeto.
- [GS.Backend.*.Api] API, projeto o qual tem a classe principal que inicia aplicacao, mas antes adiciona tudo que foi relatado na infra para o funcionamento pleno e saudavel do micro servico.
- [GS.Backend.ServicosExternos] As acoes do negocio, projeto o qual realmente acontece os processamentos.
- [GS.Backend.Testes] Os testes necessarios para aprovar um algoritmo ou processamento.

## 🚀 Adiconando itens da aplicacao:
- Criar, replicar, preencher o arquivo **GS.Backend.*.Api\appsettings.{env}.json** de acordo com seus ambientes
- Criar os comandos que representam as entradas de seus endpoints em **GS.Backend.Dominios\Comandos**
- Criar as entradas que representam as entradas de seus dominios internos em **GS.Backend.Dominios\Modelos\Entradas**
- Criar as saidas que representam os retornos de seus dominios em **GS.Backend.Dominios\Modelos\Saidas**
- Criar a validacao de sua entrada, que aplicara regras de campo e/ou negocio para permitir ou nao o processamento **GS.Backend.Dominios\Validacoes**
- Criar o processador de sua request em **GS.Backend.Dominios\Processadores**, o qual recebe seu comando, verifica as regras, e se validado, segue o processamento
- Criar os contratos de interacao com seu dominio em **GS.Backend.Dominios\ServicosExternos**, os quais representarao as acoes/processamentos que serao executados, com suas respectivas entradas e retornos
- Criar e implementar os metodos/classes/acoes em **GS.Backend.ServicosExternos** conforme os contratos do item anterior
- Criar os controllers em **GS.Backend.*.Api\Controllers** os quais representarao seus base paths e endpoints, com suas respectivas entradas e saidas
- Criar os testes necessarios conforme suas regras em **GS.Backend.Testes**
- Criar em **GS.Backend.Infra** e/ou adicionar os parametros/conteudos/dependencias necessarios para rodar sua aplicacao, por exemplo:
    - Na classe **AddConfiguracoesServices**, no metodo **AddComandos**, adicionar os comandos criados
    - Na classe **AddConfiguracoesServices**, no metodo **AddServicosExternos**, adicionar os pares **<Contrato, ServicoExterno>** criados

## Versoes .Net:
| descricao | codigo CLI |
| --: | :-- | 
| **.Net 7** | `net7.0` |
| **.Net 6** | `net6.0` |
| **.Net Core 3.1** | `netcoreapp3.1` |

[documentacao e mais versoes](https://learn.microsoft.com/en-us/dotnet/standard/frameworks)

## Tipos de projeto:
| descricao | codigo CLI |
| --: | :-- |
| **Web API** | `webapi` |
| **DLL** | `classlib` |
| **Web API** | `console` |
| **Testes** | `xunit` |

[documentacao e mais projetos](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new)

## 🔖 Comandos uteis:
<!-- | :-- (esquerda para direita) | :-: central | --: (direita para esquerda) | -->
| comando | descricao | linha completa |
| :-- | - | :-- |
| `sln` | criar solution `.sln` | `dotnet new sln -n {NOME_SOLUTION}` |
| `new` | criar projeto conforme tipo especificado | `dotnet new {TIPO_PROJETO} -f {.NET_VERSION}` |
| `sln add` | associar um projeto a solution | `dotnet sln add {NOME_PASTA_PROJETO}\{NOME_PROJETO}.csproj` |
| `sln add` | associar n projetos a solution | `dotnet sln {NOME_SOLUTION}.sln add {NOME_PASTA_PROJETO_N}\{NOME_PROJETO_N}.csproj {NOME_PASTA_PROJETO_N}\{NOME_PROJETO_N}.csproj` |
| `reference` | adicionar referencia de projeto | `dotnet add {NOME_PROJETO} reference --interactive {NOME_PASTA_PROJETO_N}\{NOME_PROJETO_N}.csproj` |
| `restore` | restaurar/baixar pacotes nuget | `dotnet restore` |
| `build` | compilar a solucao | `dotnet build` |
| `run` | rodar o projeto | `dotnet run` |
| `test` | testar o projeto | `dotnet test -f {.NET_VERSION} --configuration Release {NOME_PASTA_PROJETO_TESTES}\{NOME_PROJETO_TESTES}.csproj` |
| `package` | adicionar nuget no projeto | `dotnet add package Nome.Do.Nuget` |