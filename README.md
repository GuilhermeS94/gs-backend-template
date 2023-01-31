# 💻 gs-backend-template 🪞
Projeto modelo para criação de uma API em .Net 7.<br/>
Estrutura contempla uma API, multidiomas, com tratativa de erro global, validacoes de campos de entrada, mensagens informativas, TDD, 
<br/>

## Estrutura do projeto:
- [GS.Backend.Dominios] Representa os dominios de negocio da aplicao, tanto entradas e saidas, quanto os idiomas, excecoes, middlewares, notificacoes, validacoes, processadores e contratos de servicos externos. Aqui ficam os objetos bases para o projeto.
- [GS.Backend.Infra] Configura os servicos que irao compor aplicacao, associacao de command pattern, contratos com servicos externos, idiomas, headers, tudo que deve ser adicionado pela startup do projeto.
- [GS.Backend.Ms] API, projeto o qual tem a classe principal que inicia aplicacao, mas antes adiciona tudo que foi relatado na infra para o funcionamento pleno e saudavel do micro servico.
- [GS.Backend.ServicosExternos] As acoes do negocio, projeto o qual realmente acontece os processamentos.

## 🚀 Usando este projeto como modelo:
 - [Passo 1] Clone do projeto(http://)
 - [Passo 2] Abrir terminal, na pasta raiz do projeto (**cd src**)
 - [Passo 3] Criar pacote do modelo com comando: **dotnet pack -o ./package**
 - [Passo 4] Abrir a pasta que deseja criar o projeto no terminal e instalar o template com comando: **dotnet new --install [CAMINHO COMPLETO DO NUGET GERADO NO PASSO ANTERIOR]** (removendo os colchetes, e com nome do arquivo e extensao)
 - [Passo 5] Criar seu projeto com comando: **dotnet new gsapi -o [NAMESPACE.NOMESEUPROJETO]** (removendo os colchetes)
 - [Passo 6] Faca os renames de `namespace` necessarios com sua preferencia ou de sua empresa