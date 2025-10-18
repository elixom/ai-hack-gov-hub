# Caballero Care Portal (CCP)

A comprehensive healthcare management web application built with **Laravel 12.x** and **Vue.js 3** for patient care, appointments, and medical records management.

## 🏥 Features

- **Patient Management** - Complete patient registration, demographics, and medical history
- **Appointment Scheduling** - Calendar-based appointment booking and management
- **Medical Records** - Electronic medical records (EMR) with clinical documentation
- **Prescription Management** - Medication prescribing, tracking, and refill management
- **Document Management** - Secure storage and organization of medical documents
- **Patient Portal** - Self-service portal for patients to access their information
- **Administration** - User management, system settings, and audit logging
- **Analytics Dashboard** - Healthcare reporting and performance metrics

## 🛠️ Technology Stack

- **Backend:** Laravel 12.x with PHP 8.2
- **Frontend:** Vue.js 3 (Options API) with JavaScript
- **Database:** PostgreSQL with Eloquent ORM
- **Authentication:** Laravel Sanctum API tokens
- **Styling:** Tailwind CSS with healthcare-specific design
- **Build Tool:** Laravel Vite plugin with Vue.js support
- **State Management:** Vuex store
- **HTTP Client:** Axios

## 🚀 Quick Start

### Prerequisites

- PHP 8.2 or higher
- Composer
- Node.js 18+ and npm
- PostgreSQL database

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd caballero-care-portal
   ```

2. **Install PHP dependencies**
   ```bash
   composer install
   ```

3. **Install Node.js dependencies**
   ```bash
   npm install
   ```

4. **Environment setup**
   ```bash
   cp .env.example .env
   php artisan key:generate
   ```

5. **Configure database**
   Edit `.env` file with your PostgreSQL database credentials:
   ```env
   DB_CONNECTION=pgsql
   DB_HOST=127.0.0.1
   DB_PORT=5432
   DB_DATABASE=ccp_database
   DB_USERNAME=your_username
   DB_PASSWORD=your_password
   ```

6. **Run database migrations**
   ```bash
   php artisan migrate
   ```

7. **Build frontend assets**
   ```bash
   npm run build
   ```

8. **Start the application**
   ```bash
   php artisan serve --host=0.0.0.0 --port=8000
   ```

   The application will be available at: `http://localhost:8000`

## 🏗️ Development

### Frontend Development

For development with hot reload:
```bash
npm run dev
```

### Laravel Artisan Commands

```bash
# Generate application key
php artisan key:generate

# Run migrations
php artisan migrate

# Create new migration
php artisan make:migration create_example_table

# Create new controller
php artisan make:controller ExampleController

# Create new model
php artisan make:model Example
```

## 📁 Project Structure

```
├── app/                    # Laravel application logic
│   ├── Http/Controllers/   # API controllers
│   ├── Models/            # Eloquent models
│   └── Providers/         # Service providers
├── database/              # Database migrations and seeders
├── resources/             # Frontend resources
│   ├── css/              # Stylesheets
│   ├── js/               # Vue.js application
│   │   ├── pages/        # Vue.js page components
│   │   ├── components/   # Reusable Vue components
│   │   ├── stores/       # Vuex store modules
│   │   ├── App.vue       # Main Vue component
│   │   └── app.js        # Vue.js entry point
│   └── views/            # Blade templates
├── routes/               # Laravel routes
│   ├── api.php          # API routes
│   └── web.php          # Web routes (SPA catch-all)
├── public/              # Public assets
├── storage/             # File storage
└── vendor/              # PHP dependencies
```

## 🔐 Authentication

The application uses Laravel Sanctum for API authentication with the following endpoints:

- `POST /api/login` - User login
- `POST /api/logout` - User logout
- `GET /api/user` - Get authenticated user data
- `POST /api/register` - User registration (admin only)

## 🏥 Healthcare Modules

### 1. Dashboard
- Healthcare overview with patient statistics
- Recent appointments and activity feed
- Quick action buttons for common tasks

### 2. Patient Management
- Complete patient registration system
- Advanced search and filtering
- Medical history tracking
- Demographics management

### 3. Appointment Scheduling
- Calendar-based scheduling interface
- Conflict detection and resolution
- Provider availability management
- Appointment status tracking

### 4. Medical Records
- Clinical documentation system
- Vital signs tracking
- Diagnosis management
- Visit notes and assessments

### 5. Prescription Management
- Medication prescribing workflow
- Drug interaction checking
- Refill management
- Dosage instructions

### 6. Document Management
- Secure file storage
- Document categorization
- Search and retrieval
- Access control

### 7. Patient Portal
- Self-service patient access
- Appointment booking
- Medical record viewing
- Prescription history

### 8. Administration
- User and role management
- System configuration
- Audit logging
- Compliance tracking

### 9. Analytics
- Healthcare reporting
- Performance metrics
- Custom report generation
- Data visualization

## 🔧 Configuration

### Environment Variables

Key environment variables to configure:

```env
APP_NAME="Caballero Care Portal"
APP_ENV=production
APP_DEBUG=false
APP_URL=http://your-domain.com

DB_CONNECTION=pgsql
DB_HOST=your-database-host
DB_PORT=5432
DB_DATABASE=ccp_database
DB_USERNAME=your-username
DB_PASSWORD=your-password

SANCTUM_STATEFUL_DOMAINS=your-domain.com
SESSION_DOMAIN=your-domain.com
```

## 📝 License

This project is proprietary software developed for healthcare management purposes.

## 🤝 Support

For support and questions, please contact the development team.

---

**Caballero Care Portal** - Comprehensive Healthcare Management System