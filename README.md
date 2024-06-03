# ImproveU

## Descrição

O objetivo do projeto ImproveU é viabilizar a mudança de estilo de vida para pessoas que desejam uma rotina mais saudável acompanhada da prática de exercícios físicos, ou até para quem já possui esse estilo de vida e deseja visualizar a sua evolução de maneira mais acessível com gráficos e elevando o nível do seu treino, utilizando o treino personalizado montado por um profissional de educação física.

## Tecnologias

- .NET 8.0 (AspNet core)
- Entity Framework Core
- Docker Desktop (Windows) ou Docker Engine (Linux)
- PostgreSQL
- PgAdmin

## Instalação

- Instale o .NET 8.0 (AspNet core) através do link: https://dotnet.microsoft.com/download/dotnet/8.0
- Instale o Docker através do link: https://docs.docker.com/get-docker/
- Instale o Entity Framework Core rodando o código a seguir através do Windows PowerShell: `dotnet tool install --global dotnet-ef`

## Configuração

- Clone o repositório através do repositório, através do endereço: `https://github.com/darccelio/improveu_backend_aspnet_efcore_postgres` 


## Execução

- Inicie o serviço Docker:
	- Sistema Operacional Windows: abra a ferramenta Docker Desktop e verifique se o serviço está rodando (normalmente o serviço inicia-se com a abertura do Docker Desktop)
	- Sistema Operacional Linux: abra o terminal e execute o comando `sudo systemctl start docker` para iniciar o serviço Docker.

	- Abra o terminal e navague até o diretório do projeto onde está o arquivo `docker-compose.yml` e execute o comando `docker compose up`  para iniciar o serviço do banco de dados Postgres. Se rodar o comando acrescentando a flag `-d` no final, o serviço será iniciado em segundo plano, sem apresentar os logs no terminal (exemplo: `docker compose up -d`)

É possível executar o projeto através do Visual Studio 2022, Visual Studio Code ou através do terminal/Windows PowerShell. Abaixo estão os passos para executar o projeto conforme cada opção de ferramenta:
	
	- Acesse a pasta do projeto através do terminal/Windows PowerShell.
		- Execute o comando `dotnet restore` para atualizar as dependências e bibliotecas do projeto.
		- Execute o comando `dotnet build` para compilar o projeto.		
		- Execute o comando `dotnet ef database update` para criar o banco de dados e aplicar as migrações.
		- Execute o comando `dotnet run` para executar a aplicação.

	- Abra o projeto no Visual Studio 2022, Visual Studio Code ou através do terminal/Windows PowerShell:
		- Para abrir o projeto no Visual Studio 2022, clique no arquivo `ImproveU.sln` e aguarde a abertura do projeto.
		- Para abrir o proejto no Visual Studio Code, abra o terminal na pasta do projeto e execute o comando `code .` para abrir o projeto.
			- Execute o comando `dotnet ef database update` para criar o banco de dados e aplicar as migrações.
			- Execute o comando `dotnet run` para executar a aplicação.
				
- Acesse a aplicação através do link: http://localhost:5001/index.html ou  https://localhost:5000/index.html para testar os endpoints da API através do SwaggerUI.

- Para executar consultas sql diretamente no banco de dados conforme as opções abaixo:
	1) Acesse o PgAdmin através do link http://localhost:5004/login?next=/browser/ para realizar consultas (psql) no banco de dados com as informações:
		- Preencha o login: 
			- Email: admin@gmail.com
			- Senha: admin																					
		- Clique com o botão direito do mouse em `Servers` e em seguida, clique em `Register` e em `Server` e preencha com as seguintes informações:
			- Aba General -> Name: nome para a conexão, conforme desejar;
			- Aba Connection -> Host name/address: `improveu-database`, Port: `5432`,  Username: `improveu`, Password: `improveu`, Maintenance database: `improveudb`;

		Obs.: esse procedimento estará acessando informações disponíveis dentro do container.
	
	2) Acesse o gerenciador de Banco de dados de sua preferencia e utilize as informações:
		- Host: `localhost`, Port: `5432`,  Username: `improveu`, Password: `improveu`, Database: `improvedb` 
		
		Obs.: esse procedimento estará acessando informações disponíveis no volume-db disponível na raiz do projeto.
	
