## Simulateur de Donjon Roguelike (Unity + C#)

Projet d'apprentissage roguelike avec séparation claire :
- backend en C#/.NET pour la logique de jeu,
- frontend Unity pour la présentation.

Etat actuel : le backend console est jouable de bout en bout, l'integration Unity est encore en cours.

### Objectifs
- Pratiquer la conception orientee objet (SOLID, factories, encapsulation).
- Construire une boucle de jeu complete (combat, progression, recompenses, boss).
- Prepararer une architecture backend/frontend facilement integrable.

### Fonctionnalites

Backend (implante)
- Generation de salles (combat + boss).
- Ennemis avec difficulte progressive.
- Combat au tour par tour (cibles, degats, victoire/defaite).
- Systeme XP + niveau.
- Systeme d'equipement (arme, armure, anneau) avec bonus.
- Recompense apres chaque salle (equipement ou soin).
- Boucle complete : salles -> combats -> boss -> fin de partie.

Frontend Unity (partiel)
- Prototype de deplacement joueur (`PlayerMove.cs`).
- Assets et scenes Unity en preparation.

### Structure de la progression

Le run actuel suit ce flux:
```
[ Combat 1 ] -> [ Combat 2 ] -> [ Combat 3 ] -> [ Combat 4 ] -> [ Boss ]
```

### Technologies

- Backend : C# / .NET (`net9.0`)
- Frontend : Unity 2D
- Outils : VS Code ou Visual Studio, Git

### Prerequis

- .NET SDK 9.0+ (ou version compatible avec `net9.0`)
- Unity (pour la partie client)

### Structure du projet

```
CSharp-Roguelike-Dungeon/
├── RoguelikeDungeonSimulator/     # Backend jouable en console
├── UnityClient/                   # Projet Unity (integration en cours)
├── Docs/                          # Architecture et workflow
└── README.md
```

### Backend actuel (fichiers principaux)

```
RoguelikeDungeonSimulator/
├── Program.cs              # Entree console + boucle de rejouabilite
├── GameManager.cs          # Orchestration du run (rooms, rewards, end)
├── CombatSystem.cs         # Tour par tour et resolution de combat
├── Player.cs               # Joueur, stats, equipements, progression
├── Enemy.cs                # Base ennemi
├── EnemyTypes.cs           # Types concrets (Rat, Guerrier, Mage, Boss)
├── EnemyFactory.cs         # Creation d'ennemis
├── Rooms.cs                # Base Room + types
├── CombatRoom.cs
├── BossRoom.cs
├── RoomFactory.cs          # Generation des salles
├── Equipment.cs            # Equipements concrets
├── EquipmentFactory.cs     # Factories par rarete
├── Experience.cs           # Calcul XP/niveau
└── RoguelikeDungeonSimulator.csproj
```

### Lancer le backend (console jouable)

```bash
cd RoguelikeDungeonSimulator
dotnet build RoguelikeDungeonSimulator.sln
dotnet run
```

Pendant le jeu:
- choisir un nom de joueur,
- choisir des actions de combat (attaquer, voir equipement, passer),
- choisir une recompense entre les salles,
- vaincre le boss pour gagner.

### Etat du developpement

- Backend : termine pour une vertical slice console jouable
- Unity : prototype de mouvement uniquement, pas encore connecte au backend
- Tests automatiques : non ajoutes pour l'instant

Derniere mise a jour : 2026-03-10

### Prochaines etapes

1. Ajouter des tests backend (combat, XP, factories).
2. Exposer un contrat de donnees backend (GetState/ExecuteAction).
3. Connecter Unity au backend via un `BackendManager`.
4. Ajouter une UI de combat/progression dans Unity.
