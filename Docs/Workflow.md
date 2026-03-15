# Workflow (Updated 2026-03-10)

## 1. Goal

Build a playable vertical slice of the roguelike using:
- Backend: C#/.NET game rules and state
- Frontend: Unity 2D presentation and input
- Integration: direct assembly reference first, network/API later

---

## 2. Current Project Snapshot

### Backend (`RoguelikeDungeonSimulator/`)

Implemented:
- Domain entities and interfaces: `IEntity.cs`, `IEquipments.cs`, `Enemy.cs`, `Player.cs`, `Rooms.cs`
- Factories and variants: `EnemyFactory.cs`, `EnemyTypes.cs`, `EquipmentFactory.cs`, `RoomFactory.cs`
- Progression: `Experience.cs`
- Builds successfully with warnings (`dotnet build`)

Missing or incomplete:
- `Program.cs` is still a placeholder (no game loop)
- No `GameManager` orchestration layer yet
- No explicit combat service with turn flow and rewards pipeline
- No test project yet

### Unity (`UnityClient/`)

Implemented:
- Basic movement script: `Assets/Scripts/PlayerMove.cs`
- Asset import work in progress

Missing or incomplete:
- No backend bridge (`BackendManager`) yet
- No gameplay UI (HP, XP, room status)
- No room/encounter rendering pipeline
- No end-to-end playable loop in Unity yet

---

## 3. Development Workflow (Execution-Focused)

### Phase A: Backend Vertical Slice (Priority: P0)

Target outcome:
- Console version can run a full dungeon flow (rooms -> combats -> rewards -> boss -> end).

Tasks:
- Add `GameManager.cs` to orchestrate state transitions.
- Add `CombatSystem.cs` (or equivalent) for deterministic turn-based combat.
- Add reward resolution step after each cleared combat room.
- Replace placeholder `Program.cs` with playable loop.
- Define simple action model (`Attack`, `UseItem`, `EquipReward`, `Continue`).

Definition of done:
- `dotnet run` plays through at least one full run without manual code edits.

Recommended commits:
- `feat: add game manager and run loop`
- `feat: implement turn-based combat flow`
- `feat: add post-combat reward resolution`

### Phase B: Backend Contract for Unity (Priority: P0)

Target outcome:
- Backend exposes stable data contract that Unity can consume.

Tasks:
- Add DTOs for game state snapshot (player stats, current room, enemies, combat flags).
- Add command/result API surface (method calls, not HTTP yet):
    - `GetState()`
    - `ExecuteAction(action)`
- Add JSON serialization helpers for debug/logging.

Definition of done:
- A small console test can serialize and print complete state after each action.

Recommended commits:
- `feat: add game state dto contract`
- `feat: add backend command api surface`

### Phase C: Unity Integration MVP (Priority: P1)

Target outcome:
- Unity can drive one full backend run.

Tasks:
- Add `BackendManager.cs` in Unity to call backend assembly.
- Add minimal UI panel (HP/ATK/DEF/XP/current room).
- Bind one player action button to backend command and refresh UI from returned state.
- Keep `PlayerMove.cs` as visual/controller layer only.

Definition of done:
- Pressing Play in Unity allows entering rooms, triggering combat actions, and reaching game over or victory.

Recommended commits:
- `feat: integrate backend manager in unity`
- `feat: add minimal gameplay hud`
- `feat: bind action flow to backend state`

### Phase D: Stabilization and Tests (Priority: P1)

Tasks:
- Create backend test project and add tests for:
    - Damage calculation
    - Level up logic
    - Enemy/room factory generation rules
- Fix nullable warnings in `Player.cs` (non-null init strategy)
- Balance values after gameplay testing

Definition of done:
- Core combat/progression tests pass and warnings are reduced.

Recommended commits:
- `test: add combat and progression unit tests`
- `fix: resolve nullable warnings in player model`

---

## 4. Daily Working Loop

1. Pull latest changes.
2. Pick one small vertical task (max half-day).
3. Implement + run local checks.
4. Commit with Conventional Commit message.
5. Update docs if behavior changed.

Backend check command:
```bash
cd RoguelikeDungeonSimulator
dotnet build RoguelikeDungeonSimulator.sln
dotnet run
```

---

## 5. Immediate Next Actions (This Week)

- [ ] Create `GameManager.cs` and move run orchestration out of `Program.cs`
- [ ] Implement combat turn loop callable by one public method
- [ ] Add one reward selection flow after combat victory
- [ ] Replace placeholder `Program.cs` with playable run
- [ ] Add first state DTO and `GetState()` method

If these five tasks are done, Unity integration can start immediately with low refactor cost.

