
# Project Notes

**Requirements: Initial Meeting Fall 2023**

## Name: Predator-Prey

### Objects:
- **Predator:** any sprite  
- **Prey:** any sprite  
- **Obstacles:** randomized “circles”  

### Game:
- Turn-by-Turn
- Keep track of location and direction facing (direction)
- Each turn, pick a location and based on the location we have a possibility of the direction of the face, and then this is selected.
- Centering based on turn.  

---

## MVP
- Infinite plane
- Two objects (any) with turn-based movement
- Camera moves to object with a turn
- Keyboard for camera controls


---

## Legal Move Area

The mesh of each object's valid moves after initializing the rules of the game.  
The two parameters needed:
- `d`: maximum distance traveled in a single move  
- `R`: minimum turning radius

**Mesh Parameterization**  
- Parameterized by `t` from 0 to 1 and `a` from `-1/R` to `1/R`

**SageMath Code Example:**
```python
R=1.4
d=2
var('t,a')
parametric_plot3d((1/a*(1-cos(d*a*t)),1/a*sin(d*a*t),0),(a,-1/R,1/R),(t,0,1))
```

**Tangent Vector (Direction of Travel):**  
`<d sin(d a t), d cos(d a t)>`  
**Unit Vector:**  
`<sin(d a t), cos(d a t)>`

---

## Equation Inversion

To determine the path from a clicked point `(x, y)`, solve:

```
solve for u and v {
    x = 1/u*(1-cos(d*u*v)),
    y = 1/u*sin(d*u*v)
}
```

ion

---

## Game Design

### Options (Start Screen):
- Number of Predators (1–10)
- Number of Prey (1–10)
- Turning Radius for all Predators (0.5 – 5.0)
- Turning Radius for all Prey
- Max Travel Distance for all Predators (0.5 – 5.0)
- Max Travel Distance for all Prey
- Number of Turns
- Location (Start Distance):
  - Close (4× max travel distance)
  - Mid (8× max travel distance)
  - Far (12× max travel distance)

> Note: Without herding strategy, predators tend to split. Initial configuration has minimal long-term impact.

---

## Flow

### Start Screen
- Buttons: **Start**, **Options**, **Exit**
  - **Exit:** closes program
  - **Start:** begins game
  - **Options:** opens options panel

### Game
1. Game loads with prey and predators visible
2. Legal movement area for prey is displayed
3. All prey movement locations are selected (mouse)
4. Legal movement area for predators is displayed
5. All predator movement locations are selected (mouse)
6. All characters move to selected locations
7. Scoreboard updated with turn number
8. Collisions remove prey
9. Check game stop condition:
    - If met → go to End Game
    - If not → repeat from step 2

### End Game Screen
- Display:
  - Number of turns
  - Number of prey escaped
  - Winner
- Button: Return to Start

---

## Game Stop Condition
- All prey captured  
- Maximum number of turns reached

---

## Game States
- **PreyTurn:** select next location for all prey
- **PredatorTurn:** select next location for all predators
- **MoveAvatars:** move prey and predator simultaneously
- **GameOver:** triggered when stop condition is met

---

## Notes
- `LegalMoveArea` on UI Layer  
- Predator/Prey on Default Layer  
- They do not cause collisions due to layer settings.
