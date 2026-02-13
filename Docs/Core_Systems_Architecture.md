# Architecture des Systèmes Centraux

## Vue d'ensemble du projet

Ce document décrit l'architecture complète du système de jeu roguelike, en mettant l'accent sur les modèles de conception et la justification architecturale.

---

## 1️⃣ Vue d'ensemble des systèmes centraux

### 🎮 Les objets de jeu principaux

#### Entités joueur et ennemis
- **Player (Joueur)** - L'entité contrôlée par le joueur
- **Enemy (Ennemi)** - 3 types d'ennemis avec des attributs différents
  - Rat des Donjons (difficulté faible)
  - Guerrier Déchu (difficulté moyenne)
  - Mage Corrompu (difficulté élevée, haute puissance)
- **Boss (Chef)** - Seigneur du Donjon (affrontement final)

#### Système d'équipement
- **Weapon (Arme)** - Augmente l'Attaque
  - Épée rouillée (+3 Attaque)
  - Épée du héros (+7 Attaque)
- **Armor (Armure)** - Augmente la Défense
  - Armure légère (+2 Défense)
  - Armure renforcée (+5 Défense)
- **Ring (Anneau)** - Effets spéciaux
  - Anneau de vie (+10 PV max)
  - Anneau de puissance (+3 Attaque)

#### Système d'expérience
- Gain d'XP lors de la défaite d'ennemis
- Système de niveaux avec seuils progressifs
- Augmentation des statistiques à chaque niveau

---

## 2️⃣ Conception du Joueur (Player)

### Attributs du joueur

```
┌─────────────────┐
│ Player          │
├─────────────────┤
│ Statistiques:   │
│  • HP / MaxHP   │
│  • Attack       │
│  • Defense      │
│  • Level        │
│  • Experience   │
│                 │
│ Équipement:     │
│  • Weapon       │
│  • Armor        │
│  • Ring         │
└─────────────────┘
```

### Comportements principaux

| Comportement | Description |
|-------------|-------------|
| `AttackEnemy()` | Infliger des dégâts à un ennemi |
| `TakeDamage()` | Recevoir des dégâts |
| `GainExperience()` | Gagner de l'expérience (XP) |
| `LevelUp()` | Passer au niveau supérieur |
| `EquipItem()` | Équiper un objet |

### Évolution du joueur

Le joueur possède des statistiques évolutives et peut s'améliorer grâce à l'expérience et à l'équipement. Les équipements modifient directement ses attributs de combat.

---

## 3️⃣ Les trois types d'ennemis

### 🐀 Type 1 : Rat des Donjons (Ennemi basique)

**Position dans la courbe de difficulté:** Débutant / Tutoriel

| Attribut | Valeur |
|----------|--------|
| HP | 20 |
| Attaque | 5 |
| Défense | 0 |
| XP donné | 10 |

**Caractéristiques:**
- Attaque faible
- Aucune capacité spéciale
- Conçu pour l'apprentissage

**Utilisation:**
- Tester le système de combat
- Générer les premières salles du donjon

---

### 🧟 Type 2 : Guerrier Déchu (Ennemi moyen)

**Position dans la courbe de difficulté:** Standard

| Attribut | Valeur |
|----------|--------|
| HP | 40 |
| Attaque | 10 |
| Défense | 3 |
| XP donné | 20 |

**Caractéristiques:**
- Équilibré entre attaque et défense
- Plus résistant que l'ennemi basique
- Représente le "combat standard" du jeu

**Utilisation:**
- Remplir les salles intermédiaires du donjon
- Tester la progression du joueur

---

### 🔮 Type 3 : Mage Corrompu (Ennemi stratégique)

**Position dans la courbe de difficulté:** Élevée / Dangereux

| Attribut | Valeur |
|----------|--------|
| HP | 30 |
| Attaque | 15 |
| Défense | 1 |
| XP donné | 30 |

**Caractéristiques:**
- Attaque très élevée
- Faible résistance physique
- Crée de la pression tactique pour le joueur
- Encourage les stratégies de combat différentes

