
# 🐾 Vet App para Freelancers

[![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet?logo=dotnet)](https://dotnet.microsoft.com/)
[![MAUI](https://img.shields.io/badge/MAUI-Mobile%20App-blue?logo=microsoft)](https://learn.microsoft.com/dotnet/maui/)
[![License](https://img.shields.io/github/license/mathsantanax/Vet-App-Para-Freelancers)](LICENSE)

## ✨ Visão Geral

**Vet App para Freelancers** é um aplicativo mobile construído com .NET MAUI, voltado para **veterinários autônomos**. Ele oferece recursos completos para cadastro e gerenciamento de clientes (tutores), pets, agendamentos de serviços e controle de vacinas com notificações.

---

## 📁 Estrutura do Projeto

O projeto está todo estruturado dentro da pasta `Application`, e segue o padrão **MVVM** com uso de pastas específicas para:

- `Models`: classes de dados como `Pet`, `Tutor`, `Especies`, `Raças` etc.
- `ViewModels`: lógica de apresentação e binding com as views.
- `Views`: páginas de interface com o usuário.
- `Data`: configuração do banco SQLite e classe `VetAppDbContext`.
- `DataAccess`: repositórios para acesso aos dados (ex: `PetRepository`, `ClientRepository`).
- `Interface`: interfaces para os repositórios.
- `Notification`: serviço de notificação local de vacinas.
- `Helpers`: classes auxiliares como conversores e enums.
- `Platforms`, `Resources`, `Properties`: pastas padrão MAUI.

---

## 📱 Funcionalidades

- 🐾 Cadastro de pets com espécie e raça
- 👨‍👩‍👧 Cadastro de tutores
- 📅 Agendamento de serviços
- 💉 Controle e notificações de vacinas (2 dias antes)
- 🔎 Busca e filtros
- 📲 Interface moderna com navegação Shell

---

## 🛠️ Tecnologias Utilizadas

- .NET MAUI (Multiplataforma)
- MVVM Toolkit (CommunityToolkit.Mvvm)
- SQLite via EF Core
- MAUI Shell para navegação
- Injeção de dependência via `MauiProgram.cs`
- Notificações locais com `NotificationService`

---

## 🚀 Como Executar Localmente

### Pré-requisitos

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) com suporte ao MAUI

### Passos

```bash
git clone https://github.com/mathsantanax/Vet-App-Para-Freelancers.git
cd Vet-App-Para-Freelancers
dotnet restore
dotnet build
dotnet maui run
```

---

## 📌 Roadmap

- [x] Cadastro de clientes e pets
- [x] Agendamentos com calendário
- [x] Controle de vacinas
- [x] Notificações automáticas

---

## 🤝 Contribuições

Contribuições são bem-vindas!

1. Fork este repositório
2. Crie uma branch: `git checkout -b feature/nova-feature`
3. Commit suas mudanças: `git commit -m 'feat: nova funcionalidade'`
4. Push: `git push origin feature/nova-feature`
5. Abra um Pull Request

---

## 📄 Licença

Distribuído sob licença MIT. Veja `LICENSE.txt` para mais detalhes.

---

## 👨‍💻 Autor

Desenvolvido por [Matheus Santana](https://github.com/mathsantanax)
