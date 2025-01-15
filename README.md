
# Sistema Back-end para web de cursos online - Web Api ( CSHARP# )

## Sobre o Projeto - API

Desenvolver uma api web, na linguagem c#, para algum sistema de minha escolha, a ideia é que teremos dois atores que vão interagir com um front-end basico para um site de cursos online
</br>

### Critérios do Projeto

- Requisitos:
- Versionamento
• Organização do Código
• Modelo de arquitetura de pastas.
• Utilizar principios do DDD
• Usar Xunit ou Nunit para testes unitários
• Utilizar Dapper para acesso ao banco de dados
• .NET 8.0 ou superior
• Integração com React VITE
Bônus (Extra):
• Usar autenticação do tipo jwt, ou basic
• Utilizar Mensageria com RabbitMq ou outro (Opcional)

### Abaixo está a estrutura do diretório do projeto:

```bash
/src
├── Sistema-CursosOnline               # Solução do tipo web Api Asp.NETCore
├── Sistema-CursosOnline/              # Pasta raiz do código fonte
│   ├── Application/                   # Pasta de componentes de aplicação do projeto
│   │  ├── DTO/                        # Arquivos de DataAcessObject
│   │  │  │   ├──AssessmentDTO         # Arquivos de DataAcessObject
│   │  │  │   ├──CourseDTO             # Arquivos de DataAcessObject
│   │  │  │   ├──EnrollmentDTO         # Arquivos de DataAcessObject
│   │  │  │   ├──ModuleDTO             # Arquivos de DataAcessObject
│   │  │  │   ├──UserDTO               # Arquivos de DataAcessObject
│   │  ├── Messaging/                  # Pasta de configuração do RabbitMq
│   │  │  │   ├──RabbitMqConfig        # Arquivo de config do RabbitMq
│   │  ├── Request/                    # Requests de autenticação da api
│   │  │  │   ├──LoginRequest          # Arquivo request de login
│   │  │  │   ├──RegisterRequest       # Arquivo request de registro
│   │  ├── ServicesApp/                # Pasta de arquivos com os serviços/métodos da aplicação
│   │  │  │   ├──AssessmentService     # Arquivos de Service
│   │  │  │   ├──CourseService         # Arquivos de Service
│   │  │  │   ├──EnrollmentService     # Arquivos de Service
│   │  │  │   ├──ModuleService         # Arquivos de Service
│   │  │  │   ├──UserService           # Arquivos de Service
│   │  ├── Data/                       # Pasta de conexão com o postgres
│   │  │  │   ├──PostgresConnection    # Arquivos de Service
│   ├── Domain/                        # Pasta de arquivos de dominio
│   │  ├── Entities/                   # Pasta de entidades do negócio
│   │  │  │   ├──Assessment            # Arquivos de entidades do negócio
│   │  │  │   ├──Course                # Arquivos de entidades do negócio
│   │  │  │   ├──Enrollment            # Arquivos de entidades do negócio
│   │  │  │   ├──Module                # Arquivos de entidades do negócio
│   │  │  │   ├──User                  # Arquivos de entidades do negócio
│   │  ├── IRepository/                # Pasta de interface/repository contrato de métodos para repositórios
│   │  │  │   ├──IAssessmentRepository # Arquivo de interface/repository
│   │  │  │   ├──ICourseRepository     # Arquivo de interface/repository
│   │  │  │   ├──IEnrollmentRepository # Arquivo de interface/repository
│   │  │  │   ├──IModuleRepository     # Arquivo de interface/repository
│   │  │  │   ├──IUserRepository       # Arquivo de interface/repository
│   │  ├── IRepository/                # Pasta de interface/services contrato de métodos para serviços
│   │  │  │   ├──IAssessmentService    # Arquivo de interface/service
│   │  │  │   ├──ICourseService        # Arquivo de interface/service
│   │  │  │   ├──IEnrollmentService    # Arquivo de interface/service
│   │  │  │   ├──IModuleService        # Arquivo de interface/service
│   │  │  │   ├──IUserService          # Arquivo de interface/service
│   ├── Infrastructure/                # Pasta de arquivos de infrastrutura do dominio
│   │  │  │   ├──IAssessmentRepository # Arquivo de repository/métodos
│   │  │  │   ├──ICourseRepository     # Arquivo de repository/métodos
│   │  │  │   ├──IEnrollmentRepository # Arquivo de repository/métodos
│   │  │  │   ├──IModuleRepository     # Arquivo de repository/métodos
│   │  │  │   ├──IUserRepository       # Arquivo de repository/métodos
│   ├── Controllers/                   # Pasta de arquivos de implemntação dos services/exposição dos endpoints
│   │  │  │   ├──AssessmentController  # Arquivo de controlador
│   │  │  │   ├──CourseController      # Arquivo de controlador
│   │  │  │   ├──EnrollmentController  # Arquivo de controlador
│   │  │  │   ├──ModuleController      # Arquivo de controlador
│   │  │  │   ├──UserController        # Arquivo de controlador
│   ├── Properties/                    # Pasta de propriedades
│   │  │  │   ├──launchSetttings       # Arquivo JSON
│   ├── Program.cs/                    # Arquivo de configuração/injeção de dependencia do programa
│   ├── Sistema-CursosOnline/          # .csproj
│   ├── Sistema-CursosOnline/          # .http
│   ├── Sistema-CursosOnline/          # .sln
│   ├── appsetings.Development/        # .json
│   ├── appsettings/                   # .json
├── Sistema-CursosOnline/              # Pasta raiz de testes unitários Xunit
│   ├── Tests/                         # Arquivo de configuração/injeção de dependencia do programa
│   │  │  │   ├──AssessmentServiceTest # Teste de serviço
│   │  │  │   ├──CourseServiceTest     # Teste de serviço
│   │  │  │   ├──EnrollmentServiceTest # Teste de serviço
│   │  │  │   ├──ModuleServiceTest     # Teste de serviço
│   │  │  │   ├──RabbitMqTest          # Teste de serviço
│   │  │  │   ├──UserServiceTest       # Teste de serviço
│   ├── Sistema-CursosOnline.tests/    # .csproj
├── gitattributes/                     # Atributos git
├── .gitingore/                        # gitignore

```


### Status do projeto: **CONCLUÍDO**

## 📁 Acesso ao projeto

**Você pode acessar e baixar o código fonte do projeto final
[aqui](https://github.com/Cilentoo/Sistema-CursosOnline).**

**Após baixar o projeto, você pode abrir com o Visual Studio Community, IDE utilizada para realizar a programação.**

## Stack utilizada

- .NET 8.0
- XUNIT (Dependency Nuget)
- ASP.NETCORE WEB API
- RabbitMq
- JWT Identity model (Dependency Nuget)
- Brcypt.Net
- Dapper
- Moq
- Npgsql
- RestSharp
- **Ferramentas:**
- Git
- GitHub
- VisualStudioCommunity 2022
