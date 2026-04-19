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

# 📸 Application Gallery

Explore the complete visual ecosystem of the Bookkeeping application. The gallery showcases our sophisticated UI, covering complex CRUD workflows for both data records and their respective categories across Light and Dark themes.

<details>
  <summary><h3 style="font-weight: bold;">&nbsp;🔐 1. Authentication & Onboarding (Fixed Theme)</h3></summary>
  <br/>
  <p><i>The authentication layer uses a dedicated, high-focus layout.</i></p>
  <table width="100%">
  <tr><td width="50%" align="center"><b>Login Page</b></td><td width="50%" align="center"><b>Registration Page</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/auth/login.png" width="100%" alt="Login"/></td><td align="center"><img src="docs/imgs/screenshots/auth/register.png" width="100%" alt="Register"/></td></tr>
  </table>
</details>

<details>
  <summary><h3 style="font-weight: bold;">&nbsp;☀️ 2. Light Theme Experience</h3></summary>
  <br/>
  
  <details style="margin: 0 0 5px 25px;">
    <summary><b style="font-size: medium;">&nbsp;📊 Dashboard / Home Page</b></summary>
    <br/>
    <img src="docs/imgs/screenshots/light/home/home.png" width="100%" alt="Dashboard Light"/>
  </details>

  <details style="margin: 0 0 5px 25px;">
    <summary><b  style="font-size: medium;">&nbsp;👤 User Profile</b></summary>
    <br/>
    <table width="100%">
    <tr><td width="50%" align="center"><b>Profile View</b></td><td width="50%" align="center"><b>Edit Profile</b></td></tr>
    <tr><td align="center"><img src="docs/imgs/screenshots/light/profile/details.png" width="100%" alt="Profile View"/></td><td align="center"><img src="docs/imgs/screenshots/light/profile/edit.png" width="100%" alt="Edit Profile"/></td></tr>
    </table>
  </details>

  <details style="margin: 0 0 5px 25px;">
    <summary><b style="font-size: medium;">&nbsp;🗂️ IFRS Accounts (МСФО) — 9 Screen Workflow</b></summary>
    <br/>
    <p align="center"><b>Unified Main Table</b></p>
    <img src="docs/imgs/screenshots/light/ifrs/main-list.png" width="100%" alt="IFRS Main"/>
    <br/><br/>
    <h4>🔹 Account Management</h4>
    <table width="100%">
    <tr><td width="25%" align="center"><b>Details</b></td><td width="25%" align="center"><b>Create</b></td><td width="25%" align="center"><b>Edit</b></td><td width="25%" align="center"><b>Delete</b></td></tr>
    <tr><td align="center"><img src="docs/imgs/screenshots/light/ifrs/acc-details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/ifrs/acc-create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/ifrs/acc-edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/ifrs/acc-delete.png" width="100%"/></td></tr>
    </table>
    <h4>🔹 Category Management</h4>
    <table width="100%">
    <tr><td width="25%" align="center"><b>Details</b></td><td width="25%" align="center"><b>Create</b></td><td width="25%" align="center"><b>Edit</b></td><td width="25%" align="center"><b>Delete</b></td></tr>
    <tr><td align="center"><img src="docs/imgs/screenshots/light/ifrs/cat-details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/ifrs/cat-create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/ifrs/cat-edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/ifrs/cat-delete.png" width="100%"/></td></tr>
    </table>
  </details>

  <details style="margin: 0 0 5px 25px;">
    <summary><b style="font-size: medium;">&nbsp;📋 Reference Books (Справочники) — 9 Screen Workflow</b></summary>
    <br/>
    <p align="center"><b>Unified Main Table</b></p>
    <img src="docs/imgs/screenshots/light/reference/main-list.png" width="100%" alt="Reference Main"/>
    <br/><br/>
    <h4>🔹 Reference Item Management</h4>
    <table width="100%">
    <tr><td width="25%" align="center"><b>Details</b></td><td width="25%" align="center"><b>Create</b></td><td width="25%" align="center"><b>Edit</b></td><td width="25%" align="center"><b>Delete</b></td></tr>
    <tr><td align="center"><img src="docs/imgs/screenshots/light/reference/ref-details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/reference/ref-create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/reference/ref-edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/reference/ref-delete.png" width="100%"/></td></tr>
    </table>
    <h4>🔹 Category Management</h4>
    <table width="100%">
    <tr><td width="25%" align="center"><b>Details</b></td><td width="25%" align="center"><b>Create</b></td><td width="25%" align="center"><b>Edit</b></td><td width="25%" align="center"><b>Delete</b></td></tr>
    <tr><td align="center"><img src="docs/imgs/screenshots/light/reference/cat-details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/reference/cat-create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/reference/cat-edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/reference/cat-delete.png" width="100%"/></td></tr>
    </table>
  </details>

  <details style="margin: 0 0 5px 25px;">
    <summary><b style="font-size: medium;">&nbsp;💰 Cash Receipt Orders (ПКО)</b></summary>
    <br/>
    <img src="docs/imgs/screenshots/light/orders/list.png" width="100%" alt="Orders List"/>
    <table width="100%">
    <tr><td width="25%" align="center"><b>Details</b></td><td width="25%" align="center"><b>Create</b></td><td width="25%" align="center"><b>Edit</b></td><td width="25%" align="center"><b>Delete</b></td></tr>
    <tr><td align="center"><img src="docs/imgs/screenshots/light/orders/details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/orders/create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/orders/edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/orders/delete.png" width="100%"/></td></tr>
    </table>
  </details>

  <details style="margin: 0 0 5px 25px;">
    <summary><b style="font-size: medium;">&nbsp;📈 Income Articles (Статьи доходов)</b></summary>
    <br/>
    <img src="docs/imgs/screenshots/light/income/list.png" width="100%" alt="Income List"/>
    <table width="100%">
    <tr><td width="25%" align="center"><b>Details</b></td><td width="25%" align="center"><b>Create</b></td><td width="25%" align="center"><b>Edit</b></td><td width="25%" align="center"><b>Delete</b></td></tr>
    <tr><td align="center"><img src="docs/imgs/screenshots/light/income/details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/income/create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/income/edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/income/delete.png" width="100%"/></td></tr>
    </table>
  </details>

  <details style="margin: 0 0 5px 25px;">
    <summary><b style="font-size: medium;">&nbsp;⚖️ VAT Management (НДС)</b></summary>
    <br/>
    <img src="docs/imgs/screenshots/light/vat/list.png" width="100%" alt="VAT List"/>
    <table width="100%">
    <tr><td width="25%" align="center"><b>Details</b></td><td width="25%" align="center"><b>Create</b></td><td width="25%" align="center"><b>Edit</b></td><td width="25%" align="center"><b>Delete</b></td></tr>
    <tr><td align="center"><img src="docs/imgs/screenshots/light/vat/details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/vat/create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/vat/edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/vat/delete.png" width="100%"/></td></tr>
    </table>
  </details>
