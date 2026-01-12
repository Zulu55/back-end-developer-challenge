# Juan Zuluaga's Test Solution

Full-stack application for D&D character management with .NET backend and React + TypeScript frontend.

## Project Structure

- **DnDBeyond.Backend** - ASP.NET Core Web API
- **DnDBeyond.Frontend** - React + TypeScript with Vite
- **DnDBeyond.Shared** - Shared entities and enums

## Prerequisites

- .NET 8 SDK
- Node.js (v16 or higher)
- SQL Server or SQL Server Express

## Running Instructions

### 1. Backend

Open a PowerShell terminal and run:

```powershell
cd DnDBeyond.Backend
dotnet restore
dotnet run
```

The backend will be available at:
- HTTPS: `https://localhost:7048`
- HTTP: `http://localhost:5048`
- Swagger UI: `https://localhost:7048/swagger`

**Note:** The database is created and seeded automatically with some characters when the application starts.

<img width="1306" height="490" alt="image" src="https://github.com/user-attachments/assets/d7be22bc-d186-42b1-b7aa-055db3bf3916" />

### 2. Frontend

Open a new PowerShell terminal and run:

```powershell
cd DnDBeyond.Frontend
npm install
npm run dev
```

The frontend will be available at:
- `http://localhost:3000`

<img width="877" height="925" alt="image" src="https://github.com/user-attachments/assets/c8f385d2-892e-4eda-a20a-a919198323d5" />

### 3. Access the Application

Once both services are running:
1. Open your browser to `http://localhost:3000`
2. You will see Briv's character sheet with all his stats

## Implemented Features

### Backend API
- ✅ Character management (CRUD)
- ✅ Deal Damage with damage type calculations
- ✅ Defense system (immunity, resistance, vulnerability)
- ✅ Character healing
- ✅ Temporary Hit Points management
- ✅ Entity Framework Core with SQL Server
- ✅ Swagger documentation

### Frontend
- ✅ Character sheet display:
  - Name, class, and level
  - Hit Points (current/maximum)
  - Temporary Hit Points
  - Ability scores with modifiers
- ✅ Action controls:
  - Deal damage (with damage type selection)
  - Heal character
  - Set temporary HP
- ✅ Auto-refresh every 5 seconds
- ✅ Responsive design (mobile, tablet, desktop)
- ✅ Full accessibility (keyboard navigation, screen readers)
- ✅ CSS Modules for styling

## API Endpoints

- `GET /api/characters` - Get all characters
- `GET /api/characters/{id}` - Get character by ID
- `POST /api/characters/{id}/damage` - Deal damage to character
- `POST /api/characters/{id}/heal` - Heal character
- `POST /api/characters/{id}/temp-hp` - Set temporary HP

## Technologies Used

**Backend:**
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server

**Frontend:**
- React 18
- TypeScript
- Vite 4
- CSS Modules

## Troubleshooting

### Backend won't start
- Verify that SQL Server is running
- Check the connection string in `appsettings.json`

### Frontend can't connect to backend
- Ensure the backend is running on port 7048
- Verify the proxy configuration in `vite.config.ts`

### Node.js error
- If you have Node.js 16, the project uses Vite 4 (compatible)
- For Vite 5, upgrade Node.js to v18 or higher

## Author

**Juan Zuluaga**

---

# DDB Back End Developer Challenge (Original Requirements)

### Overview
This task focuses on creating an API for managing a player character's Hit Points (HP) within our game. The API will enable clients to perform various operations related to HP, including dealing damage of different types, considering character resistances and immunities, healing, and adding temporary Hit Points. The task requires building a service that interacts with HP data provided in the `briv.json` file and persists throughout the application's lifetime.

### Task Requirements

#### API Operations
1. **Deal Damage**
    - Implement the ability for clients to deal damage of different types (e.g., bludgeoning, fire) to a player character.
    - Ensure that the API calculates damage while considering character resistances and immunities.

    > Suppose a player character is hit by an attack that deals Piercing damage, and the attacker rolls a 14 on the damage's Hit Die (with a Piercing damage type). `[Character Hit Points - damage: 25 - 14 = 11]`

2. **Heal**
    - Enable clients to heal a player character, increasing their HP.

3. **Add Temporary Hit Points**
    - Implement the functionality to add temporary Hit Points to a player character.
    - Ensure that temporary Hit Points follow the rules: they are not additive, always taking the higher value, and cannot be healed.

    > Imagine a player character named "Eldric" currently has 11 Hit Points (HP) and no temporary Hit Points. He finds a magical item that grants him an additional 10 HP during the next fight. When the attacker rolls a 19, Eldric will lose all 10 temporary Hit Points and 9 from his player HP.

#### Implementation Details
- Build the API using either C# or NodeJS.
- Ensure that character information, including HP, is initialized during the start of the application. Developers do not need to calculate HP; it is provided in the `briv.json` file.
- Retrieve character information, including HP, from the `briv.json` file.


#### Data Storage
- You have the flexibility to choose the data storage method for character information.

### Instructions to Run Locally
1. Clone the repository or obtain the project files.
2. Install any required dependencies using your preferred package manager.
3. Configure the API with necessary settings (e.g., database connection if applicable).
4. Build and run the API service locally.
5. Utilize the provided `briv.json` file as a sample character data, including HP, for testing the API.

### Additional Notes
- Temporary Hit Points take precedence over the regular HP pool and cannot be healed.
- Characters with resistance take half damage, while characters with immunity take no damage from a damage type.
- Use character filename as identifier

#### Possible Damage Types in D&D
Here is a list of possible damage types that can occur in Dungeons & Dragons (D&D). These damage types should be considered when dealing damage or implementing character resistances and immunities:
- Bludgeoning
- Piercing
- Slashing
- Fire
- Cold
- Acid
- Thunder
- Lightning
- Poison
- Radiant
- Necrotic
- Psychic
- Force

If you have any questions or require clarification, please reach out to your Wizards of the Coast contact, and we will provide prompt assistance.

Good luck with the implementation!

## Full-stack addendum
If completing this challenge as a full-stack engineer, please provide the following UI experience to complement the API.

Build out components that display the following information from the character:
- Name
- Class
- Level
- Hit Points: Current/Max
- Temporary Hit Points
- Each of the stat values
  
**This interface should update automatically when a change is made from the API.**

Provide an interface for dealing damage, healing, and adding temporary hit points:
- Textbox Select(damage type values) Button(text: Deal Damage)
- Textbox Button(text: Heal)
- Textbox Button(text: Set Temporary HP)
  
Requirements for the UI:
- Built in React and Typescript
- CSS Modules for styling
- Must work on large and small screens
- Accessibility: should be navigable by keyboard and include proper labels for screen reading software
