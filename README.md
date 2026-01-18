# PeopleCert eShop Management System

Team bootcamp project that implements a complete exam management and certification eShop system with user roles, authentication, and REST API architecture.
The system allows users to register, authenticate securely, browse certification products, purchase exam vouchers, and track assigned exams, while administrators and staff manage the overall process.

---

## Project Features

The system supports multiple user roles:

### Admin
- Full access to all data
- Manage candidates (CRUD)
- Manage certifications (CRUD)
- Manage exams and marking
- Assign marking jobs to markers

### Marker
- View assigned unmarked exams
- Submit exam marks

### Quality Control
- View-only access to:
  - Candidates
  - Certifications
  - Exams

### eShop / Candidate
- View available certification products
- Purchase exam vouchers
- View certifications and exam results

---

## Tech Stack

### Backend
- ASP.NET Core 7 (`net7.0`)
- C#
- REST API
- Entity Framework Core
- SQL Database (via EF Core)
- JWT Authentication & Authorization
- Swagger / OpenAPI

### Frontend
- React
- Vite
- JavaScript
- CSS

---

## My Role & Contributions

This was a **team project** developed during a bootcamp.  
My main contributions include:

### 🔹 Frontend
- Development of React Components (UI)
- Data display and user interaction with backend API

### 🔹 Backend
- Implementation of REST API endpoints inside `Controllers`
- Example features:
  - Product listing
  - User purchases
  - Exam assignment after purchase
  - Role-based authorization using JWT

### 🔹 Models
- Creation and maintenance of Model classes
- DTO usage for API requests and responses

My work focused on both **frontend UI** and **backend API logic**, providing full-stack experience within the project.

---
