# Workflow de Développement

## Simulateur de Donjon Roguelike (Unity 2D + C# .NET)

Ce document décrit le processus de développement du projet avec une **architecture client-serveur** : backend C# .NET + frontend Unity 2D.

---

## 🎯 Architecture Globale

```
┌─────────────────────────────────────────────┐
│         Frontend (Unity 2D)                 │
│  - Rendu graphique (top-down view)          │
│  - Interface utilisateur                    │
│  - Gestion des entrées                      │
└─────────────────┬───────────────────────────┘
                  │ JSON / Events
                  ▼
┌─────────────────────────────────────────────┐
│      Backend (C# .NET)                      │
│  - Logique de jeu                           │
│  - Génération procédurale                   │
│  - Systèmes de combat                       │
│  - Gestion des récompenses                  │
└─────────────────────────────────────────────┘
```

**Séparation des responsabilités :**
- **Backend** = Données et logique métier (state, règles, validation)
- **Frontend** = Présentation et interaction (visuel, input, animation)

---

## 📋 Phases de Développement

---

## GROUPE 1️⃣ : BACKEND C# .NET

### Phase 1.1 : Initialisation du backend

**Objectifs :**
- Créer le projet C# (.NET)
- Initialiser Git
- Mettre en place la structure de base

**Tâches :**
- `dotnet new console` pour créer le projet
- Créer les dossiers : Models, Systems, UI, Utilities
- Configuration `.gitignore` et `README.md`

**Commit :** `chore: initialize backend project structure`

---

### Phase 1.2 : Implémentation des entités de base

**Module :** Models/

**Classes à créer :**
- `Player.cs` — PV, statistiques, inventaire
- `Enemy.cs` — PV, attaque, récompense
- `Room.cs` — Contenu, difficulté
- `Item.cs` — Équipement, consommables

**Commit :** `feat: add base game entities (player, enemy, room)`

---

### Phase 1.3 : Système de combat

**Module :** Systems/CombatSystem.cs

**Fonctionnalités :**
- Combat au tour par tour
- Le joueur attaque en premier
- Gestion de la vie et de la mort
- Calcul des dégâts

**Commit :** `feat: implement turn-based combat system`

---

### Phase 1.4 : Génération du donjon

**Module :** Systems/DungeonGenerator.cs

**Fonctionnalités :**
- Génération procédurale des salles
- Progression linéaire
- Identification du boss final

**Commit :** `feat: add procedural dungeon generation`

---

### Phase 1.5 : Système de récompenses

**Module :** Systems/RewardSystem.cs

**Fonctionnalités :**
- Récompenses aléatoires après victoire
- Amélioration des stats (+HP, +Attaque)
- Choix de récompenses

**Commit :** `feat: add random reward system`

---

### Phase 1.6 : Backend complet et API

**Module :** Program.cs / GameManager.cs

**Objectifs :**
- Intégrer tous les systèmes
- Créer une API / Interface pour le frontend
- Sérialisation JSON des données

**Fonctionnalités :**
- Méthodes publiques pour le frontend (GetGameState, MakeAction, etc.)
- Serialization des entités en JSON
- Logging complet

**Commit :** `feat: create backend API and game manager`

---

## GROUPE 2️⃣ : FRONTEND UNITY 2D

### Phase 2.1 : Setup du projet Unity

**Objectifs :**
- Créer le projet Unity 2D
- Configurer les réglages de base
- Importer le backend C#

**Tâches :**
- Créer le projet avec Unity 2024.3+ LTS
- Organiser les dossiers : Scripts, Sprites, Prefabs, Scenes
- Ajouter le backend en tant que DLL ou référence

**Commit :** `chore: setup unity project structure`

---

### Phase 2.2 : Système de caméra et grille

**Module :** Systems/CameraSystem.cs

**Objectifs :**
- Implémenter une caméra top-down
- Système de grille pour le mouvement

**Fonctionnalités :**
- Caméra qui suit le joueur
- Grille de tuiles (tile grid)
- Positionnement des objets

