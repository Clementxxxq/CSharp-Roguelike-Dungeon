# Workflow de Développement

## Simulateur de Donjon Roguelike (Console C#)

Ce document décrit le processus de développement du projet de manière itérative et progressive.

---

## Phase 1️⃣ : Initialisation du projet

**Objectifs :**
- Créer la structure du projet C# (.NET)
- Initialiser le contrôle de version Git
- Mettre en place l'architecture de base

**Tâches :**
- Créer le projet C# avec `dotnet new console`
- Initialiser le dépôt Git
- Créer la structure de dossiers (Models, Systems, UI, Utilities)
- Ajouter README.md et WORKFLOW.md

**Commit :** `chore: initialize project structure`

---

## Phase 2️⃣ : Implémentation des entités de base

**Modules :** Models (Player, Enemy, Room)

**Objectifs :**
- Créer les classes fondamentales du jeu
- Implémenter les propriétés et méthodes de base

**Classes à implémenter :**
- `Player.cs` — Joueur avec PV, attaque, inventaire
- `Enemy.cs` — Ennemi avec PV, attaque, récompense
- `Room.cs` — Salle avec contenu et difficulté

**Commit :** `feat: add base game entities (player, enemy, room)`

---

## Phase 3️⃣ : Système de combat

**Module :** Systems (CombatSystem.cs)

**Objectifs :**
- Implémenter un combat au tour par tour
- Gérer la logique de vie et de mort

**Fonctionnalités :**
- Le joueur attaque en premier
- Échange d'attaques jusqu'à la mort d'un combattant
- Gestion de la victoire/défaite

**Commit :** `feat: implement turn-based combat system`

---

## Phase 4️⃣ : Génération du donjon

**Module :** Systems (DungeonGenerator.cs)

**Objectifs :**
- Créer une génération procédurale simple
- Établir une progression linéaire

**Fonctionnalités :**
- Générer une liste de salles
- Identifier la salle finale (Boss)
- Permettre la progression salle par salle

**Commit :** `feat: add procedural dungeon structure`

---

## Phase 5️⃣ : Système de récompenses

**Module :** Systems (RewardSystem.cs)

**Objectifs :**
- Créer un système d'amélioration du joueur
- Ajouter de la progression et de la motivation

**Fonctionnalités :**
- Récompenses aléatoires après un combat
- Amélioration des statistiques (+HP, +Attaque)
- Choix simples pour le joueur

**Commit :** `feat: add random reward system`

---

## Phase 6️⃣ : Interface utilisateur console

**Module :** UI (GameDisplay.cs, InputHandler.cs)

**Objectifs :**
- Créer une interface claire et lisible
- Permettre l'interaction avec le joueur

**Fonctionnalités :**
- Affichage des informations de combat
- Gestion des entrées clavier
- Représentation ASCII du donjon
- Messages clairs et instructions

**Commit :** `feat: implement console user interface`

---

## Phase 7️⃣ : Intégration et boucle principale

**Module :** Program.cs (GameManager.cs)

**Objectifs :**
- Connecter tous les systèmes
- Créer la boucle de jeu principale

**Fonctionnalités :**
- Gestion de la progression globale
- Boucle de jeu (menu → dungeon → résultat)
- Sauvegarde/chargement basique

**Commit :** `feat: integrate game systems and main loop`

---

## Phase 8️⃣ : Tests et équilibrage

**Objectifs :**
- Vérifier la jouabilité complète
- Équilibrer les valeurs de jeu

**Tâches :**
- Tests manuels complets
- Équilibrage des PV, dégâts, récompenses
- Correction des bugs logiques
- Vérification de la progression du donjon

**Commit :** `fix: balance game values and fix logic issues`

---

## Phase 9️⃣ : Documentation et nettoyage

**Objectifs :**
- Améliorer la lisibilité du code
- Documenter les classes principales

**Tâches :**
- Ajouter des commentaires XML sur les classes
- Simplifier le code redondant
- Supprimer le code mort
- Mettre à jour README.md et WORKFLOW.md

**Commit :** `docs: add code comments and update documentation`

---

## Phase 10️⃣ : Livraison finale

**Objectifs :**
- Préparer le projet pour la présentation

**État final :**
- ✅ Jeu jouable de bout en bout
- ✅ Architecture claire et modulaire
- ✅ Code documenté et nettoyé
- ✅ Prêt pour portfolio ou projet académique

**Améliorations futures possibles :**
- 🔮 Système de sauvegarde persistant
- 🔮 Types de salles supplémentaires
- 🔮 Compétences spéciales du joueur
- 🔮 IA ennemie plus avancée

