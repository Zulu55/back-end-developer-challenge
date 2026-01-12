# D&D Beyond Frontend

A React + TypeScript frontend application for managing D&D character sheets.

## Features

- **Character Display**: Shows character name, class, level, hit points (current/max), temporary hit points, and all ability scores
- **Action Controls**: Interface for dealing damage (with damage type selection), healing, and setting temporary hit points
- **Auto-Refresh**: Automatically updates character data every 5 seconds to reflect API changes
- **Responsive Design**: Works on both large and small screens
- **Accessible**: Keyboard navigable with proper ARIA labels for screen readers

## Technology Stack

- React 18
- TypeScript
- Vite (build tool)
- CSS Modules (scoped styling)

## Getting Started

### Prerequisites

- Node.js (version 16 or higher)
- npm or yarn
- Backend API running on `http://localhost:5000`

### Installation

1. Install dependencies:
```bash
npm install
```

### Running the Application

1. Start the development server:
```bash
npm run dev
```

2. Open your browser to `http://localhost:3000`

3. Make sure the backend API is running on `http://localhost:5000`

### Building for Production

```bash
npm run build
```

The built files will be in the `dist` directory.

### Preview Production Build

```bash
npm run preview
```

## Project Structure

```
src/
├── components/          # React components
│   ├── ActionControls.tsx
│   ├── ActionControls.module.css
│   ├── CharacterSheet.tsx
│   └── CharacterSheet.module.css
├── services/           # API service layer
│   └── characterService.ts
├── types/              # TypeScript type definitions
│   └── Character.ts
├── App.tsx             # Main application component
├── App.module.css      # App styles
├── main.tsx           # Application entry point
└── index.css          # Global styles
```

## API Integration

The frontend communicates with the backend API through the following endpoints:

- `GET /api/characters` - Fetch all characters
- `GET /api/characters/{id}` - Fetch a specific character
- `POST /api/characters/{id}/damage` - Deal damage to a character
- `POST /api/characters/{id}/heal` - Heal a character
- `POST /api/characters/{id}/temp-hp` - Set temporary hit points

## Accessibility Features

- Semantic HTML elements
- ARIA labels and roles
- Keyboard navigation support
- Screen reader announcements for dynamic content
- Focus management in forms
- Proper form labels and associations

## Responsive Design

The application is fully responsive and adapts to:
- Desktop screens (> 768px)
- Tablet screens (481px - 768px)
- Mobile screens (< 481px)

CSS Grid is used for flexible layouts that automatically adjust based on available space.
