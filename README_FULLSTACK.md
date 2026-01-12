# D&D Beyond Full-Stack Application

A full-stack application for managing D&D character sheets with a .NET backend and React frontend.

## Project Structure

- **DnDBeyond.Backend** - ASP.NET Core Web API
- **DnDBeyond.Frontend** - React + TypeScript application
- **DnDBeyond.Shared** - Shared entities and enums

## Quick Start

### Backend Setup

1. Navigate to the backend directory:
```bash
cd DnDBeyond.Backend
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Update the database connection string in `appsettings.json` if needed

4. Run the backend:
```bash
dotnet run
```

The API will be available at `http://localhost:5000` (or `https://localhost:5001`)

### Frontend Setup

1. Navigate to the frontend directory:
```bash
cd DnDBeyond.Frontend
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
npm run dev
```

The frontend will be available at `http://localhost:3000`

## Features

### Backend API
- Character management (CRUD operations)
- Deal damage with damage type calculations
- Healing functionality
- Temporary hit points management
- Defense modifiers (immunity, resistance, vulnerability)
- Entity Framework Core with SQL Server
- Swagger API documentation

### Frontend Application
- Character sheet display
  - Name, class, and level
  - Current/Max hit points
  - Temporary hit points
  - All ability scores with modifiers
- Action controls
  - Deal damage (with damage type selection)
  - Heal character
  - Set temporary hit points
- Auto-refresh every 5 seconds
- Fully responsive design
- Accessibility features (keyboard navigation, screen readers)
- CSS Modules for scoped styling

## API Endpoints

- `GET /api/characters` - Get all characters
- `GET /api/characters/{id}` - Get character by ID
- `POST /api/characters/{id}/damage` - Deal damage to character
- `POST /api/characters/{id}/heal` - Heal character
- `POST /api/characters/{id}/temp-hp` - Set temporary hit points

## Technology Stack

### Backend
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server

### Frontend
- React 18
- TypeScript
- Vite
- CSS Modules

## Development

### Running Both Applications

1. Start the backend (Terminal 1):
```bash
cd DnDBeyond.Backend
dotnet run
```

2. Start the frontend (Terminal 2):
```bash
cd DnDBeyond.Frontend
npm run dev
```

3. Open your browser to `http://localhost:3000`

### Building for Production

Backend:
```bash
cd DnDBeyond.Backend
dotnet publish -c Release
```

Frontend:
```bash
cd DnDBeyond.Frontend
npm run build
```

## Accessibility

The frontend application follows WCAG 2.1 guidelines:
- Semantic HTML elements
- ARIA labels and roles
- Keyboard navigation support
- Screen reader announcements
- Proper form labels
- Focus management

## License

This project is for educational and demonstration purposes.
