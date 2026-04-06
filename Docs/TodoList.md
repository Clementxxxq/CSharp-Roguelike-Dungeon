# Combat V1 Todo List (UI Buttons, One Skill)

## 1. Scope and Goal

- [x] Build a playable combat prototype without movement controls.
- [x] Control the player only through simple UI buttons.
- [x] Keep exactly one active skill in V1.
- [ ] Finish one full run with wave-based combat and clear win/lose results.

## 2. Out of Scope (Do Not Build in V1)

- [x] No WASD or joystick movement.
- [x] No procedural map generation.
- [x] No out-of-run meta progression.
- [x] No multi-scene chapter flow.

## 3. Core Combat Loop

- [x] Enter combat scene and initialize player plus current wave.
- [x] Player turn: click one action button.
- [x] Enemy turn: enemy acts automatically.
- [x] End turn: apply damage, cooldown, and state updates.
- [x] Check win/lose state after each turn.

## 4. UI and Buttons

- [x] Add player panel (HP, optional shield/energy).
- [x] Add enemy panel (name, HP, next intent text).
- [x] Add combat log area (recent 3-6 lines).
- [x] Add button: Normal Attack.
- [x] Add button: Skill (single active skill with cooldown).
- [x] Add button: Defend.
- [x] Add button: Restart Run.

## 5. Player Actions (V1 Rules)

- [x] Normal Attack: deal fixed damage to current enemy.
- [x] Skill: high damage or heal, with 2-3 turn cooldown.
- [x] Defend: reduce incoming damage for one turn.
- [x] Disable action buttons while enemy turn is running.

## 6. Enemy and Waves

- [x] Implement at least 2 enemy types (melee plus ranged/simple caster).
- [x] Define enemy intent each turn (attack, defend).
- [x] Spawn enemies by waves (recommended 3 waves).
- [x] Increase enemy stats per wave with simple scaling.
- [x] Show wave index in UI (for example, Wave 2/3).

## 7. Win/Lose and Flow

- [x] Win wave when all enemies are defeated.
- [ ] Start next wave automatically after short delay.
- [x] Win run when final wave is cleared.
- [x] Lose run when player HP is zero or below.
- [x] Show result panel with Restart button.

## 8. Data and Architecture

- [x] Keep battle flow in a state machine (player turn, enemy turn, result).
- [x] Keep stat changes event-driven for UI updates.
- [ ] Store combat values in config or ScriptableObject data.
- [x] Separate UI binding code from combat logic.

## 9. Feedback and Readability

- [x] Show clear damage numbers or concise combat logs.
- [x] Show skill cooldown state on button text or icon.
- [x] Highlight enemy intent every turn.
- [x] Ensure every action has immediate visible feedback.

## 10. V1 Acceptance Criteria

- [ ] One complete run takes around 3-8 minutes.
- [ ] A new player can understand controls in under 2 minutes.
- [ ] Death reasons are understandable and traceable.
- [ ] No blocking bugs in start -> fight -> result -> restart flow.

## 11. Suggested Implementation Order

- [x] Step 1: Build turn state machine.
- [x] Step 2: Implement three buttons and callbacks (Attack, Skill, Defend).
- [x] Step 3: Add enemy intent and wave spawner.
- [x] Step 4: Add result panel and restart flow.
- [ ] Step 5: Tune numbers and polish feedback.
