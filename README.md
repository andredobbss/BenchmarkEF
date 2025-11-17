# Benchmark: EF Core vs Dapper — Consultas de Funcionários e Projetos

Este projeto realiza um comparativo detalhado de performance entre diferentes estratégias de acesso a dados utilizando **Entity Framework Core** e **Dapper**, aplicadas em consultas envolvendo Funcionários, Departamentos e Projetos.

O foco principal é medir, com precisão científica, o desempenho de cada abordagem utilizando **BenchmarkDotNet**, com uma base de dados populada por milhares de registros gerados pelo **Bogus**.

---

## 🚀 Tecnologias Utilizadas

| Tecnologia | Finalidade |
|-----------|------------|
| **.NET 8** | API e benchmarks |
| **Entity Framework Core** | LINQ, SQL Raw e Views |
| **Dapper** | Micro ORM focado em performance |
| **Bogus** | Geração de dados fake realistas |
| **BenchmarkDotNet** | Execução de benchmarks com rigor estatístico |
| **SQL Server** | Banco de dados utilizado |

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