</details>

<details>
  <summary><h3 style="font-weight: bold;">&nbsp;🌙 3. Dark Theme Experience</h3></summary>
  <br/>
  
  <details style="margin: 0 0 5px 25px;">
    <summary><b style="font-size: medium;">&nbsp;📊 Dashboard / Home Page</b></summary>
    <br/>
    <img src="docs/imgs/screenshots/dark/home/home.png" width="100%" alt="Dashboard Dark"/>
  </details>

  <details style="margin: 0 0 5px 25px;">
    <summary><b style="font-size: medium;">&nbsp;👤 User Profile</b></summary>
    <br/>
    <table width="100%">
    <tr><td width="50%" align="center"><b>Profile View</b></td><td width="50%" align="center"><b>Edit Profile</b></td></tr>
    <tr><td align="center"><img src="docs/imgs/screenshots/dark/profile/details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/profile/edit.png" width="100%"/></td></tr>
    </table>
  </details>

  <details style="margin: 0 0 5px 25px;">
    <summary><b style="font-size: medium;">&nbsp;🗂️ IFRS Accounts (МСФО) — 9 Screen Workflow</b></summary>
    <br/>
    <p align="center"><b>Unified Main Table</b></p>
    <img src="docs/imgs/screenshots/dark/ifrs/main-list.png" width="100%" alt="IFRS Main Dark"/>
    <br/><br/>
    <h4>🔹 Account Management</h4>
    <table width="100%">
    <tr><td width="25%" align="center"><b>Details</b></td><td width="25%" align="center"><b>Create</b></td><td width="25%" align="center"><b>Edit</b></td><td width="25%" align="center"><b>Delete</b></td></tr>
    <tr><td align="center"><img src="docs/imgs/screenshots/dark/ifrs/acc-details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/ifrs/acc-create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/ifrs/acc-edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/ifrs/acc-delete.png" width="100%"/></td></tr>
    </table>
    <h4>🔹 Category Management</h4>
    <table width="100%">
    <tr><td width="25%" align="center"><b>Details</b></td><td width="25%" align="center"><b>Create</b></td><td width="25%" align="center"><b>Edit</b></td><td width="25%" align="center"><b>Delete</b></td></tr>
    <tr><td align="center"><img src="docs/imgs/screenshots/dark/ifrs/cat-details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/ifrs/cat-create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/ifrs/cat-edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/ifrs/cat-delete.png" width="100%"/></td></tr>
    </table>
  </details>

  <details style="margin: 0 0 5px 25px;">
    <summary><b style="font-size: medium;">&nbsp;📋 Reference Books (Справочники) — 9 Screen Workflow</b></summary>
    <br/>
    <p align="center"><b>Unified Main Table</b></p>
    <img src="docs/imgs/screenshots/dark/reference/main-list.png" width="100%" alt="Reference Main Dark"/>
    <br/><br/>
    <h4>🔹 Reference Item Management</h4>
    <table width="100%">
    <tr><td width="25%" align="center"><b>Details</b></td><td width="25%" align="center"><b>Create</b></td><td width="25%" align="center"><b>Edit</b></td><td width="25%" align="center"><b>Delete</b></td></tr>
    <tr><td align="center"><img src="docs/imgs/screenshots/dark/reference/ref-details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/reference/ref-create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/reference/ref-edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/reference/ref-delete.png" width="100%"/></td></tr>
    </table>
    <h4>🔹 Category Management</h4>
    <table width="100%">
    <tr><td width="25%" align="center"><b>Details</b></td><td width="25%" align="center"><b>Create</b></td><td width="25%" align="center"><b>Edit</b></td><td width="25%" align="center"><b>Delete</b></td></tr>
    <tr><td align="center"><img src="docs/imgs/screenshots/dark/reference/cat-details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/reference/cat-create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/reference/cat-edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/reference/cat-delete.png" width="100%"/></td></tr>
    </table>
  </details>

  <details style="margin: 0 0 5px 25px;">
    <summary><b style="font-size: medium;">&nbsp;💰 Cash Receipt Orders (ПКО)</b></summary>
    <br/>
    <img src="docs/imgs/screenshots/dark/orders/list.png" width="100%" alt="Orders List Dark"/>
    <table width="100%">
    <tr><td width="25%" align="center"><b>Details</b></td><td width="25%" align="center"><b>Create</b></td><td width="25%" align="center"><b>Edit</b></td><td width="25%" align="center"><b>Delete</b></td></tr>
    <tr><td align="center"><img src="docs/imgs/screenshots/dark/orders/details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/orders/create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/orders/edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/orders/delete.png" width="100%"/></td></tr>
    </table>
  </details>

  <details style="margin: 0 0 5px 25px;">
    <summary><b style="font-size: medium;">&nbsp;📈 Income Articles (Статьи доходов)</b></summary>
    <br/>
    <img src="docs/imgs/screenshots/dark/income/list.png" width="100%" alt="Income List Dark"/>
    <table width="100%">
    <tr><td width="25%" align="center"><b>Details</b></td><td width="25%" align="center"><b>Create</b></td><td width="25%" align="center"><b>Edit</b></td><td width="25%" align="center"><b>Delete</b></td></tr>
    <tr><td align="center"><img src="docs/imgs/screenshots/dark/income/details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/income/create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/income/edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/income/delete.png" width="100%"/></td></tr>
    </table>
  </details>

  <details style="margin: 0 0 5px 25px;">
    <summary><b style="font-size: medium;">&nbsp;⚖️ VAT Management (НДС)</b></summary>
    <br/>
    <img src="docs/imgs/screenshots/dark/vat/list.png" width="100%" alt="VAT List Dark"/>
    <table width="100%">
    <tr><td width="25%" align="center"><b>Details</b></td><td width="25%" align="center"><b>Create</b></td><td width="25%" align="center"><b>Edit</b></td><td width="25%" align="center"><b>Delete</b></td></tr>
    <tr><td align="center"><img src="docs/imgs/screenshots/dark/vat/details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/vat/create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/vat/edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/vat/delete.png" width="100%"/></td></tr>
    </table>
  </details>
</details>

---

## 🤝 Contributing

Contributions are welcome! If you have suggestions or want to contribute, please feel free to open issues or pull requests.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE.txt) file for details.

---
