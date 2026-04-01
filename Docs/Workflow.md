# Workflow (Updated 2026-03-31)

## 1. Goal

Ship and iterate the roguelike using a Unity-only workflow.

All gameplay logic is now maintained as Unity source code.
No separate backend build or DLL export is part of the workflow.

---

## 2. Source of Truth

- Unity project: `UnityClient/`
- Core gameplay code: `UnityClient/Assets/Scripts/GameCore/`
- Supporting docs: `Docs/`

GameCore folders:
- `enemy/`
- `player/`
- `manager/`
- `room/`
- `equipment/`

---

## 3. Daily Development Loop

1. Open `UnityClient/` in Unity Editor.
2. Implement one vertical change in `Assets/Scripts/GameCore/`.
3. Press Play and verify the behavior in-scene.
4. Fix compile/runtime issues in Console.
5. Commit only source changes and related docs.

---

## 4. Engineering Rules

- Do not add back DLL-based game logic integration.
- Prefer source-level refactors in `GameCore` over plugin binaries.
- Keep gameplay logic and UI/controller concerns separated:
    - Logic in `GameCore`
    - Presentation/input in scene scripts
- Update docs when balance rules or gameplay flow changes.

---

## 5. This Week Priorities

- [ ] Replace any remaining DLL-era references in scenes/scripts
- [ ] Add one in-Unity combat debug panel (HP, XP, room, enemy list)
- [ ] Add play mode tests for combat and leveling
- [ ] Tune enemy/player values directly in `GameCore`

If these are done, iteration speed stays high without backend coupling.

