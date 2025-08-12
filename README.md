# Game Structure Overview

## 🎮 Player
Handles all player-related mechanics including movement, jumping, dashing, and interactions.

- **Controls:**
  - `A` / `D` → Move Left / Right
  - `W` → Dash
  - `Space` → Jump
  - **Double Jump** and **Dash** are supported

---

## 🧠 GameManager
Acts as the **central controller** for managing core game logic and state transitions.

---

## 🖥️ UIManager
Responsible for managing and updating **all in-game UI elements**.

---

## 🧩 EventBus Architecture
Implements an **event-driven system** to maintain **decoupled code structure**, enabling clean communication between different components.

---

## 🗺️ Level Design
### Level One & Level Two
- Each level is handled by a dedicated designer script.
- Controls the gameplay flow and unique mechanics for each level.

---

## 🔄 Resettable Object
This script resets objects to their original state whenever the player respawns.

---

## ⚡ TriggerAction
Defines and manages **events triggered** when the player interacts with specific game objects.

---

## 🕹️ Gameplay Summary

- A simple **2D platformer**.
- **No clues provided** – players must carefully plan each step.
- Gameplay emphasizes **trial, error, and careful decision-making**.

---

## 🚀 Version 1 Overview

- **Level 1:** Contains **7 unique variations**
- **Level 2:** Contains **4 unique variations**

---

### 💡 Tip:
There are no hints. Every move matters.  
**Think carefully, step wisely, and enjoy the mystery!**

---

**Happy Coding, Happy Gaming!**