**Utilisation:**
- Jouer le rôle de "boss mineur" dans les salles tardives
- Offrir une variété de défis de combat

---

## 4️⃣ Conception du Boss

### 👑 Boss : Seigneur du Donjon

| Attribut | Valeur |
|----------|--------|
| HP | 120 |
| Attaque | 20 |
| Défense | 5 |
| XP donné | 100 |

### Caractéristiques distinctives

- **Endurance:** Beaucoup plus de points de vie que les ennemis normaux
- **Puissance:** Attaque nettement supérieure
- **Défense:** Meilleure résistance pour élever la difficulté
- **Récompense:** XP considérablement augmentée

### Rôle stratégique

Le boss représente l'aboutissement du donjon et teste directement la progression du joueur. Il valide que le joueur a suffisamment progressé et amassé des ressources pour vaincre le défi final.

### Évolutions futures (optionnel)

- Capacités spéciales
- Phases multiples de combat
- Modération de difficulté basée sur le niveau du joueur

---

## 5️⃣ Système d'équipement (Equipment)

### 🗡️ Armes (Weapon)

**Effet:** Augmente l'Attaque du joueur

| Nom | Bonus Attaque |
|-----|--------------|
| Épée rouillée | +3 |
| Épée du héros | +7 |

### 🛡️ Armures (Armor)

**Effet:** Augmente la Défense du joueur

| Nom | Bonus Défense |
|-----|--------------|
| Armure légère | +2 |
| Armure renforcée | +5 |

### 💍 Anneaux (Ring)

**Effet:** Modifications spéciales aux statistiques

| Nom | Effet |
|-----|-------|
| Anneau de vie | +10 PV max |
| Anneau de puissance | +3 Attaque |

### Mécanique d'équipement

Les objets s'équipent directement et modifient les attributs du joueur instantanément. Un joueur peut porter:
- 1 Arme
- 1 Armure
- 1 Anneau

---

## 6️⃣ Système d'expérience et de niveaux

### Règles d'expérience

```
Vaincre ennemi → Gagner XP → Atteindre seuil → Monter de niveau
```

### Table de progression

| Transition | XP requis |
|------------|-----------|
| Niveau 1 → 2 | 50 |
| Niveau 2 → 3 | 100 |
| Niveau 3 → 4 | 150 |
| Niveau n → n+1 | 50n |

### Récompenses de montée de niveau

À chaque niveau gagné, le joueur obtient:
- **PV Max:** +10
- **Attaque:** +2
- **Défense:** +1

### Progression totale exemple

| Niveau | PV | Attaque | Défense | XP cumulé |
|--------|-----|---------|---------|-----------|
| 1 | 100 | 15 | 5 | 0 |
| 2 | 110 | 17 | 6 | 50 |
| 3 | 120 | 19 | 7 | 150 |
| 4 | 130 | 21 | 8 | 300 |

---

## 7️⃣ Tableau synthétique des modèles de conception

### Vue d'ensemble architecturale

| Module | Modèle de conception recommandé | Justification |
|--------|--------------------------------|---------------|
| **Player** | Singleton (optionnel) | Instance unique accessible globalement |
| **Enemy (3 types)** | Factory Method | Centralise la création des 3 types d'ennemis |
| **Boss** | Factory Method | Utilise la même logique que Enemy |
| **Equipment** | Abstract Factory | Crée des familles d'objets (Weapon, Armor, Ring) |
| **Weapon / Armor / Ring** | Strategy | Chaque type modifie les statistiques différemment |
| **Experience System** | Observer | Notifie les observateurs lors d'une montée de niveau |

**Raison du choix:**
Cette combinaison est la plus courante, la plus sûre et la plus facile à justifier dans un rapport.

---

## 8️⃣ Explication détaillée des modèles de conception

### 1️⃣ Player → Singleton (optionnel, mais recommandé)

