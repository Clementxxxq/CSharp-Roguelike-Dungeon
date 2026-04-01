# 设计模式结构图（GameManager 与 UIManager）

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

## 设计解释

### GameManager

- Singleton：全局唯一实例，方便 UIManager 或其他系统调用。
- State：不同状态（菜单、战斗、游戏结束）封装逻辑，避免过多 if/else。
- Observer：通过事件通知 UIManager 更新，不让 UIManager 直接依赖内部数据。

### UIManager

- Observer：订阅 GameManager 事件，自动更新界面。
- Command：按钮点击封装成命令对象，便于扩展快捷键和宏操作。

## 可行性结论

这套结构可行，适合当前 Roguelike 项目阶段：

- 流程控制集中在 GameManager，职责清晰。
- UI 与游戏核心逻辑解耦，后续改 UI 风险小。
- 行为命令化后，输入系统扩展（键盘、手柄、自动回放）更容易。

## 推荐补充

- 事件参数尽量结构化（例如 HPChangedEventArgs），减少 UI 反查。
- 在 OnEnable/OnDisable 中统一订阅与取消订阅，避免重复绑定。
- GameManager 负责编排，重逻辑继续放在 CombatSystem 与 RoomManager。

## 核心实体设计模式（Enemy / Equipment / Player / Room）

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

- Strategy：将攻击方式（近战、远程、法术）抽象为 `IAttackStyle`，切换武器或职业时无需改 Player 主流程。
- Observer：玩家血量、护盾、经验变化通过事件抛出，UI 与音效系统可独立订阅。

### Equipment

- Decorator：装备词缀（例如 `+火焰伤害`、`+暴击率`）用装饰器叠加，避免子类爆炸。
- Factory Method：装备创建统一走工厂，便于按稀有度、章节和掉落表生成。

### Enemy

- Strategy：AI 行为解耦为策略（激进、防守、巡逻），同一敌人配置即可切换行为。
- Factory：敌人从配置数据批量实例化，支持按房间等级动态缩放属性。

### Room

- Composite：房间与分支关系用组合结构表达，适合多分叉地牢和特殊事件房。
- Builder：将房间生成过程（布局、敌人、奖励、门连接）拆成可复用构建步骤。

## 与现有 GameManager 的衔接建议

- `GameManager` 继续负责状态切换和流程调度，不直接写敌人 AI 或词缀细节。
- `RoomManager` 通过 `DungeonBuilder` 构建地图，再用 `EnemyFactory` 与 `EquipmentFactory` 填充内容。
- `Player` 与 `Enemy` 的属性变化统一发事件，`UIManager` 只做订阅和显示。