**Commit :** `feat: implement camera and tile grid system`

---

### Phase 2.3 : Rendu des entités visuelles

**Module :** Entities/ (PlayerView, EnemyView, TileView)

**Objectifs :**
- Afficher le joueur, ennemis, salles
- Animation simple des déplacements

**Composants :**
- Sprite renderer pour chaque entité
- Animation de mouvement
- Gestion des collisions

**Commit :** `feat: implement visual entities and rendering`

---

### Phase 2.4 : Système d'input et interaction

**Module :** Systems/InputHandler.cs

**Objectifs :**
- Récupérer les entrées au clavier
- Envoyer les actions au backend

**Fonctionnalités :**
- Mouvements (WASD / Flèches)
- Actions de combat (Space / Enter)
- Menu de pause (Esc)

**Commit :** `feat: implement input handling system`

---

### Phase 2.5 : Interface utilisateur (UI)

**Module :** UI/

**Écrans à créer :**
- `MainMenuUI.cs` — Menu principal
- `DungeonUI.cs` — Affichage combat en cours (PV, stats)
- `GameOverUI.cs` — Écran fin de jeu
- `PauseMenuUI.cs` — Menu pause

**Utiliser :** Canvas + UI Toolkit d'Unity

**Commit :** `feat: implement game UI and menus`

---

### Phase 2.6 : Communication avec le backend

**Module :** Managers/BackendManager.cs

**Objectifs :**
- Intégrer la logique du backend
- Synchroniser state entre backend et frontend

**Fonctionnalités :**
- Appeler les méthodes du backend C#
- Mettre à jour l'état du jeu
- Gérer les événements (combat, récompense, fin)

**Commit :** `feat: integrate backend with frontend`

---

### Phase 2.7 : Affichage des animations

**Module :** Systems/AnimationSystem.cs

**Animations à implémenter :**
- Déplacement du joueur et des ennemis
- Attaques et impacts
- Réactions aux dégâts
- Transitions de scènes

**Commit :** `feat: add animation system`

---

### Phase 2.8 : Effets visuels et audio

**Module :** Systems/EffectsSystem.cs

**Éléments à ajouter :**
- Effets visuels simples (flashes, particles)
- Sons de combat
- Musique de fond

**Commit :** `feat: add sound and visual effects`

---

## GROUPE 3️⃣ : INTÉGRATION ET FINITION

### Phase 3.1 : Tests manuels complets

**Objectifs :**
- Tester le jeu de bout en bout
- Vérifier la jouabilité
- Identifier les bugs

**Tâches :**
- Parcourir plusieurs dungeons complets
- Tester tous les systèmes de combat
- Vérifier les transitions de scènes

**Commit :** `test: manual testing and bug fixes`

---

### Phase 3.2 : Équilibrage et ajustements

**Objectifs :**
- Ajuster la difficulté
- Équilibrer les valeurs de jeu

**Paramètres à ajuster :**
- PV joueur / ennemis
- Dégâts des attaques
- Récompenses
- Nombre de salles

**Commit :** `balance: adjust game difficulty and values`

---

### Phase 3.3 : Polissage final

**Objectifs :**
- Nettoyage du code
- Documentation
- Préparation à la présentation

**Tâches :**
- Refactoriser le code redondant
- Ajouter des commentaires XML
- Mettre à jour README et documentation
- Optimiser les performances

**Commit :** `docs: add documentation and clean codebase`

---

## 📌 Notes importantes

- **Itération** : Chaque phase peut être testée indépendamment
- **Commits** : Utiliser le format **Conventional Commits** (feat:, fix:, docs:, etc.)
- **Architecture** : Backend et Frontend sont **complètement découplés**
- **Extensibilité** : Les systèmes sont conçus pour être facilement extensibles

**Timeline estimée :**
- Backend : 1-2 semaines
- Frontend : 2-3 semaines
- Intégration : 1 semaine
- **Total : 4-6 semaines**

