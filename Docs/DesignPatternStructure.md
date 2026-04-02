# Design Pattern Structure Diagram (GameManager and UIManager)

```text
┌─────────────────────┐
│     GameManager      │  <-- Singleton
│─────────────────────│
│ - Player player      │
│ - RoomManager roomM  │
│ - CombatSystem combat│
│─────────────────────│
│ + StartGame()        │
│ + EnterCurrentRoom() │
│ + RestartGame()      │
│─────────────────────│
│ <<State Pattern>>    │
│ - IGameState state   │
│   ├─ MenuState       │
│   ├─ PlayingState    │
│   └─ GameOverState   │
│─────────────────────│
│ <<Observer Pattern>> │
│ - event OnHPChanged  │
│ - event OnRoomChange │
└─────────┬───────────┘
          │ broadcasts events
          ▼
┌─────────────────────┐
│      UIManager       │
│─────────────────────│
│ - Text playerText    │
│ - Text enemyText     │
│ - Text logText       │
│ - Button start/attack│
│─────────────────────│
│ <<Observer Pattern>> │
│ - Subscribes to      │
│   OnHPChanged        │
│   OnRoomChange       │
│─────────────────────│
│ <<Command Pattern>>  │
│ - StartCommand       │
│ - AttackCommand      │
│ - RestartCommand     │
└─────────────────────┘
```

## Design Notes

### GameManager

- Singleton: A globally unique instance, convenient for UIManager and other systems to access.
- State: Encapsulates logic for different states (menu, combat, game over), avoiding too many if/else branches.
- Observer: Notifies UIManager through events, so UIManager does not directly depend on internal data.

### UIManager

- Observer: Subscribes to GameManager events and updates UI automatically.
- Command: Wraps button clicks into command objects for easier extension to hotkeys and macros.

## Feasibility Conclusion

This structure is practical and fits the current stage of the Roguelike project:

- Flow control is centralized in GameManager, with clear responsibilities.
- UI is decoupled from core game logic, reducing risk for future UI changes.
- Once actions are command-based, input expansion (keyboard, gamepad, auto replay) becomes easier.

## Recommended Additions

- Keep event args structured (for example, HPChangedEventArgs) to reduce UI-side lookups.
- Centralize subscribe/unsubscribe in OnEnable/OnDisable to avoid duplicate bindings.
- Let GameManager orchestrate only, while heavy logic remains in CombatSystem and RoomManager.

## Core Entity Patterns (Enemy / Equipment / Player / Room)

```text
┌─────────────────────┐
│       Player         │
│─────────────────────│
│ - BaseStats stats    │
│ - List<Equipment> eq │
│ - IAttackStyle style │
│─────────────────────│
│ <<Strategy>>         │
│ - MeleeStyle         │
│ - RangedStyle        │
│ - MagicStyle         │
│─────────────────────│
│ <<Observer>>         │
│ - event OnPlayerHP   │
└─────────┬───────────┘
          │ equips
          ▼
┌─────────────────────┐
│      Equipment       │
│─────────────────────│
│ - name, rarity       │
│ - IStatModifier mod  │
│─────────────────────│
│ <<Decorator>>        │
│ - BaseEquipment      │
│ - PrefixDecorator    │
│ - SuffixDecorator    │
│─────────────────────│
│ <<Factory Method>>   │
│ - EquipmentFactory   │
└─────────────────────┘

┌─────────────────────┐
│        Enemy         │
│─────────────────────│
│ - EnemyData data     │
│ - IEnemyAI ai        │
│ - IDropTable drops   │
│─────────────────────│
│ <<Strategy>>         │
│ - AggressiveAI       │
│ - DefensiveAI        │
│ - PatrolAI           │
│─────────────────────│
│ <<Factory>>          │
│ - EnemyFactory       │
└─────────┬───────────┘
          │ spawns in
          ▼
┌─────────────────────┐
│         Room         │
│─────────────────────│
│ - id, type           │
│ - List<Enemy> enemies│
│ - List<Loot> loots   │
│ - List<Room> links   │
│─────────────────────│
│ <<Composite>>        │
│ - NodeRoom           │
│ - BranchRoom         │
│─────────────────────│
│ <<Builder>>          │
│ - RoomBuilder        │
│ - DungeonBuilder     │
└─────────────────────┘
```

### Player

- Strategy: Abstract attack styles (melee, ranged, magic) behind `IAttackStyle`, so weapon/class switches do not alter the Player main flow.
- Observer: Emit player HP, shield, and XP changes as events, allowing UI and audio systems to subscribe independently.

### Equipment

- Decorator: Stack equipment affixes (for example, `+Fire Damage`, `+Critical Chance`) with decorators to avoid subclass explosion.
- Factory Method: Centralize equipment creation via factory methods to generate by rarity, chapter, and drop table.

### Enemy

- Strategy: Decouple AI behaviors into strategies (aggressive, defensive, patrol), enabling behavior swaps on the same enemy config.
- Factory: Instantiate enemies in batches from config data, with dynamic stat scaling by room level.

### Room

- Composite: Represent rooms and branches as a composite structure, suitable for multi-branch dungeons and special event rooms.
- Builder: Split room generation (layout, enemies, rewards, door links) into reusable build steps.

## Integration Suggestions With Current GameManager

- `GameManager` should continue handling state transitions and flow scheduling, without directly implementing enemy AI or affix details.
- `RoomManager` can build maps via `DungeonBuilder`, then fill content with `EnemyFactory` and `EquipmentFactory`.
- `Player` and `Enemy` stat changes should emit events in a unified way, while `UIManager` only subscribes and displays.