#### Problème
- Le jeu ne comporte qu'un seul joueur
- Plusieurs systèmes (UI, Combat, XP) accèdent au joueur
- Risque d'incohérence si plusieurs instances existaient

#### Solution : Singleton
```csharp
public class Player : IEntity
{
    private static Player instance;
    
    public static Player GetInstance()
    {
        if (instance == null)
            instance = new Player();
        return instance;
    }
}
```

#### Avantages
- Garantit une unique instance dans le jeu
- Accessible de manière centraliste
- Évite les bugs liés à la synchronisation

#### Justification pour le rapport
> Le joueur est représenté comme une instance unique accessible globalement, garantissant la cohérence des données d'état du joueur au sein de l'ensemble de l'application.

**Note:** L'utilisation de Singleton est optionnelle mais représente une valeur ajoutée pédagogique.

---

### 2️⃣ Enemy + Boss → Factory Method (point clé)

#### Problème
- 3 types d'ennemis différents + 1 Boss
- Chaque type a des propriétés uniques
- Placer des `new Rat()`, `new Mage()` partout dans le code → mauvaise pratique

#### Solution : EnemyFactory
```
EnemyFactory
 ├── CreateEnemy("Rat") → Rat des Donjons
 ├── CreateEnemy("Warrior") → Guerrier Déchu
 ├── CreateEnemy("Mage") → Mage Corrompu
 └── CreateEnemy("Boss") → Seigneur du Donjon
```

#### Bénéfices architecturaux
- **Centralisation:** Toute la logique de création en un seul endroit
- **Open/Closed Principle:** Ajouter un nouvel ennemi sans modifier le système de combat
- **Testabilité:** Facile de créer des mocks pour les tests
- **Maintenabilité:** Les changements de paramètres d'ennemi n'affectent qu'une classe

#### Justification pour le rapport
> Le patron Factory Method est utilisé pour centraliser la création des ennemis (Rat des Donjons, Guerrier Déchu, Mage Corrompu, et Boss) et faciliter l'extension du jeu avec de nouveaux types d'ennemis sans modification du système de combat existant.

---

### 3️⃣ Equipment → Abstract Factory (ajout structurel)

#### Problème
- Les équipements forment des "familles"
- Armes, Armures, Anneaux = 3 catégories
- Chaque catégorie peut avoir plusieurs niveaux de rareté

#### Solution : Abstract Factory
```
EquipmentFactory (Abstract)
 ├── BasicEquipmentFactory
 │    ├── CreateWeapon() → Épée rouillée
 │    ├── CreateArmor() → Armure légère
 │    └── CreateRing() → Anneau de vie
 │
 └── AdvancedEquipmentFactory
      ├── CreateWeapon() → Épée du héros
      ├── CreateArmor() → Armure renforcée
      └── CreateRing() → Anneau de puissance
```

#### Bénéfices
- **Cohésion:** Les objets d'une même famille sont créés ensemble
- **Évolutivité:** Ajouter une nouvelle rareté (Épique, Légendaire) = nouvelle Factory
- **Flexibilité:** Le système de récompense peut choisir dynamiquement la Factory

#### Impression pédagogique
> "Ah, il comprend le concept de famille d'objets (famille d'objets) et sait comment les organiser."

#### Justification pour le rapport
> L'Abstract Factory centralise la création des familles d'équipements (armes, armures, anneaux) selon différents niveaux de rareté et facilite l'ajout de nouvelles catégories d'équipements.

---

### 4️⃣ Weapon / Armor / Ring → Strategy (très avancé)

#### Problème
- Les équipements modifient simplement les chiffres
- Mettre une logique `if (weaponType == "sword")` en 10 endroits → mauvaise architecture
- Comment rendre le système flexible et lisible?

#### Solution : Strategy
```
IStatModifier (Interface)
 ├── WeaponModifier (Stratégie: modifier Attack)
 ├── ArmorModifier (Stratégie: modifier Defense)
 └── RingModifier (Stratégie: modifier un attribut quelconque)
```

