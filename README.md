# 🚗 Sistema de Locadora de Veículos - API

API desenvolvida para o Trabalho Prático (Etapa 2) do curso de **Análise e Desenvolvimento de Sistemas (TADS)** na **PUC Minas**. O sistema gerencia o fluxo de locação de veículos, integrando clientes, frotas e precificação dinâmica por categoria.

## 🚀 Tecnologias Utilizadas
* **Linguagem:** C# 10+
* **Framework:** ASP.NET Core (Template Empty)
* **ORM:** Entity Framework Core 6.0+
* **Banco de Dados:** SQL Server Express
* **Documentação:** Swagger (OpenAPI)

## 🏗️ Arquitetura e Design
O projeto segue uma estrutura de camadas para separação de responsabilidades:
* **Models:** Entidades de domínio (Fabricante, Categoria, Veiculo, Cliente, Aluguel).
* **Data:** Contexto do Entity Framework (`LocadoraContext`) e configurações de Fluent API.
* **Controllers:** Endpoints REST para operações de CRUD e filtros complexos.
* **Migrations:** Versionamento do esquema do banco de dados.

## 📊 Modelo de Dados
O banco de dados foi projetado de forma normalizada com as seguintes relações:
1. **Fabricante (1:N) Veículo**.
2. **Categoria (1:N) Veículo**.
3. **Veículo (1:N) Aluguel**.
4. **Cliente (1:N) Aluguel**.

> **Nota Técnica:** Campos financeiros utilizam `decimal(18,2)` para garantir precisão monetária.

## 🛠️ Desafios Técn
