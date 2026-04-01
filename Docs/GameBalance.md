# Game Balance

## 1. Objectif

Ce document définit une base claire pour équilibrer le gameplay du Roguelike Dungeon.

Objectifs de design:
- Garder les premiers combats lisibles et non punitifs
- Créer une montée de pression par étage
- Faire du Boss un pic de difficulté net mais juste
- Laisser l'équipement et le level up avoir un impact ressenti

## 2. Référentiel actuel

Stats joueur de base:
- HP: 100
- ATK: 15
- DEF: 5

Stats ennemis actuelles:
- Rat: HP 20, ATK 5, DEF 0, XP 10
- Warrior: HP 40, ATK 10, DEF 3, XP 20
- Mage: HP 30, ATK 15, DEF 1, XP 30
- Boss: HP 120, ATK 20, DEF 5, XP 100

Règles de dégâts observées:
- Dégâts reçus par le joueur = max(1, ATK ennemi - DEF joueur)
- Dégâts infligés aux ennemis = ATK joueur (la DEF de l'ennemi n'est pas encore appliquée)

## 3. Cibles d'équilibrage (Vertical Slice)

Cibles pour le ressenti global:
- Rat: 1 à 2 tours pour vaincre
- Warrior: 2 à 4 tours pour vaincre
- Mage: 2 à 3 tours pour vaincre, pression offensive plus forte
- Boss: 8 à 12 tours pour vaincre au niveau attendu

Cibles de survie du joueur (sans potion):
- Salle normale: perte de 8% à 20% de HP max
- Enchaînement de 3 salles: perte totale autour de 35% à 55% de HP max
- Boss: combat exigeant, mais gagnable avec bonne progression

## 4. Courbe de difficulté recommandée

Difficulté par phase:
- Niveau 1 à 2: majorité Rat, quelques Warrior
- Niveau 3 à 4: Rat + Warrior + Mage (mix équilibré)
- Niveau 5+: Warrior + Mage majoritaires
- Salle finale: Boss unique

Probabilités recommandées (alignées avec EnemyFactory):
- Niveau <= 2: Rat 80%, Warrior 20%
- Niveau <= 4: Rat 40%, Warrior 40%, Mage 20%
- Niveau > 4: Warrior 50%, Mage 50%

## 5. Ajustements numériques proposés

Ajustements légers (version 1):
- Rat: garder tel quel
- Warrior: ATK 9 au lieu de 10 si early game trop punitif
- Mage: garder ATK 15 mais surveiller sa fréquence
- Boss: garder HP 120, ATK 20, DEF 5

Ajustements alternatifs (si boss trop dur):
- Option A: Boss HP 110
- Option B: Boss ATK 18
- Option C: conserver les stats mais offrir une récompense forte avant boss

## 6. Indicateurs à suivre

Mesures minimales à logger par run:
- Dégâts subis par salle
- Nombre de tours par combat
- HP restant avant boss
- Taux de victoire sur boss
- Niveau atteint avant boss

Seuils de validation:
- Taux de victoire global (build interne): 55% à 70%
- Taux de victoire boss avec progression normale: 45% à 60%
- Aucun combat normal ne doit devenir impossible sans erreur de joueur

## 7. Plan d'itération

Boucle simple en 4 étapes:
1. Jouer 10 runs avec les mêmes règles
2. Noter les indicateurs
3. Changer un seul paramètre à la fois
4. Rejouer 10 runs et comparer

Règle clé:
- Ne jamais modifier plusieurs stats majeures en même temps (ex: HP et ATK du boss simultanément)

## 8. Checklist d'acceptation

- Les 3 premiers combats sont compréhensibles et gagnables
- Le joueur ressent la montée en puissance de son niveau
- Le boss est difficile mais pas arbitraire
- Les quatre archétypes Rat, Warrior, Mage, Boss ont une identité lisible
- Les ajustements sont tracés dans ce document

## 9. Historique des décisions

Template à remplir à chaque changement:
- Date:
- Changement:
- Raison:
- Impact observé:
- Décision finale:
