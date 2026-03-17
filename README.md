<div align="center">

# Collapsed Universe — YZTA Game Jam 2025

A 2D platformer where you traverse between two parallel universes through portals.

![Last Commit](https://img.shields.io/github/last-commit/emirbesir/collapsed-universe?style=flat&logo=git&logoColor=white&color=0080ff)
![Top Language](https://img.shields.io/github/languages/top/emirbesir/collapsed-universe?style=flat&color=0080ff)
![Unity](https://img.shields.io/badge/Unity-FFFFFF.svg?style=flat&logo=Unity&logoColor=black)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

_Google AI & Technology Academy — Parallel Universes themed Game Jam_

_Made and tested with **Unity 6000.0.48f1**_

</div>

## Gameplay

Travel between two distinct universes (Cyberpunk and Steampunk) connected by portals. Each universe has its own obstacles and puzzles. Wall-jump, sprint, and use pressure plates to navigate platforms while collecting items to progress. Shatter effects provide visual feedback during universe transitions.

## Screenshots

![Screenshot 1](docs/img/ingame_screenshot_1.png)
*Cyberpunk universe*

---

![Screenshot 2](docs/img/ingame_screenshot_2.png)
*Steampunk universe*

## Technical Highlights

- **Portal Teleportation:** Velocity-preserving universe transitions — static flag and 0.5s delay prevent loops
- **Wall Jump:** Dual-sided wall detection, direction tracking, and cooldown system to prevent spam
- **Physics 2D:** Velocity-based movement, Overlap Circle for ground/wall detection
- **Sprint System:** 1.25x speed boost via Shift
- **Narrative System:** MonologueTrigger zones with automatic text display

## Team

- Emir Beşir
- Furkan Beşirli
- Berke Bakırcı
- Rümeysa Sardohan
- Arzu Ekinli

## Assets Used

[Assets](Portal-Game/Assets/External/README.md)

## Links

- [Play (itch.io)](https://calippooo.itch.io/collapsed-universe)
- [Video (YouTube)](https://www.youtube.com/watch?v=8obRY19qgFE)
