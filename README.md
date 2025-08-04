# 🛍️ MyShopAPI

**MyShopAPI** is a full-featured ASP.NET Core Web API backend for an e-commerce platform. It supports user authentication, product management, cart and wishlist operations, reviews, and payment processing via Monnify and PayStack.

🔗 **Live API Endpoint:** [https://myshopapi-ggcb.onrender.com](https://myshopapi-ggcb.onrender.com)  
🌐 **Frontend App:** [https://maishop.netlify.app/](https://maishop.netlify.app/)

---

## 📦 Features

- ✅ User registration, login, and email confirmation
- 🔐 Password reset and account management
- 🛒 Product listing, filtering, and search
- ❤️ Cart and wishlist functionality
- ⭐ Product reviews and ratings
- 🚚 Delivery profile management
- 📦 Order tracking
- 💳 Payment integration with Monnify and PayStack

---

## 🧰 Tech Stack

| Layer       | Technology                    |
| ----------- | ----------------------------- |
| Backend API | ASP.NET Core Web API          |
| ORM         | Entity Framework Core         |
| API Docs    | Swagger (OpenAPI 3.0)         |
| Auth        | JWT Authentication            |
| Database    | SQL Server (or compatible DB) |
| Payments    | Monnify & PayStack APIs       |

---

## 🚀 Getting Started

### 🔧 Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- SQL Server or any compatible database
- Visual Studio or VS Code

### ⚙️ Installation

```bash
# Clone the repository
git clone https://github.com/your-username/MyShopAPI.git
cd MyShopAPI

# Update your appsettings.json with your database connection string
# Example:
# "ConnectionStrings": {
#   "DefaultConnection": "Your-Database-Connection-String"
# }

# Apply migrations and update the database
dotnet ef database update

# Run the application
dotnet run
```
