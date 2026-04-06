## Roguelike Dungeon (Unity-Only)

This project uses a Unity-only architecture.

Gameplay logic that used to live in a separate .NET backend has been migrated into Unity source files. No external backend DLL build is required.

## Current Architecture

- Runtime and gameplay: Unity
- Game core code: `UnityClient/Assets/Scripts/GameCore/`
- UI and scene scripts: `UnityClient/Assets/Scripts/`

Core modules:
- `UnityClient/Assets/Scripts/GameCore/enemy/`
- `UnityClient/Assets/Scripts/GameCore/player/`
- `UnityClient/Assets/Scripts/GameCore/manager/`
- `UnityClient/Assets/Scripts/GameCore/room/`
- `UnityClient/Assets/Scripts/GameCore/equipment/`

## Game Result Rules

- Lose condition: player HP reaches 0.
- Win condition: defeat the final boss room.
- Game over UI now supports two outcomes: `YOU DIE` and `YOU WIN`.

## Development Workflow

1. Open `UnityClient/` in Unity Editor.
2. Edit gameplay code in `Assets/Scripts/GameCore/`.
3. Press Play to validate behavior.
4. Commit Unity source changes directly.

## Screenshots

### Start Screen

![Start Screen](Imgs/StartScreen.png)

### Game Start

![Game Start](Imgs/GameStart.png)

### Gameplay

![Gameplay](Imgs/Gameplay.png)

### Game Over

![Game Over](Imgs/GameOver.png)

### Victory

![Victory](Imgs/Win.png)

### UML Diagram

![UML Diagram](Imgs/UML.drawio.png)

## Project Structure

```
CSharp-Roguelike-Dungeon/
|- UnityClient/
|  |- Assets/
|  |  |- Scripts/
|  |  |  |- GameCore/
|- Docs/
|- Imgs/
|- README.md
```

Last updated: 2026-04-06
