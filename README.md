# 💻 gs-backend-template 📝
Projeto modelo para criação de uma API em .Net 7.<br/>
Estrutura contempla uma API, multidiomas, com tratativa de erro global, validacoes de campos de entrada, mensagens informativas, TDD, testes automatizados,
<br/>

## Estrutura do projeto:
- [GS.Backend.Dominios] Representa os dominios de negocio da aplicao, tanto entradas e saidas, quanto os idiomas, excecoes, middlewares, notificacoes, validacoes, processadores e contratos de servicos externos. Aqui ficam os objetos bases para o projeto.
- [GS.Backend.Infra] Configura os servicos que irao compor aplicacao, associacao de command pattern, contratos com servicos externos, idiomas, headers, tudo que deve ser adicionado pela startup do projeto.
- [GS.Backend.*.Api] API, projeto o qual tem a classe principal que inicia aplicacao, mas antes adiciona tudo que foi relatado na infra para o funcionamento pleno e saudavel do micro servico.
- [GS.Backend.ServicosExternos] As acoes do negocio, projeto o qual realmente acontece os processamentos.
- [GS.Backend.Testes] Os testes necessarios para aprovar um algoritmo ou processamento.

## 🚀 Usando este projeto como modelo:
 - [Clone do projeto](https://github.com/GuilhermeS94/gs-backend-template)
 - Abrir terminal, na pasta raiz do projeto (**cd src**)
 - Criar pacote do modelo com comando: **dotnet pack -o ./package**
 - Abrir a pasta que deseja criar o projeto no terminal e instalar o template com comando: **dotnet new --install [CAMINHO COMPLETO DO NUGET GERADO NO PASSO ANTERIOR]** (removendo os colchetes, e com nome do arquivo e extensao, uma vez instalado, pode ir diretamente para criacao da sua aplicacao)
 - Criar seu projeto com comando: **dotnet new gs-api -o [NOMESEUPROJETO]** (removendo os colchetes)
 - Faca os renames de `namespace` necessarios com sua preferencia ou de sua empresa


## 🔖 Comandos uteis:
<!-- | :-- (esquerda para direita) | :-: central | --: (direita para esquerda) | -->
| comando | descricao | linha completa |
| :-- | - | :-- |
| `pack` | comando para gerar o pacote do template na pasta `package` | `dotnet pack -o ./package` | 
| `install` | comando para instalar o template gerado | `dotnet new install {NOME_PASTA/NOME_PACKAGE}` | 
| `gs-api` | comando customizado que cria o projeto baseado no template | `dotnet new gs-api -o {NOME_PROJETO}` | 
| `uninstall` | comando que lista os templates instalados | `dotnet new uninstall` | 
| `uninstall` | comando que remove o template instalado | `dotnet new uninstall {NOME_PACKAGE}` | 