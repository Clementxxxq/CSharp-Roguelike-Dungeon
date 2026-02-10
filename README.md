## Simulateur de Donjon Roguelike (Console C#)

Un jeu de donjon roguelike basé sur la console avec des salles procédurales, des combats au tour par tour et des récompenses aléatoires.

### Objectif du projet
Ce projet vise à démontrer la conception d'un jeu roguelike simple en console,
en mettant l'accent sur la structure du code, la programmation orientée objet
et la séparation des responsabilités.

### Fonctionnalités
- 🎮 Jeu roguelike en console
- 🏰 Génération procédurale de salles
- ⚔️ Combat au tour par tour (joueur vs ennemi)
- 💰 Système de récompenses aléatoires

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
- C# (.NET)
- Programmation orientée objet (POO)
- Principes SOLID
- Architecture modulaire (séparation des systèmes)

### Prérequis
- .NET 6.0 ou supérieur
- Visual Studio, Visual Studio Code ou tout éditeur C#
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
Roguelike-Dungeon/
├── Models/           # Entités du jeu (Player, Enemy, Room...)
├── Systems/          # Logique de jeu (Combat, Dungeon...)
├── UI/              # Affichage console
├── Utilities/       # Fonctions utilitaires
└── Program.cs       # Point d'entrée
```

#### Architecture détaillée
```
Roguelike-Dungeon/
├── Models/
│   ├── Player.cs         # Classe joueur (PV, statistiques, inventaire)
│   ├── Enemy.cs          # Classe ennemi (PV, attaque, récompense)
│   ├── Room.cs           # Classe salle (contenu, difficulté)
│   └── Item.cs           # Classe objet (équipement, consommables)
├── Systems/
│   ├── DungeonGenerator.cs    # Génération procédurale des salles
│   ├── CombatSystem.cs        # Logique de combat au tour par tour
│   ├── RewardSystem.cs        # Gestion des récompenses
│   └── GameManager.cs         # Gestionnaire principal du jeu
├── UI/
│   ├── GameDisplay.cs    # Affichage des écrans
│   ├── InputHandler.cs   # Gestion des entrées utilisateur
│   └── Messages.cs       # Messages et dialogues
├── Utilities/
│   ├── RandomGenerator.cs     # Générateur de nombres aléatoires
│   ├── Constants.cs           # Constantes du jeu
│   └── Logger.cs              # Système de logging
├── Program.cs            # Boucle principale du jeu
├── Roguelike-Dungeon.csproj   # Fichier de configuration C#
└── README.md             # Documentation
```

### Comment Jouer
```bash
dotnet run
```
Puis suivez les instructions affichées dans la console pour explorer le donjon et combattre les ennemis.

### État du Développement
🔄 Projet d'apprentissage en cours
- Phase : Implémentation des systèmes fondamentaux
- Complétude : ~70%
- Dernière mise à jour : 2026
