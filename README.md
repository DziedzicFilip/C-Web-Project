# C-Web-Project

## Overview

C-Web-Project is a simple online store that enables product presentation, promotion management, order placement, and contacting the company. The application is built with ASP.NET Core MVC using Razor Pages and Entity Framework. It includes both a public section (for customers) and an administrative panel (intranet) for managing content and products.

## Features

- **Product Catalog**: Organize products into categories like Computers, Smartphones, Accessories, and Monitors.
- **Promotions**: Manage time-limited product promotions and discounts.
- **Orders (Zamówienia)**: Allow users to place and track orders.
- **Contact Page**: Display company contact information, including office address, phone, email, and working hours.
- **Content Management**: Manage site pages, banners, and regulations (terms).
- **User Authentication & Roles**: Supports user registration, login, and role-based access (admin/regular user).
- **Admin Panel (Intranet)**: Backend for managing all data entities (products, promotions, banners, users, etc.).
- **Localization**: Configured for Polish language and locale (pl-PL).
- **Responsive UI**: Uses Bootstrap for modern, responsive design.

## Tech Stack

- **Backend**: ASP.NET Core MVC, Razor Pages, Entity Framework Core
- **Frontend**: Bootstrap, jQuery, custom JS/CSS
- **Database**: SQL Server (via Entity Framework migrations)
- **Authentication**: ASP.NET Identity

## Project Structure

- `index.html` — Main landing page with navigation (Produkty, Zamówienia, Kontakt, Koszyk).
- `Firma/Firma.Intranet` — Intranet/admin backend, with controllers for entities such as `Promocja`, `ProduktPromocja`, `Strona`, `Baner`, `Kontakt`, `Regulamin`, `Uzytkownik`.
- `Firma/Firma.PortalWWW` — Public-facing portal (shop/catalog).
- `Firma/Firma.Data` — EF Core data models and database migrations.
- `wwwroot/lib/` — Frontend libraries (Bootstrap, jQuery-validation, etc.).

## Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/DziedzicFilip/C-Web-Project.git
   ```

2. **Setup the database**
   - Update the connection string in `appsettings.json` as needed.
   - Run migrations:
     ```
     dotnet ef database update
     ```

3. **Run the application**
   ```bash
   dotnet run --project Firma/Firma.Intranet
   ```

4. **Access**
   - Frontend: `http://localhost:5000/`
   - Admin/Intranet: `http://localhost:5000/intranet` (may require admin login)

## Contributing

Feel free to fork the repository and submit pull requests. Issues and suggestions are welcome!

## License

This project uses open-source libraries such as Bootstrap and jQuery Validation under the MIT License. See respective `/wwwroot/lib/.../LICENSE` files for details.

---

**Note:**  
The project and content are primarily in Polish (`pl-PL` locale).
