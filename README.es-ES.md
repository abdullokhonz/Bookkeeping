

# 🌌 Plataforma de Contabilidad

<p align="center">
  <strong>Fundación de Contabilidad y ERP Empresarial de Nueva Generación</strong>
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

> **Bookkeeping** es un sistema de gestión financiera escalable y listo para producción, construido sobre **Clean Architecture** y **CQRS**. Diseñado como una base robusta para un ecosistema ERP completo, va más allá del simple CRUD para manejar lógica de negocio compleja, cumplimiento de IFRS y seguimiento financiero en tiempo real.

## ✨ Características de Nivel Empresarial

| 💰 Motor Financiero | 🌍 Cumplimiento IFRS | 📊 Analítica en Tiempo Real |
| :--- | :--- | :--- |
| • Ciclo de vida de documentos (Borrador ➔ Procesado)<br>• Numeración secuencial automática<br>• Cálculo dinámico del IVA | • Estructuras de cuentas basadas en árbol<br>• Modelado financiero jerárquico<br>• Listo para impuestos multirregionales | • Actualizaciones en vivo del panel<br>• Gráficos interactivos de MudBlazor<br>• Información financiera instantánea |

| 📚 Datos Maestros | 🔐 Seguridad Avanzada | 📂 Gestión de Activos |
| :--- | :--- | :--- |
| • Datos de referencia centralizados<br>• Categorización flexible<br>• Ordenación alfabética inteligente | • Autenticación JWT y Tokens de Refresco<br>• Endpoints de API protegidos<br>• Control de acceso basado en roles | • Cargas de archivos seguras<br>• Sistema de adjuntos de documentos<br>• Gestión de imágenes y medios |

---

## 🏗️ Arquitectura y CQRS

La plataforma aplica una **Clean Architecture** estricta, asegurando que el dominio central permanezca aislado de las preocupaciones de infraestructura y presentación. Las mutaciones de estado y la recuperación de datos están estrictamente separadas utilizando el patrón **CQRS** a través de MediatR.

* 🟢 **Comandos:** Mutan el estado conteniendo validaciones de negocio complejas.
* 🔵 **Consultas:** Operaciones de lectura altamente optimizadas que devuelven DTOs limpios.
* 🧩 **Manejadores:** Lógica de negocio aislada que garantiza alta testabilidad y mantenibilidad.

<details open>
<summary><b>📂 Ver Estructura del Proyecto 👇</b></summary>
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

# 📸 Galería de la Aplicación

Explora el ecosistema visual completo de la aplicación Bookkeeping. La galería muestra nuestra interfaz sofisticada, cubriendo flujos de trabajo CRUD complejos tanto para registros de datos como para sus respectivas categorías en los temas Claro y Oscuro.

<details>
<summary><h3 style="display:inline;">🔐 1. Autenticación y Onboarding (Tema Fijo)</h3></summary>
<br/>
<p><i>La capa de autenticación utiliza un diseño dedicado de alto enfoque.</i></p>
<table width="100%">
  <tr><td width="50%" align="center"><b>Página de Inicio de Sesión</b></td><td width="50%" align="center"><b>Página de Registro</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/auth/login.png" width="100%" alt="Login"/></td><td align="center"><img src="docs/imgs/screenshots/auth/register.png" width="100%" alt="Register"/></td></tr>
</table>
</details>

<details>
<summary><h3 style="display:inline;">☀️ 2. Experiencia en Tema Claro</h3></summary>
<br/>

<details>
<summary><b>📊 Panel / Página de Inicio</b></summary>
<br/>
<p align="center">
  <img src="docs/imgs/screenshots/light/home/home.png" width="80%" alt="Dashboard Light" style="max-width:800px;"/>
</p>
</details>

<details>
<summary><b>👤 Perfil de Usuario</b></summary>
<br/>
<table width="100%">
  <tr><td width="50%" align="center"><b>Vista del Perfil</b></td><td width="50%" align="center"><b>Editar Perfil</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/light/profile/details.png" width="100%" alt="Profile View"/></td><td align="center"><img src="docs/imgs/screenshots/light/profile/edit.png" width="100%" alt="Edit Profile"/></td></tr>
</table>
</details>

