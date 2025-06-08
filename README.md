# Projeto SARA - Sistema de Alerta e Resposta Automática

## 📘 Visão Geral

O **Projeto SARA** (Sistema de Alerta e Resposta Automática) é uma API REST desenvolvida com .NET 6, focada em oferecer uma solução inovadora para auxiliar a população em situações de emergência ambiental, como enchentes, deslizamentos e outros eventos críticos.

Esta aplicação foi desenvolvida como parte da disciplina **Advanced Business Development with .NET**, visando atender a todos os requisitos técnicos exigidos.

---

## ⚙️ Tecnologias Utilizadas

- [.NET 6](https://dotnet.microsoft.com/en-us/)
- [Entity Framework Core (Oracle)](https://docs.microsoft.com/ef/core/providers/oracle/)
- Oracle Database
- Razor Pages + TagHelpers
- Swagger (Swashbuckle)
- LINQ / Migrations / HttpClient

---
## 📡 Endpoints REST

A API expõe endpoints para as seguintes entidades:

- `Usuario`
- `TipoUsuario`
- `Sensor`
- `TipoSensor`
- `Alerta`
- `NivelAlerta`
- `LeituraSensor`
- `Localizacao`
- `Notificacao`
- `StatusNotificacao`

Cada controller expõe rotas padrão REST:
- `GET /api/[entidade]`
- `GET /api/[entidade]/{id}`
- `POST /api/[entidade]`
- `PUT /api/[entidade]/{id}`
- `DELETE /api/[entidade]/{id}`

Além disso, alguns têm rotas customizadas (e.g. busca por email, contagem, etc).

---

## 🚀 Como Rodar o Projeto

### 1. Pré-requisitos
- .NET 6 SDK
- Oracle Database (ou instância de teste)
- Visual Studio 2022 ou superior (ou VS Code)

### 2. Restaurar e Compilar

dotnet restore
dotnet build

dotnet ef database update

dotnet run

---

## 🧪 Testando com Swagger
Acesse: http://localhost:5001/swagger

Você poderá visualizar toda a documentação da API, testar os endpoints e verificar as respostas.

---

## ✅ Requisitos Atendidos

- API RESTful com boas práticas
- Persistência com banco relacional (Oracle)
- Relacionamentos 1:N entre entidades
- Documentação interativa via Swagger
- Implementação de Razor Pages com TagHelpers
- Uso de Migrations com Entity Framework Core

