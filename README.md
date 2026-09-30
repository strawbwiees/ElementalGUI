# ElementalGUI

A cartoon-style Windows Forms elemental battle game written in C#.

## About

ElementalGUI lets two players choose elemental characters and battle turn-based. Choose from:

- **Lumen** - Fire
- **Ripple** - Water
- **Grunch** - Earth
- **Gale** - Air

Each fighter has 100 HP, a basic attack (20 dmg), up to 3 special attacks (35 dmg), and a defend action that halves the next hit taken.

## Features

- Comic-style UI: chunky outlined text, rounded buttons with ink outlines, and smiley HP bars that shift from green to gold to red
- POW! / BLOCKED! / K.O.! comic bursts, screen shake, and generated retro sound effects
- Special attacks are limited to 3 per fighter - the button shows how many are left
- Clean screen-to-screen navigation with no hidden windows left behind
- Play again after every battle, or exit cleanly from anywhere

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
4. Select a character for Player 1 and Player 2, confirm, and fight!

## Project structure

- `Form1.cs` - Main menu
- `ChooseCharacter.cs` - Character selection (Player 1 blue, Player 2 red)
- `ConfirmSelection.cs` - Ready-up dialog
- `BattleIntro.cs` - VS screen
- `BattleForm.cs` - Battle screen (attacks, defend, specials, animations)
- `Character.cs` - Character stats and actions
- `CartoonUI.cs` - Shared cartoon styling, HP bars, bursts, and sound helpers
- `SpriteAnimator.cs` - Single-clock sprite playback for smooth GIF animation
- `BufferedPictureBox.cs` - Flicker-free picture box used on the battle screen
- `GameFlow.cs` - Screen navigation that keeps the app clean
