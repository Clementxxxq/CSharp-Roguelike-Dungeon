## Roguelike Dungeon (Unity-Only)

This project now uses a Unity-only architecture.

Game logic that previously lived in a separate .NET backend was migrated into Unity source files.
No external backend DLL build is required anymore.

## Current Architecture

- Runtime and gameplay: Unity
- Game core code: `UnityClient/Assets/Scripts/GameCore/`
- UI and scene scripts: `UnityClient/Assets/Scripts/`

Core modules now live under:
- `UnityClient/Assets/Scripts/GameCore/enemy/`
- `UnityClient/Assets/Scripts/GameCore/player/`
- `UnityClient/Assets/Scripts/GameCore/manager/`
- `UnityClient/Assets/Scripts/GameCore/room/`
- `UnityClient/Assets/Scripts/GameCore/equipment/`

## What Changed

- Removed dependency on `Assets/Plugins/RoguelikeDungeonSimulator.dll`
- Moved core gameplay source code into Unity project
- Deprecated standalone backend project workflow

## Development Workflow

1. Open `UnityClient/` in Unity Editor.
2. Edit gameplay code in `Assets/Scripts/GameCore/`.
3. Press Play to validate behavior.
4. Commit Unity source changes directly.

## Project Structure

```
CSharp-Roguelike-Dungeon/
|- UnityClient/
|  |- Assets/
|  |  |- Scripts/
|  |  |  |- GameCore/
|- Docs/
|- README.md
```

Last updated: 2026-03-31
