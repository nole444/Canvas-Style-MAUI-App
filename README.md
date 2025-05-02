# Canvas-Like Learning Management System (LMS)
This MAUI app allows teachers to create classes and students to join them and complete assignments similar to the Canvas web application.
## 📖 Overview
The project is a **Canvas-like Learning Management System (LMS)** built using **C#**. It consists of two main components:

1. **App.Canvas** - The frontend application and core logic for managing students, courses, and assignments.
2. **MyWebAPI** - A lightweight API that provides RESTful endpoints for interacting with student and course data. It persists data **in memory** instead of using a database.

The project is designed to provide a **lightweight, scalable, and user-friendly** platform for managing educational courses, student records, and assignments.

---

##  Project Structure

---

##  **Technologies Used**
### Backend API (`MyWebAPI`)
- **ASP.NET Core Web API** - RESTful API for backend services
- **In-Memory Data Storage** - Data is persisted in-memory instead of using a database
- **C# Dependency Injection** - Manages service lifecycles efficiently
- **Swagger** - API documentation for testing endpoints

### Core Application (`App.Canvas`)
- **.NET MAUI (XAML)** - UI framework for cross-platform apps
- **MVVM Architecture** - Separation of UI and business logic
- **HttpClient** - API communication for retrieving and sending data
- **Dependency Injection** - Manages services efficiently

---

## **Setup Instructions**

### Prerequisites
Ensure you have the following installed:
- **.NET 7 or later**
- **Visual Studio 2022** (with .NET MAUI workload)
- **Postman / Swagger** (for API testing)
