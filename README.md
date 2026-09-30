---

## 2. A* Pathfinding Visualizer (C#)

```markdown
# A* Pathfinding Visualizer

An interactive 2D pathfinding visualizer using the A* search algorithm built in C#. Designed to demonstrate real-time grid traversal, dynamic obstacle node updates, and heuristic evaluation for RTS unit navigation.

---

## The Problem It Solves

In strategy games with hundreds of moving units, pathfinding can quickly consume a massive portion of the CPU budget. Naive pathfinding algorithms either check too many unneeded tiles or produce unnatural paths.

## The Solution

This tool implements the **A* algorithm** using a binary min-heap priority queue. It balances path efficiency and distance evaluation to calculate the shortest path while avoiding terrain obstacles in real time.

---

## How It Works (The Metaphor)

Imagine walking through a dense forest to reach a bright beacon on a hill:

1. **G-Cost (Distance from start):** How many steps you've already walked from camp.
2. **H-Cost / Heuristic (Estimated distance to target):** How far away the beacon looks in a straight line.
3. **F-Cost ($G + H$):** The total score. A* always explores the tile with the lowest $F$-Cost first, guaranteeing it heads toward the goal while working around walls.

---

## Features

- **Interactive Grid:** Click and drag to place walls, start positions, and goal targets in real time.
- **Min-Heap Priority Queue:** Replaces standard list searching to keep open-set node selection at $O(\log N)$ time complexity.
- **Visual Node States:**
  - **Green:** Open set (nodes under evaluation).
  - **Red:** Closed set (evaluated nodes).
  - **Blue:** Final calculated shortest path.
- **Dynamic Heuristic Toggling:** Switch between Euclidean and Manhattan distance metrics on the fly.

---

## Controls

- `Left Click + Drag`: Place walls / obstacles
- `Right Click + Drag`: Move Start / Goal nodes
- `Spacebar`: Run / Pause pathfinding visualization
- `C`: Clear grid

---

## How to Run

1. Clone this repository:
   ```bash
   git clone [https://github.com/09dylano-ux/astar-pathfinding-visualizer.git](https://github.com/09dylano-ux/astar-pathfinding-visualizer.git)
