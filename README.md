
# ApiFinanceiro

## Descrição

O **ApiFinanceiro** é uma API desenvolvida em ASP.NET Core que permite o gerenciamento de transações financeiras, incluindo operações de entrada e saída de valores, categorização e busca por data e tipo.

## Tecnologias Utilizadas

- **ASP.NET Core**: Framework para construção de aplicações web.
- **Entity Framework Core**: ORM para interagir com o banco de dados.
- **SQL Server**: Banco de dados relacional.
- **Swagger**: Documentação da API.

## Pré-requisitos

- [.NET 6 ou superior](https://dotnet.microsoft.com/download/dotnet)
- SQL Server (pode ser o SQL Server Express)
- [Visual Studio](https://visualstudio.microsoft.com/) ou qualquer editor de código de sua preferência.

## Instalação

1. **Clone o repositório**

   ```bash
   git clone https://github.com/seu_usuario/ApiFinanceiro.git
   cd ApiFinanceiro
   ```

2. **Instale as dependências**

   Execute o seguinte comando para instalar os pacotes necessários:

   ```bash
   dotnet restore
   ```

3. **Configurar a String de Conexão**

   No arquivo `appsettings.json`, configure a string de conexão para o seu banco de dados SQL Server:

   ```json
   "ConnectionStrings": {
       "SqlString": "Server=seu_servidor;Database=seu_banco_de_dados;Trusted_Connection=True;"
   }
   ```

4. **Criar a Migração e Atualizar o Banco de Dados**

   Após fazer as alterações no modelo, crie uma nova migração:

   ```bash
   dotnet ef migrations add NomeDaMigracao
   ```

   Atualize o banco de dados com a nova migração:

   ```bash
   dotnet ef database update
   ```

## Uso

### Endpoints

- **Criar uma Transação**
  
  `POST /Financeiro`

  Body:
  ```json
  {
      "Date": "2024-10-04",
      "Valor": 100.00,
      "Descricao": "Descrição da transação",
      "Tipo": "Entrada",
      "Categoria": "Casa"
  }
  ```

- **Buscar por Tipo**
  
  `GET /Financeiro/{Tipo}`

- **Atualizar uma Transação**

  `PUT /Financeiro/{id}`

  Body:
  ```json
  {
      "Date": "2024-10-04",
      "Valor": 150.00,
      "Descricao": "Descrição atualizada",
      "Tipo": "Saida",
      "Categoria": "Alimentação"
  }
  ```

- **Excluir uma Transação**
  
  `DELETE /Financeiro/{id}`

### Testando a API

Você pode testar a API usando ferramentas como [Postman](https://www.postman.com/) ou [Insomnia](https://insomnia.rest/).

## Documentação da API

A documentação da API pode ser acessada através do Swagger. Ao executar a aplicação, abra o navegador e acesse:

```
http://localhost:5000/swagger
```

## Contribuição

Contribuições são bem-vindas! Se você tiver sugestões, correções ou melhorias, sinta-se à vontade para criar um pull request.

