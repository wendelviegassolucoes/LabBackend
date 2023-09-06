# LabBackend

> Esse laboratório foi inspirado no Rinha-Back-End, fiz uma versão mais simples somente para ilustrar os conhecimentos necessários/aplicados no desafio.
> Link para o desafio original: https://github.com/zanfranceschi/rinha-de-backend-2023-q3

## 💻 Pré-requisitos

Antes de começar, verifique se você atendeu aos seguintes requisitos:

* Docker.
* Caso queira ver/debuggar/compilar o código, Visual Studio ou VsCode

## ☕ Usando LabBackend

Para usar LabBackend, siga estas etapas:

1. Tenha o Docker na sua máquina (Ex: Docker Desktop)
2. Dê build na imagem dos servidores asp-net com o comando: docker build -t api:v2.0 .
	2.1 Caso o seu banco de dados Mongo precise de credenciais, passar via args no build com o usuario e senha que preferir Ex: docker build -t api:v2.0 --build-arg MONGO_USER=USER --build-arg MONGO_PASSWORD=PASSWORD .
	2.2 Para alterar a string de conexão do banco (caso necessário) está em MongoDbContext no Projeto Mongo.Repository
3. Suba os containers via docker-compose.yml com o comando: docker-compose up -d
4. Os Endpoints são
4.1. http://localhost:9999/api/Empresa/GetCountEnterprises - GET pega a contagem das empresas
4.2. http://localhost:9999/api/Empresa/SearchEnterprise?t=Cantrell - GET passando como param t=ValorPesquisa
4.3. http://localhost:9999/api/Empresa/GetEnterpriseById?id=64f7fde654716d91d9fb8257 - GET passando como para id=IdEmpresa
4.4. http://localhost:9999/api/Empresa/GetEnterprises - GET pega todas as empresas cadastradas
5. Para testes de carga está indo junto um script requests.py que já gera a inserção, sendo necessário executar o script python e adequar conforme a necessidade.

## 📫 Contribuindo para LabBackend

Para contribuir com <nome_do_projeto>, siga estas etapas:

1. Bifurque este repositório.
2. Crie um branch: `git checkout -b <nome_branch>`.
3. Faça suas alterações e confirme-as: `git commit -m '<mensagem_commit>'`
4. Envie para o branch original: `git push origin <nome_do_projeto> / <local>`
5. Crie a solicitação de pull.

Como alternativa, consulte a documentação do GitHub em [como criar uma solicitação pull](https://help.github.com/en/github/collaborating-with-issues-and-pull-requests/creating-a-pull-request).
