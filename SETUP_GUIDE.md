# Setup Instructions for D&D Beyond Full-Stack Application

## Prerequisites

Before you begin, ensure you have the following installed:
- .NET 8 SDK
- Node.js (v16 or higher)
- SQL Server or SQL Server Express
- A code editor (Visual Studio Code recommended)

## Step-by-Step Setup

### 1. Backend Setup

#### Install Dependencies
```powershell
cd DnDBeyond.Backend
dotnet restore
```

#### Configure Database
The application uses SQL Server. Update the connection string in `appsettings.json` if needed:
```json
{
  "ConnectionStrings": {
    "LocalConnection": "Server=(localdb)\\mssqllocaldb;Database=DnDBeyond;Trusted_Connection=true;TrustServerCertificate=true"
  }
}
```

#### Run Migrations
The database will be created and seeded automatically when you run the application.

#### Start the Backend
```powershell
dotnet run
```

The API will start at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

You can view the Swagger documentation at `http://localhost:5000/swagger`

### 2. Frontend Setup

#### Install Dependencies
Open a new terminal and navigate to the frontend:
```powershell
cd DnDBeyond.Frontend
npm install
```

#### Start the Development Server
```powershell
npm run dev
```

The frontend will start at `http://localhost:3000`

### 3. Access the Application

1. Ensure the backend is running on port 5000
2. Ensure the frontend is running on port 3000
3. Open your browser to `http://localhost:3000`
4. You should see the character sheet for "Briv" (the seeded character)

## Verifying the Setup

### Test the Backend API

1. Open your browser to `http://localhost:5000/swagger`
2. Try the `GET /api/characters` endpoint
3. You should see a JSON response with character data

### Test the Frontend

1. Open `http://localhost:3000`
2. You should see:
   - Character name: "Briv"
   - Class: "Fighter"
   - Level: 5
   - Current HP: 25
   - Max HP: 50
   - Ability scores displayed

3. Try the actions:
   - **Deal Damage**: Enter an amount (e.g., 10), select a damage type, click "Deal Damage"
   - **Heal**: Enter an amount (e.g., 5), click "Heal"
   - **Set Temporary HP**: Enter an amount (e.g., 10), click "Set Temporary HP"

4. The character sheet should update automatically after each action

## Troubleshooting

### Backend Issues

**Database Connection Error**
- Ensure SQL Server is running
- Verify the connection string in `appsettings.json`
- Check that you have permissions to create databases

**Port Already in Use**
- Change the port in `Properties/launchSettings.json`
- Update the frontend proxy configuration in `vite.config.ts`

### Frontend Issues

**Cannot Connect to API**
- Ensure the backend is running on port 5000
- Check browser console for CORS errors
- Verify the proxy configuration in `vite.config.ts`

**Dependencies Installation Failed**
- Delete `node_modules` and `package-lock.json`
- Run `npm install` again
- Ensure you're using Node.js v16 or higher

## Development Tips

### Hot Reload
- Both backend and frontend support hot reload
- Backend: Changes to C# files will trigger a rebuild
- Frontend: Changes to React components update instantly

### Debugging
- Backend: Use Visual Studio or Visual Studio Code with C# extension
- Frontend: Use browser DevTools (F12)

### API Testing
- Use Swagger UI at `http://localhost:5000/swagger`
- Or use Postman/Thunder Client to test endpoints

## Building for Production

### Backend
```powershell
cd DnDBeyond.Backend
dotnet publish -c Release -o ./publish
```

### Frontend
```powershell
cd DnDBeyond.Frontend
npm run build
```

The frontend build output will be in the `dist` folder.

## Features Checklist

✅ Character display with:
- Name
- Class  - Level
- Current/Max HP
- Temporary HP
- All ability scores with modifiers

✅ Action controls:
- Deal damage with damage type selection
- Heal character
- Set temporary hit points

✅ Auto-refresh every 5 seconds

✅ Responsive design (desktop, tablet, mobile)

✅ Accessibility features:
- Keyboard navigation
- ARIA labels
- Screen reader support
- Semantic HTML

✅ CSS Modules for scoped styling

## Next Steps

- Add more characters through the API
- Customize the styling
- Add more features (inventory, spells, etc.)
- Deploy to a production server

## Support

For issues or questions:
1. Check the browser console for errors
2. Check the backend logs
3. Verify all prerequisites are installed
4. Ensure both services are running on the correct ports
