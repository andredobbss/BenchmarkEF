# Benchmark: EF Core vs Dapper — Consultas de Funcionários e Projetos

Este projeto realiza um comparativo detalhado de performance entre diferentes estratégias de acesso a dados utilizando **Entity Framework Core** e **Dapper**, aplicadas em consultas envolvendo Funcionários, Departamentos e Projetos.

O foco principal é medir, com precisão científica, o desempenho de cada abordagem utilizando **BenchmarkDotNet**, com uma base de dados populada por milhares de registros gerados pelo **Bogus**.

---

## 🚀 Tecnologias Utilizadas

| Tecnologia                              | Finalidade                                                     |
| --------------------------------------- | ---------------------------------------------------------------------------------------- |
| **.NET 8**                              | Plataforma principal da aplicação, APIs e execução dos benchmarks                        |
| **Entity Framework Core**               | ORM para consultas LINQ, SQL Raw, criação do banco e mapeamento ORM                      |
| **Dapper**                              | Micro-ORM de alta performance para leitura e consultas diretas                           |
| **Bogus**                               | Geração de dados fake realistas para popular o banco nos testes                          |
| **BenchmarkDotNet**                     | Framework para execução de benchmarks com precisão estatística                           |
| **SQL Server**                          | Banco relacional utilizado pela aplicação e pelos testes                                 |
| **Z.EntityFramework.Extensions.EFCore** | Extensão do EF Core para *bulk operations* (inserções, atualizações e deleções em massa) |
| **AutoMapper**                          | Mapeamento automático entre entidades e DTOs, usado para simplificar conversões          |

---

## 🎯 Objetivo do Projeto

Comparar o desempenho das seguintes estratégias de leitura:

1. **EF Core LINQ**
2. **EF Core SQL Raw**
3. **EF Core SQL Raw (arquivo .sql)**
4. **EF Core SQL Raw com View**
5. **Dapper**
6. **Dapper (arquivo .sql)**
7. **Dapper com View**

Para cada teste, são medidas:

- Tempo médio de execução (*Mean*)
- Mediana
- Desvio padrão
- Rank
- Alocação de memória
- Geração de lixo (Gen0/Gen1)

---

## 📊 Resultados (Resumo)

Os resultados mostram um padrão consistente:

| Estratégia | Tempo Médio |
|------------|-------------|
| **Dapper** | ~23–24 ms |
| **Dapper (arquivo SQL)** | ~24 ms |
| **Dapper (View)** | ~24 ms |
| **EF SQL Raw** | ~28–30 ms |
| **EF LINQ** | ~28 ms |

📌 **Dapper foi até 20% mais rápido** em todos os cenários.  
📌 EF Core usando **Views** performou melhor que SQL Raw em diversos cenários.  
📌 Todas as implementações EF Core tiveram alocação de memória superior (~6 MB).  
📌 As implementações com Dapper ficaram em ~5.45 MB.

---

## 📁 Estrutura do Projeto

<img src="https://github.com/andredobbss/BenchmarkEF/blob/master/BenchmarkEF.Infraestructure/img/Estrutura.png"/>

---

## 🛢️ DER

<img src="https://github.com/andredobbss/BenchmarkEF/blob/master/BenchmarkEF.Infraestructure/img/DER.png" />

---

## 📈 Exemplo de Benchmark

<img src="https://github.com/andredobbss/BenchmarkEF/blob/master/BenchmarkEF.Infraestructure/img/Benchmark.png"/>

---

## 🧪 Executando o Projeto
- 1 - Defina o nome do banco de dados no arquivo ConnectionStringConfiguration do projeto de infraestrutura.
- 2 - Informe o nome do servidor, o user ID e senha na string do arquivo ConnectionStringConfiguration do projeto de infraestrutura.
- 3 - Crie a connection string como variável de ambiente e informe o nome no método GetEnvironmentVariable do arquivo ConnectionStringConfiguration do projeto de infraestrutura.
```csharp
internal static class ConnectionStringConfiguration
{
    internal const string databaseName = "BenchmarkEF"; // Defina o nome do banco de dados
    internal static string GetConnectionString()
    {
                                                                  // Obtenha a string de conexão da variável de ambiente
        string sqlConnectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION_BENCHMARKEF") ??
          $@"Server = SERVERABC;
             Database = {databaseName}; 
             User ID = abc;
             Password = xxxxxxxx;
             Trusted_Connection = False;
             TrustServerCertificate = True";

        return sqlConnectionString;
    }
}
```
- 4 - Defina o projeto console como projeto de inicialização e em modo Release.  ⚠ Importante: BenchmarkDotNet só roda em Release.
- 5 - Rode.
- 6 - Crie e popule o banco de dados (opção 1).
- 7 - Execute o Benchmark (opção 2).

```shell
==========================================
Benchmark EF Tool
==========================================
1. Criar e popular o banco de dados
2. Executar benchmarks
0. Sair

Selecione uma opção:
```
---

## 📄 Licença
Este projeto está sob a licença MIT.

---