<details>
<summary><b>🗂️ Cuentas IFRS (МСФО) — Flujo de 9 Pantallas</b></summary>
<br/>
<p align="center"><b>Tabla Principal Unificada</b></p>
<img src="docs/imgs/screenshots/light/ifrs/main-list.png" width="100%" alt="IFRS Main"/>
<br/><br/>
<h4>🔹 Gestión de Cuentas</h4>
<table width="100%">
  <tr><td width="25%" align="center"><b>Detalles</b></td><td width="25%" align="center"><b>Crear</b></td><td width="25%" align="center"><b>Editar</b></td><td width="25%" align="center"><b>Eliminar</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/light/ifrs/acc-details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/ifrs/acc-create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/ifrs/acc-edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/ifrs/acc-delete.png" width="100%"/></td></tr>
</table>
<h4>🔹 Gestión de Categorías</h4>
<table width="100%">
  <tr><td width="25%" align="center"><b>Detalles</b></td><td width="25%" align="center"><b>Crear</b></td><td width="25%" align="center"><b>Editar</b></td><td width="25%" align="center"><b>Eliminar</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/light/ifrs/cat-details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/ifrs/cat-create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/ifrs/cat-edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/ifrs/cat-delete.png" width="100%"/></td></tr>
</table>
</details>

<details>
<summary><b>📋 Manuales de Referencia (Справочники) — Flujo de 9 Pantallas</b></summary>
<br/>
<p align="center"><b>Tabla Principal Unificada</b></p>
<img src="docs/imgs/screenshots/light/reference/main-list.png" width="100%" alt="Reference Main"/>
<br/><br/>
<h4>🔹 Gestión de Elementos de Referencia</h4>
<table width="100%">
  <tr><td width="25%" align="center"><b>Detalles</b></td><td width="25%" align="center"><b>Crear</b></td><td width="25%" align="center"><b>Editar</b></td><td width="25%" align="center"><b>Eliminar</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/light/reference/ref-details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/reference/ref-create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/reference/ref-edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/reference/ref-delete.png" width="100%"/></td></tr>
</table>
<h4>🔹 Gestión de Categorías</h4>
<table width="100%">
  <tr><td width="25%" align="center"><b>Detalles</b></td><td width="25%" align="center"><b>Crear</b></td><td width="25%" align="center"><b>Editar</b></td><td width="25%" align="center"><b>Eliminar</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/light/reference/cat-details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/reference/cat-create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/reference/cat-edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/reference/cat-delete.png" width="100%"/></td></tr>
</table>
</details>

<details>
<summary><b>💰 Órdenes de Recibo en Efectivo (ПКО)</b></summary>
<br/>
<img src="docs/imgs/screenshots/light/orders/list.png" width="100%" alt="Orders List"/>
<table width="100%">
  <tr><td width="25%" align="center"><b>Detalles</b></td><td width="25%" align="center"><b>Crear</b></td><td width="25%" align="center"><b>Editar</b></td><td width="25%" align="center"><b>Eliminar</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/light/orders/details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/orders/create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/orders/edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/orders/delete.png" width="100%"/></td></tr>
</table>
</details>

<details>
<summary><b>📈 Artículos de Ingresos (Статьи доходов)</b></summary>
<br/>
<img src="docs/imgs/screenshots/light/income/list.png" width="100%" alt="Income List"/>
<table width="100%">
  <tr><td width="25%" align="center"><b>Detalles</b></td><td width="25%" align="center"><b>Crear</b></td><td width="25%" align="center"><b>Editar</b></td><td width="25%" align="center"><b>Eliminar</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/light/income/details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/income/create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/income/edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/income/delete.png" width="100%"/></td></tr>
</table>
</details>

<details>
<summary><b>⚖️ Gestión del IVA (НДС)</b></summary>
<br/>
<img src="docs/imgs/screenshots/light/vat/list.png" width="100%" alt="VAT List"/>
<table width="100%">
  <tr><td width="25%" align="center"><b>Detalles</b></td><td width="25%" align="center"><b>Crear</b></td><td width="25%" align="center"><b>Editar</b></td><td width="25%" align="center"><b>Eliminar</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/light/vat/details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/vat/create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/vat/edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/light/vat/delete.png" width="100%"/></td></tr>
</table>
</details>
</details>

<details>
<summary><h3 style="display:inline;">🌙 3. Experiencia en Tema Oscuro</h3></summary>
<br/>

<details>
<summary><b>📊 Panel / Página de Inicio</b></summary>
<br/>
<p align="center">
  <img src="docs/imgs/screenshots/dark/home/home.png" width="80%" alt="Dashboard Dark" style="max-width:800px;"/>
</p>
</details>

<details>
<summary><b>👤 Perfil de Usuario</b></summary>
<br/>
<table width="100%">
  <tr><td width="50%" align="center"><b>Vista del Perfil</b></td><td width="50%" align="center"><b>Editar Perfil</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/dark/profile/details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/profile/edit.png" width="100%"/></td></tr>
