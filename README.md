# Appointment Booking App

A simple appointment booking application built using ASP.NET Core MVC and MySQL.

This project was developed as a practical full-stack development assignment. It allows users to create and manage patient appointments and also includes an AI feature for generating a short appointment summary.

## Features

* Book a new appointment
* Enter patient details
* Select a doctor
* Select appointment date and time
* Add reason for visit
* Generate AI appointment summary
* View all appointments
* Mark appointment as completed
* Cancel appointment
* Delete appointment
* Appointment status tracking
* Form validation
* MySQL database storage
* Responsive UI

## Technologies Used

* C#
* ASP.NET Core MVC
* Entity Framework Core
* MySQL
* HTML
* CSS
* Bootstrap
* JavaScript
* Razor Views
* Google Gemini API

## Project Structure

```text
AppointmentBookingApp
│
├── Controllers
├── Data
├── Migrations
├── Models
├── Services
├── Views
├── wwwroot
├── Program.cs
├── appsettings.json
└── AppointmentBookingApp.csproj
```

## How It Works

The application uses the MVC pattern.

```text
User
 ↓
View
 ↓
Controller
 ↓
Entity Framework Core
 ↓
MySQL
```

For the AI summary:

```text
Reason for Visit
 ↓
AppointmentsController
 ↓
GeminiService
 ↓
Google Gemini API
 ↓
AI Summary
```

## Database Setup

The application uses MySQL.

Create a database named:

```sql
CREATE DATABASE appointmentbooking_db;
```

The required tables are created using Entity Framework Core migrations.

## Configuration

Update the MySQL connection string in `appsettings.json` according to your local MySQL setup.

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=appointmentbooking_db;user=root;password=;"
}
```

If your MySQL account has a password, add it to the connection string.

### Gemini API Key

The Gemini API key is **not stored in `appsettings.json` or GitHub**.

For local development, ASP.NET Core User Secrets can be used:

```bash
dotnet user-secrets set "Gemini:ApiKey" "YOUR_API_KEY"
```

The Gemini model used by the application is:

```text
gemini-3.6-flash
```

## Running the Project

### 1. Clone the repository

```bash
git clone https://github.com/vinitpatil0423/AppointmentBookinggApp.git
```

### 2. Open the project

Open `AppointmentBookingApp.slnx` in Visual Studio.

### 3. Create the database

Create the `appointmentbooking_db` database in MySQL.

### 4. Apply migrations

Run:

```bash
dotnet ef database update
```

### 5. Run the application

Run the project from Visual Studio.

## AI Appointment Summary

The application has an optional AI feature where the user can enter the reason for the appointment and generate a short summary.

The AI prompt is designed only to summarize the information provided by the user. It is not used to diagnose a patient or provide medical treatment advice.

The generated summary is saved with the appointment and can also be viewed from the appointment list.

## Validation

The application includes validation for:

* Patient name
* Mobile number
* Doctor selection
* Reason for visit
* Appointment date
* Appointment time

The appointment date cannot be selected in the past.

## Appointment Status

Appointments can have three statuses:

* Scheduled
* Completed
* Cancelled

Users can update the status from the appointment list.

## Future Improvements

Some features that could be added later:

* Login and user roles
* Doctor management
* Appointment time-slot checking
* Prevent double booking
* Search and filtering
* Email or SMS reminders
* Better dashboard and reports

## Author
**Vinit Patil**
GitHub: https://github.com/vinitpatil0423
