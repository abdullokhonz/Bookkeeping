# 🌌 Bookkeeping Platform

<p align="center">
  <strong>Next-Generation Enterprise Accounting & ERP Foundation</strong>
</p>

<div align="center">

[![License](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen)](https://github.com/abdullokhonz/Bookkeeping/actions)
[![Contributors](https://img.shields.io/github/contributors/abdullokhonz/Bookkeeping)](https://github.com/abdullokhonz/FoxBerry.API/graphs/contributors)

</div>

<p align="center">
  <img src="https://img.shields.io/badge/.NET_10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 10" />
  <img src="https://img.shields.io/badge/Blazor_Web_App-5C2D91?style=for-the-badge&logo=blazor&logoColor=white" alt="Blazor" />
  <img src="https://img.shields.io/badge/PostgreSQL-336791?style=for-the-badge&logo=postgresql&logoColor=white" alt="PostgreSQL" />
  <img src="https://img.shields.io/badge/CQRS_with_MediatR-222222?style=for-the-badge" alt="CQRS" />
  <img src="https://img.shields.io/badge/Clean_Architecture-00C853?style=for-the-badge" alt="Clean Architecture" />
</p>

---

> **Bookkeeping** is a production-ready, scalable financial management system built on **Clean Architecture** and **CQRS**. Designed as a robust foundation for a full ERP ecosystem, it moves beyond simple CRUD to handle complex business logic, IFRS compliance, and real-time financial tracking.

## ✨ Enterprise-Grade Features

| 💰 Financial Engine | 🌍 IFRS Compliance | 📊 Real-Time Analytics |
| :--- | :--- | :--- |
| • Document lifecycle (Draft ➔ Processed)<br>• Automated sequence numbering<br>• Dynamic VAT calculation | • Tree-based account structures<br>• Hierarchical financial modeling<br>• Multi-region tax ready | • Live dashboard updates<br>• Interactive MudBlazor charts<br>• Instant financial insights |

| 📚 Master Data | 🔐 Advanced Security | 📂 Asset Handling |
| :--- | :--- | :--- |
| • Centralized reference data<br>• Flexible categorization<br>• Smart alphabetical sorting | • JWT Auth & Refresh Tokens<br>• Protected API endpoints<br>• Role-based access control | • Secure file uploads<br>• Document attachment system<br>• Image & media management |

---

## 🏗️ Architecture & CQRS

The platform enforces a strict **Clean Architecture**, ensuring the core domain remains isolated from infrastructure and presentation concerns. State mutations and data retrieval are strictly separated using the **CQRS** pattern via MediatR.

* 🟢 **Commands:** Mutate state containing complex business validations.
* 🔵 **Queries:** Highly optimized read operations returning clean DTOs.
* 🧩 **Handlers:** Isolated business logic ensuring high testability and maintainability.

<details open>
<summary><b>📂 View Project Structure 👇</b></summary>
<br>

```text
📦 Bookkeeping Solution  
├── 📂 Bookkeeping  
│   ├── 📂 Bookkeeping (Main Server / API)  
│   │   ├── 📂 Application (Commands / Queries / Handlers)  
│   │   ├── 📂 Entities (Domain Models)  
│   │   ├── 📂 Controllers (API Endpoints)  
│   │   ├── 📂 Infrastructure (EF Core, Auth, Repositories)  
│   │   ├── 📂 Services (Business logic implementations)  
│   │   └── 📂 Components (Blazor Server UI)  
│   │  
│   └── 📂 Bookkeeping.Client (Blazor WASM Frontend)  
│       ├── 📂 Pages (Accounts, Auth, Orders, etc.)  
│       ├── 📂 Providers (Auth State)  
│       └── 📂 Layouts & Dialogs  
│  
└── 📂 Bookkeeping.Contracts (Shared Library)  
    ├── 📂 DTOs & Models  
    ├── 📂 Enums  
    └── 📂 Common (Responses, Results, Pagination) 
```
</details>

---

## 🤝 Contributing

Contributions are welcome! If you have suggestions or want to contribute, please feel free to open issues or pull requests.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE.txt) file for details.

---
