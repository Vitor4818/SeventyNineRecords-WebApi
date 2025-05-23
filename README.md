# SeventyNineRecords-WebApi
SeventyNineRecords-WebApi é uma API RESTful desenvolvida com ASP.NET Core para gerenciar informações de bandas, álbuns e músicas. Através dessa API é possível criar, ler, atualizar e deletar dados de bandas, seus álbuns e faixas musicais, integrando banco de dados Oracle e documentação automática via Swagger.
O nome SeventyNineRecords é uma homenagem à banda The Smashing Pumpkins e à música 1979, que influenciaram a identidade do projeto, unindo tecnologia e paixão pela música.

## 🎯 Objetivo Geral
O objetivo principal desta API é oferecer um backend organizado para o gerenciamento de bandas, álbuns e músicas. Por meio de endpoints RESTful, permite realizar operações CRUD (Create, Read, Update, Delete) sobre esses recursos musicais, facilitando o desenvolvimento de aplicações voltadas a discografias ou registros de músicas.

## 🔗 Estrutura de Relacionamentos
No modelo de dados adotado, usamos relacionamentos 1:N (um-para-muitos) para estruturar as entidades. Relações desse tipo significam que uma entidade principal está associada a várias entidades dependentes Assim, temos:
- Uma banda pode ter vários álbuns (relação 1:N).
- Um álbum pode conter várias músicas (relação 1:N).
- Cada música pertence a exatamente uma banda e a um álbum específico.
Isso reflete que, por exemplo, ao cadastrar uma banda, pode-se associar vários álbuns a ela, e cada álbum, por sua vez, possui várias músicas. Cada música armazena referências para sua banda e seu álbum correspondentes.

## 🏗 Estrutura do Projeto
A arquitetura do SeventyNineRecords-WebApi segue um padrão em camadas. Cada camada tem responsabilidade bem definida, evitando que a camada API acesse diretamente o banco, por exemplo. Na prática:
- Camada API (Controllers) – Recebe e trata requisições HTTP, mapeando endpoints REST. As Controllers apenas orquestram chamadas à camada de negócio, sem lógica complexa.
- Camada Business (Serviços) – Contém as regras de negócio da aplicação (validações, lógica de domínio). Essa camada executa operações como criar banda/álbum/música ou aplicar regras antes de salvar dados.
- Camada Data (Infraestrutura) – Responsável pelo acesso a dados via Entity Framework Core. Aqui ficam o DbContext do EF Core para persistir as entidades no Oracle.
- Camada Model (Entidades) – Define as classes de modelo (Band, Album, Song) que representam as tabelas do banco. Os modelos incluem atributos e relações (propriedades de navegação) que mapeiam o relacionamento entre bandas, álbuns e músicas.
## 📁 Estrutura do Projeto

| Projeto            | Descrição |
|--------------------|-----------|
| `SeventyNineApi`     | API Web com os endpoints (`Controllers`) |
| `SeventyBusiness` | Camada de lógica de negócio |
| `SeventyData`     | Camada de acesso a dados com EF Core + Oracle |
| `SeventyModel`    | Contém os modelos: `Album`, `Song`, `Band` |

Esse isolamento segue boas práticas de design: a camada de API interage apenas com a camada Business, que por sua vez usa a camada Data. Assim, a manutenção e testes ficam mais fáceis, pois cada camada é independente.

## 🛠 Tecnologias Utilizadas
- ASP.NET Core (.NET 8) – Framework principal para construir a API web.
- Entity Framework Core – ORM utilizado para mapear classes C# para tabelas no Oracle. O EF Core permite o uso de vários bancos via providers
- Oracle Database – Banco de dados relacional (Oracle SQL) usado em produção. É acessado via o provedor Oracle.EntityFrameworkCore (para Oracle 11.2+), conforme listado na documentação do EF Core
- Swagger / OpenAPI (via Swashbuckle) – Ferramenta para gerar documentação interativa da API. O Swagger cria uma especificação OpenAPI que é usada para a UI. Swagger UI oferece uma interface web amigável para testar e explorar os endpoints

## 🚀 Como Executar Localmente
Para rodar o projeto na sua máquina, siga os passos:

1. Clonar o repositório:
```bash
git clone https://github.com/vitor4818/SeventyNineRecords-WebApi.git
cd SeventyNineRecords-WebApi
```

2. Criar a rede docker:
   ```
   ndocker network create cp3-montClio
   ```
   
4. Subir o container do Oracle:
```
docker run -d --name oracle-database --network cp3-montClio \
-p 1521:1521 -p 8080:8080 \
-e ORACLE_PASSWORD='F7uLw9kZ!mXv' \
-e ORACLE_DATABASE=ORCL \
-e APP_USER=appuser_n73X \
-e APP_USER_PASSWORD='F7uLw9kZ!mXv' \
-v oracle-database:/opt/oracle/oradata \
gvenzl/oracle-xe
```

4. Realizar o build da imagem da aplicação
```
docker build -t fiap/seventyninerecords-api:1.0 .   
```

6. Rodar o container da aplicação:
```bash
docker run -d -p 5000:8080 --name SeventyNineAPIContainer  --network cp3-montClio -e ConnectionStrings__DefaultConnection="User Id=appuser_n73X;Password=F7uLw9kZ!mXv;Data Source=oracle-database:1521/XEPDB1" fiap/seventyninerecords-api:1.0
```
A API será iniciada

## Testando a API
Para testar a aplicação, utilize os arquivos .http disponíveis no projeto ou ferramentas como o Thunder Client ou Postman.# Teste com Endpoint banda

# Testando o Endpoint de Banda
Abaixo estão os prints de cada operação realizada via endpoints:


- **POST /bandas** → Criar banda  
![image](https://github.com/user-attachments/assets/485d4803-bc47-4d0c-8ca5-5b5fe0c4d4ac)

- **GET /bandas** → Listar todas  
![image](https://github.com/user-attachments/assets/12d3f4b5-24f2-4a24-98b7-bd8bad425717)

- **GET /bandas/{id}** → Buscar por ID  
![image](https://github.com/user-attachments/assets/6fbd8c02-b333-4621-a2c5-a0d8e266627f)

- **PUT /bandas/{id}** → Atualizar  
![image](https://github.com/user-attachments/assets/973e800b-b4e1-41b8-9754-0a1e32b10200)

- **DELETE /bandas/{id}** → Deletar  
![image](https://github.com/user-attachments/assets/226db3ee-cca1-4241-b659-32a6d9e083c6)



##  📚 Documentação (Swagger)

Após iniciar a API, a documentação interativa do Swagger estará disponível em:

- [https://localhost:5001/swagger](https://localhost:5001/swagger)
- [http://localhost:5000/swagger](http://localhost:5000/swagger)

Essa interface web permite explorar todos os endpoints, visualizar modelos de dados e testar chamadas diretamente no navegador.  
O Swagger UI carrega a especificação **OpenAPI** gerada automaticamente pelo projeto, exibindo cada recurso e seus verbos HTTP de forma amigável.
