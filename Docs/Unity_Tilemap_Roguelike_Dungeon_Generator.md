# Unity Tilemap 随机 Roguelike 地牢生成器（经典版）

更新时间：2026-03-11

本教程提供一个可以直接在 Unity 2D Tilemap 中运行的经典地牢生成器，特性包括：

- 随机房间
- 自动连接走廊
- 自动绘制墙和地板

目标效果示意：

```text
###############
#      ##     #
#      ##     #
#             #
####     ######
#             #
#      ##     #
###############
```

---

## 1. 准备 Tilemap

在 Unity 中创建：

```text
GameObject
 -> 2D Object
   -> Tilemap
     -> Rectangular
```

Unity 会生成：

```text
Grid
 └ Tilemap
```

然后准备两个 Tile 资源：

- floorTile（地板）
- wallTile（墙）

后续会在 Inspector 中拖拽到脚本字段。

---

## 2. 创建脚本

在 Unity 工程中创建脚本文件：

```text
DungeonGenerator.cs
```

这里只保留核心思路代码，避免放整份实现：

### 2.1 核心字段

```csharp
public int width = 60, height = 40;
public int roomCount = 8, minRoomSize = 4, maxRoomSize = 8;
public Tilemap tilemap;
public Tile floorTile, wallTile;

int[,] map;
List<RectInt> rooms = new List<RectInt>();
```

### 2.2 生成主流程

```csharp
void Start()
{
    GenerateDungeon();
    DrawMap();
}
```

### 2.3 房间连接核心

```csharp
void ConnectRooms(RectInt roomA, RectInt roomB)
{
    Vector2Int a = roomA.center;
    Vector2Int b = roomB.center;

    int x = a.x;
    int y = a.y;

    while (x != b.x)
    {
        map[x, y] = 1;
        x += (b.x > x) ? 1 : -1;
    }

    while (y != b.y)
    {
        map[x, y] = 1;
        y += (b.y > y) ? 1 : -1;
    }
}
```

### 2.4 Tilemap 绘制核心

```csharp
void DrawMap()
{
    tilemap.ClearAllTiles();

    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            var pos = new Vector3Int(x, y, 0);
            tilemap.SetTile(pos, map[x, y] == 1 ? floorTile : wallTile);
        }
    }
}
```

说明：

- map[x, y] == 1 表示地板，0 表示墙。
- 先生成房间，再连接走廊，最后统一绘制。

---

## 3. 挂载脚本与 Inspector 设置

1. 在场景中创建一个空物体：

```text
DungeonGenerator
```

2. 把 DungeonGenerator 脚本挂到该物体。
3. 在 Inspector 中设置字段：

```text
Tilemap
Floor Tile
Wall Tile
```

把对应资源拖入即可。

---

## 4. 运行效果

点击 Play 后，每次会自动生成：

- 随机房间
- 随机走廊
- 随机布局

这就是 Roguelike 的经典地牢底图生成。

---

## 5. 可选升级功能

### 5.1 玩家出生点

```csharp
Vector2Int spawn = rooms[0].center;
```

将玩家实例生成在第一个房间中心。

### 5.2 出口（楼梯）

```csharp
Vector2Int exit = rooms[rooms.Count - 1].center;
```

在最后一个房间中心放置出口。

### 5.3 敌人生成

在每个房间中按概率生成敌人，避免出生点房间与出口房间过于拥挤。

### 5.4 宝箱生成

随机选择若干房间，生成宝箱或奖励点。

---

## 6. 与当前项目后端系统对接建议

你当前后端已有 Rooms / EnemyFactory / Combat / Reward 等核心模块，推荐流程如下：

```text
生成房间
↓
进入房间
↓
触发 CombatSystem
↓
胜利结算 Reward
↓
开门进入下一房间
```

建议先完成“地图生成 + 房间进入触发”的最小闭环，再接入完整战斗与奖励系统。

---

## 7. 常见问题（FAQ）

- 看不到 Tile：
  - 检查 Tilemap 字段是否已绑定。
  - 检查 floorTile 与 wallTile 是否拖入。
  - 检查 Camera 是否覆盖到 Tilemap 生成区域。

- 地图全是墙或全是地板：
  - 检查 width / height / roomCount 是否合理。
  - 检查 minRoomSize 与 maxRoomSize 是否过大导致无法放置房间。

- 每次地图都一样：
  - 当前实现使用 UnityEngine.Random，默认是随机种子。
  - 若你手动设置了固定种子，需确认是否重复设置。

---

## 8. 下一步（进阶版方向）

进阶版可继续加入：

- 墙体边缘自动化（根据邻居格子自动选墙体样式）
- 门的自动生成与开关逻辑
- 小地图系统
- 更细粒度的房间类型（战斗房、事件房、商店房、Boss 房）

该版本可以保持在约 200 行核心逻辑内，仍便于维护和扩展。
