# ElementalGUI

A simple Windows Forms elemental battle game written in C#.

## About

ElementalGUI lets two players choose elemental characters and prepare for a turn-based battle. Choose from:

- **Lumen** - Fire
- **Ripple** - Water
- **Grunchwood** - Earth
- **Gale** - Air

Each character has health, a basic attack, a limited special attack, and a defensive action.

## Requirements

- Windows
- .NET 10 SDK
- Visual Studio 2022 or another IDE that supports Windows Forms and .NET 10

## Running the project

1. Clone the repository:

   ```bash
   git clone https://github.com/strawbwiees/ElementalGUI.git
   cd ElementalGUI
   ```

2. Open `ElementalGUI.slnx` in Visual Studio.
3. Build and run the project.
4. Select a character for Player 1 and Player 2.

## Project structure

- `Form1.cs` - Main menu
- `ChooseCharacters.cs` - Character selection
- `Battle.cs` - Battle screen
- `Character.cs` - Character stats and actions
- `gameData.cs` - Shared player data