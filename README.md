# ImproveU

## Descrição

O objetivo do projeto ImproveU é viabilizar a mudança de estilo de vida para pessoas que desejam uma rotina mais saudável acompanhada da prática de exercícios físicos, ou até para quem já possui esse estilo de vida e deseja visualizar a sua evolução de maneira mais acessível com gráficos e elevando o nível do seu treino, utilizando o treino personalizado montado por um profissional de educação física.


## Instalação

O projeto pode ser rodado através das seguintes ferramentas:

- Clone o repositório;
- Abra o projeto no Visual Studio 2022 ou Visual Studio Code;
- Execute o projeto.
	- execute os comandos pela linha de comando/terminal no diretório do projeto:
	- `dotnet restore`
	-	`dotnet build`
		- `docker compose up` (para rodar o banco de dados)
		- `dotnet ef database update` (aguarde a criação do banco de dados através do log no terminal para poder aplicar as migrações no banco de dados em container)
		- `dotnet run`

- Acesse a aplicação através do link: https://localhost:5000/index.html ou http://localhost:5001/index.html para acessar os endpoints da API.

- Para realizar consultas no banco de dados:- 
	- Acesse o PgAdmin através do link http://localhost:5004/login?next=/browser/ para realizar consultas (psql) no banco de dados com as informações:
		- Preencha o login: 
			- Email: admin@admin.com
			- Senha: admin																					
		- Clique em `Register` e preencha com as seguintes informações:
			- Aba General -> Name: nome para a conexão, conforme desejar;
			- Aba Connection -> Host: ` improveu-databa	se`, Port: `5432`,  Username: `improveu`, Password: `improveu`, Maintenance database: `postgres`;

		Obs.: esse procedimento estará acessando informações disponíveis dentro do container.
	
	- Acesse o gerenciador de Banco de dados de sua preferencia e utilize as informações:
		- Host: `localhost`, Port: `5432`,  Username: `improveu`, Password: `improveu`, Database: `improvedb` 
		
		Obs.: esse procedimento estará acessando informações disponíveis no volume-db disponível na raiz do projeto.
	