</table>
</details>

<details>
<summary><b>🗂️ Cuentas IFRS (МСФО) — Flujo de 9 Pantallas</b></summary>
<br/>
<p align="center"><b>Tabla Principal Unificada</b></p>
<img src="docs/imgs/screenshots/dark/ifrs/main-list.png" width="100%" alt="IFRS Main Dark"/>
<br/><br/>
<h4>🔹 Gestión de Cuentas</h4>
<table width="100%">
  <tr><td width="25%" align="center"><b>Detalles</b></td><td width="25%" align="center"><b>Crear</b></td><td width="25%" align="center"><b>Editar</b></td><td width="25%" align="center"><b>Eliminar</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/dark/ifrs/acc-details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/ifrs/acc-create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/ifrs/acc-edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/ifrs/acc-delete.png" width="100%"/></td></tr>
</table>
<h4>🔹 Gestión de Categorías</h4>
<table width="100%">
  <tr><td width="25%" align="center"><b>Detalles</b></td><td width="25%" align="center"><b>Crear</b></td><td width="25%" align="center"><b>Editar</b></td><td width="25%" align="center"><b>Eliminar</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/dark/ifrs/cat-details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/ifrs/cat-create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/ifrs/cat-edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/ifrs/cat-delete.png" width="100%"/></td></tr>
</table>
</details>

<details>
<summary><b>📋 Manuales de Referencia (Справочники) — Flujo de 9 Pantallas</b></summary>
<br/>
<p align="center"><b>Tabla Principal Unificada</b></p>
<img src="docs/imgs/screenshots/dark/reference/main-list.png" width="100%" alt="Reference Main Dark"/>
<br/><br/>
<h4>🔹 Gestión de Elementos de Referencia</h4>
<table width="100%">
  <tr><td width="25%" align="center"><b>Detalles</b></td><td width="25%" align="center"><b>Crear</b></td><td width="25%" align="center"><b>Editar</b></td><td width="25%" align="center"><b>Eliminar</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/dark/reference/ref-details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/reference/ref-create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/reference/ref-edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/reference/ref-delete.png" width="100%"/></td></tr>
</table>
<h4>🔹 Gestión de Categorías</h4>
<table width="100%">
  <tr><td width="25%" align="center"><b>Detalles</b></td><td width="25%" align="center"><b>Crear</b></td><td width="25%" align="center"><b>Editar</b></td><td width="25%" align="center"><b>Eliminar</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/dark/reference/cat-details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/reference/cat-create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/reference/cat-edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/reference/cat-delete.png" width="100%"/></td></tr>
</table>
</details>

<details>
<summary><b>💰 Órdenes de Recibo en Efectivo (ПКО)</b></summary>
<br/>
<img src="docs/imgs/screenshots/dark/orders/list.png" width="100%" alt="Orders List Dark"/>
<table width="100%">
  <tr><td width="25%" align="center"><b>Detalles</b></td><td width="25%" align="center"><b>Crear</b></td><td width="25%" align="center"><b>Editar</b></td><td width="25%" align="center"><b>Eliminar</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/dark/orders/details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/orders/create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/orders/edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/orders/delete.png" width="100%"/></td></tr>
</table>
</details>

<details>
<summary><b>📈 Artículos de Ingresos (Статьи доходов)</b></summary>
<br/>
<img src="docs/imgs/screenshots/dark/income/list.png" width="100%" alt="Income List Dark"/>
<table width="100%">
  <tr><td width="25%" align="center"><b>Detalles</b></td><td width="25%" align="center"><b>Crear</b></td><td width="25%" align="center"><b>Editar</b></td><td width="25%" align="center"><b>Eliminar</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/dark/income/details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/income/create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/income/edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/income/delete.png" width="100%"/></td></tr>
</table>
</details>

<details>
<summary><b>⚖️ Gestión del IVA (НДС)</b></summary>
<br/>
<img src="docs/imgs/screenshots/dark/vat/list.png" width="100%" alt="VAT List Dark"/>
<table width="100%">
  <tr><td width="25%" align="center"><b>Detalles</b></td><td width="25%" align="center"><b>Crear</b></td><td width="25%" align="center"><b>Editar</b></td><td width="25%" align="center"><b>Eliminar</b></td></tr>
  <tr><td align="center"><img src="docs/imgs/screenshots/dark/vat/details.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/vat/create.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/vat/edit.png" width="100%"/></td><td align="center"><img src="docs/imgs/screenshots/dark/vat/delete.png" width="100%"/></td></tr>