#### Concept clé
> L'équipement n'est pas un "objet qui change les chiffres".
> L'équipement est un "comportement qui modifie les statistiques".

#### Architecture
```csharp
public interface IStatModifier
{
    void ApplyModifier(Player player);
    void RemoveModifier(Player player);
}

public class WeaponModifier : IStatModifier
{
    public void ApplyModifier(Player player)
    {
        player.Attack += attackBonus;
    }
}
```

#### Bénéfices
- **Décentralisation:** Chaque équipement connaît comment se modifier
- **Extensibilité:** Ajouter un anneau spécial = créer 1 nouvelle classe Strategy
- **Testabilité:** Chaque stratégie peut être testée indépendamment

#### Pourquoi c'est "avancé"
Les étudiants voient généralement les équipements comme des données (classe avec propriétés).
Voir les équipements comme des **comportements** = compréhension profonde du design OOP.

#### Justification pour le rapport
> Les équipements utilisent le patron Strategy afin de modifier dynamiquement les statistiques du joueur de manière interchangeable. Chaque type d'équipement incarne une stratégie de modification différente, permettant une architecture flexible et maintenable.

---

### 5️⃣ Experience System → Observer (pattern idéal)

#### Problème
- Joueur gagne XP
- XP atteint le seuil → doit monter de niveau
- **Qui** calcule? **Qui** notifie? **Qui** met à jour l'UI?
- Si tout est mélangé dans Player → classe énorme et confuse

#### Solution : Observer
```
ExperienceSystem (Subject/Observable)
 ├── notifie → Player (Observateur)
 ├── notifie → UIManager (Observateur)
 └── notifie → SoundManager (Observateur)  [optionnel]
```

#### Architecture
```csharp
public interface IExperienceObserver
{
    void OnLevelUp(int newLevel);
    void OnExperienceGained(int xpAmount);
}

public class ExperienceSystem
{
    private List<IExperienceObserver> observers = new();
    
    public void GainXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= xpNeeded)
            NotifyLevelUp();
    }
    
    private void NotifyLevelUp()
    {
        foreach (var observer in observers)
            observer.OnLevelUp(currentLevel);
    }
}
```

#### Avantages critiques
- **Faible couplage:** ExperienceSystem ne connaît pas Player ou UIManager
- **Haute cohésion:** Chaque observateur gère sa propre logique
- **Extensibilité:** Ajouter un nouveau comportement au niveau 10 = créer 1 observateur
- **Testabilité:** Tester le système d'exp indépendamment des observateurs

#### Aspect pédagogique clé
Cela démontre une compréhension de:
- Inversion de dépendance
- Loose coupling vs tight coupling
- Communication basée sur les événements (Event-driven)

#### Justification pour le rapport
> Le système d'expérience utilise le patron Observer pour découpler la logique de gain d'XP de ses observateurs (Player, UI, etc.). Cela garantit une faible couplage (faible couplage) et une haute capacité d'extension sans modifier le système central d'expérience.

---

## Conclusion

Cette architecture démontre:

✅ **Compréhension des principes SOLID**
- Single Responsibility
- Open/Closed Principle
- Dependency Inversion

✅ **Maîtrise des modèles de conception**
- Factory Method
- Abstract Factory
- Strategy
- Observer

✅ **Pensée architecturale**
- Séparation des préoccupations
- Couplage faible
- Haute cohésion

✅ **Extensibilité**
- Ajouter de nouveaux ennemis, équipements, systèmes → minimal impact sur le code existant

Cette approche est professionnelle, scalable et parfaite pour une présentation académique.

---

## Prochaines étapes de développement

1. ✅ **Implémentation des modèles de conception**
2. ✅ **Tests unitaires par composant**
3. ✅ **Intégration avec le frontend Unity**
4. ⏳ **Balance et tuning des valeurs**
5. ⏳ **Système de sauvegarde (optionnel)**
6. ⏳ **Modes de difficulté avancés (optionnel)**
