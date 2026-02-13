## Simulateur de Donjon Roguelike (Unity 2D)

Un jeu roguelike 2D en vue du dessus (top-down) avec génération procédurale, combats au tour par tour et système de récompenses. Le projet utilise **Unity** pour le frontend et **C# .NET** pour la logique métier.

### Objectif du projet
Ce projet vise à démontrer :
- La conception d'une architecture client-serveur avec **séparation nette** entre frontend et backend
- La programmation orientée objet et les principes SOLID
- Le développement de jeux en 2D avec Unity
- L'intégration entre C# .NET (backend) et Unity (frontend)

### Fonctionnalités
- 🎮 Interface Unity 2D en vue du dessus
- 🏰 Génération procédurale des salles de donjon
- ⚔️ Combat au tour par tour avec animation
- 💰 Système de récompenses et progression du joueur
- 🎨 Graphismes simples et efficaces
- 🔌 Architecture modulaire (backend + frontend séparé)

### Structure de la Carte
```
[ P ] - [ ? ] - [ E ] - [ B ]
```
Où:
- **P** (Joueur) = Position du joueur
- **?** (Mystère) = Salles inconnues à explorer
- **E** (Ennemi) = Rencontres d'ennemis
- **B** (Boss) = Combat final contre le boss

### Technologie

**Backend (Logique métier)**
- C# (.NET 6.0+)
- Architecture verticale (Models, Systems, Utilities)
- Principes SOLID et Programmation orientée objet

**Frontend (Client)**
- Unity 2D
- Système de composants (ECS pattern)
- Interface utilisateur avec UI Toolkit

**Communication**
- Sérialisation JSON pour l'échange de données
- API simple ou événementiel

### Prérequis

**Backend**
- .NET 6.0 ou supérieur
- Visual Studio ou Visual Studio Code

**Frontend**
- Unity 2023.2 LTS ou supérieur
- Module 2D installé dans Unity

**Général**
- Git pour le contrôle de version
- Système d'exploitation : Windows, macOS, ou Linux

### Règles du Jeu
- Le joueur traverse un donjon en avançant de salle en salle
- À chaque salle, le joueur peut rencontrer un ennemi ou une récompense
- Le combat se fait au tour par tour : le joueur et l'ennemi attaquent alternativement
- Chaque victoire octroie des points et des récompenses
- L'objectif final est de vaincre le boss à la fin du donjon

### Structure du Projet

#### Vue d'ensemble
```
CSharp-Roguelike-Dungeon/
├── RoguelikeDungeonSimulator/    # Backend C# .NET
├── RoguelikeFrontend/            # Frontend Unity (à créer)
├── Docs/                          # Documentation
├── README.md
└── .gitignore
```

#### Architecture détaillée

**Backend (RoguelikeDungeonSimulator/)**
```
RoguelikeDungeonSimulator/
├── Models/
│   ├── Player.cs         # Entité joueur
│   ├── Enemy.cs          # Entité ennemi
│   ├── Room.cs           # Entité salle
│   └── Item.cs           # Entité objet
├── Systems/
│   ├── DungeonGenerator.cs    # Génération procédurale
│   ├── CombatSystem.cs        # Logique de combat
│   ├── RewardSystem.cs        # Gestion des récompenses
│   └── GameManager.cs         # Gestionnaire global
├── Utilities/
│   ├── RandomGenerator.cs     # Nombres aléatoires
│   ├── Constants.cs           # Constantes
│   └── Logger.cs              # Logging
├── Program.cs            # Point d'entrée
└── RoguelikeDungeonSimulator.csproj
```

**Frontend (RoguelikeFrontend/) - À créer**
```
RoguelikeFrontend/
├── Assets/
│   ├── Scripts/
│   │   ├── UI/               # Interface et écrans
│   │   ├── Managers/         # Gestionnaires (GameManager, BackendManager)
│   │   ├── Entities/         # Entités visuelles (Player, Enemy, Tile)
│   │   ├── Systems/          # Systèmes (Input, Rendering, Animation)
│   │   └── Utils/            # Utilitaires
│   ├── Prefabs/              # Préfabriqués (Joueur, Ennemi, Salle)
│   ├── Scenes/               # Scènes (MainMenu, DungeonLevel, GameOver)
│   ├── Sprites/              # Assets graphiques
│   └── Audio/                # Sons et musiques
└── ProjectSettings/          # Configurations Unity
```

### Comment Jouer

**1. Lancer le backend** (optionnel, selon l'architecture)
```bash
cd RoguelikeDungeonSimulator
dotnet run
```

**2. Lancer le jeu** 
- Ouvrir le projet Unity dans `RoguelikeFrontend/`
- Appuyer sur Play dans l'éditeur
- Utiliser les flèches ou WASD pour se déplacer
- Appuyer sur Entrée/Space pour attaquer

**3. Objectif**
- Explorer le donjon en avançant de salle en salle
- Vaincre les ennemis en combats au tour par tour
- Atteindre le boss final et le vaincre

### État du Développement
🔄 Projet d'apprentissage en cours
- **Backend** : En cours (Systèmes fondamentaux )
- **Frontend** : À démarrer (Architecture 0%)
- Complétude globale : 
- Dernière mise à jour : 2026-02-13

### Prochaines étapes
1. ✅ Finaliser backend C#
2. ⏳ Créer projet Unity (frontend)
3. ⏳ Implémenter système de rendu 2D
4. ⏳ Intégrer backend avec frontend
5. ⏳ Tester et équilibrer