</table>
</details>
</details>

<hr />

<h2 id="getting-started">🚀 Primeros Pasos</h2>
<p>Siga estos pasos para obtener y ejecutar una copia local. Ejecutar los comandos desde el directorio del servidor principal restaurará y compilará automáticamente todos los proyectos vinculados (Cliente y Contratos).</p>

<h3>📋 1. Prerrequisitos</h3>
<ul>
    <li><strong><a href="https://dotnet.microsoft.com/download/dotnet/10.0">.NET 10 SDK</a></strong></li>
    <li><strong><a href="https://www.postgresql.org/download/">PostgreSQL</a></strong></li>
</ul>

<h3>🛠️ 2. Instalación y Configuración</h3>

<ol>
    <li>
        <strong>Clonar el repositorio:</strong>
        <pre><code>git clone https://github.com/abdullokhonz/Bookkeeping.git
cd Bookkeeping</code></pre>
    </li>
    <li>
        <strong>Navegar al proyecto del Servidor Principal:</strong>
        <p><i>La solución está estructurada para compilar todas las dependencias desde este punto de entrada:</i></p>
        <pre><code>cd Bookkeeping
cd Bookkeeping</code></pre>
    </li>
    <li>
        <strong>Configurar Entorno (appsettings.json):</strong>
        <p>Actualice el archivo de configuración dentro de la carpeta <code>Bookkeeping</code> con sus credenciales. Necesitará configurar la base de datos, JWT y los proveedores de servicios:</p>
<pre><code>{
  "ApiSettings": { "BaseUrl": "https://localhost:7277/" },
  "ConnectionStrings": {
    "DbPostgres": "Host=localhost;Port=5432;Database=BookkeepingDB;User ID=postgres;Password=your_password"
  },
  "JwtSettings": {
    "Key": "YOUR_SECURE_JWT_KEY_HERE",
    "Issuer": "Bookkeeping_IdentityServer",
    "Audience": "Bookkeeping_WebClient",
    "LifetimeMinutes": 15
  },
  "EmailSettings": {
    "SmtpHost": "YOUR_SMTP_HOST_HERE",
    "SmtpPort": "YOUR_SMTP_PORT_HERE",
    "FromName": "Bookkeeping Notifications",
    "FromEmail": "YOUR_EMAIL_HERE",
    "EmailPassword": "YOUR_EMAIL_PASSWORD_HERE"
  },
  "SmsSettings": {
    "Dlm": "YOUR_SMS_DLM_HERE",
    "T": "YOUR_SMS_T_HERE",
    "Login": "YOUR_SMS_LOGIN_HERE",
    "PassHash": "YOUR_SMS_PASSHASH_HERE",
    "Sender": "YOUR_SMS_SENDER_HERE"
  }
}</code></pre>
    </li>
    <li>
        <strong>Compilar y Ejecutar:</strong>
        <p>Esto restaurará todos los proyectos (Servidor, Cliente, Contratos) e iniciará la aplicación:</p>
        <pre><code>dotnet restore
dotnet build
dotnet run</code></pre>
        <p><i>La plataforma estará disponible en su <code>BaseUrl</code> configurado.</i></p>
    </li>
</ol>

<hr />

<h2 id="authentication-notes">🔐 Notas Importantes sobre Autenticación</h2>

<table width="100%">
    <thead>
        <tr>
            <th align="left">Entorno</th>
            <th align="left">Instrucciones de Acceso</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><strong>🌐 Web Frontend (Blazor)</strong></td>
            <td>El flujo de autenticación completo está activo. Puede registrarse e iniciar sesión para acceder al panel.</td>
        </tr>
        <tr>
            <td><strong>🚀 Postman / External</strong></td>
            <td>Use <code>/auth/login</code> para obtener un token y agregarlo al encabezado <code>Authorization: Bearer</code>.</td>
        </tr>
        <tr>
            <td><strong>🛠️ Swagger UI</strong></td>
            <td>
                <strong>Advertencia:</strong> La autorización a través de Swagger UI no está configurada actualmente.<br />
                <u>Para probar mediante Swagger:</u> Elimine manualmente el atributo <code>[Authorize]</code> de los Controllers.
            </td>
        </tr>
    </tbody>
</table>

<hr />

## 🤝 Contribuciones

¡Las contribuciones son bienvenidas! Si tiene sugerencias o desea contribuir, no dude en abrir issues o pull requests.

## 📄 Licencia

Este proyecto está licenciado bajo la Licencia MIT - consulte el archivo [LICENSE](LICENSE.txt) para más detalles.

---
