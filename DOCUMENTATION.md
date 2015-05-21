Documentation
=============
This is the documentation for the life raft simulator.

Terminology
-------------
In this document we will refer to the real life human being that is supplying the game with keyboard and mouse input as the *player*. The character on screen that the player is controlling we refer to as the *hero*.

Synopsis
-------------
Our hero is floating in the open ocean with debris floating around him.
A life raft is also near by in which coincidently will where he will attempt to survive for the next 7 days.

Disclaimer
-------------
The game is not supposed to be easy. The aim is to get a good score. If the game was easy it would not be difficult to get the best score.

Phases
-------------
The game is split into three different phases.

### Phase 1 - Debris field
Loot is all around our hero but there is not much time to choose what to take before it sinks or the raft floats away.
Phase 2 begins when there is no more loot to collect in the water surface.

### Phase 2 - Alone
Our hero is in the raft now it is time to make the best of it. Craft a hook to fish, put up some shade, filter some water, anything to make the stay in raft a little less unbearable.
Phase 3 starts when land is spotted.

### Phase 3 - Rescue
Our hero must paddle to land.

Score
-------------
The score is based on the current stats of the hero.

> **Example** 
>
Energy: 15
>
Comfort: -30
>
Hunger: +20 (positive is bad)
>
Total: -35

Multiply the score by 1,000 so it looks like something.

Character stats
-------------
Statistics are not only used to calculate the score as shown in the "Score" chapter but also to give visual and audio feedback to the player on how the hero is doing, if not dead already.

Each stat is a floating-point number tbetween -100.00 and 100.00.

### Table of stats
| Stat | Feedback | Aim |
| :--- | :-----| :----: |
| Comfort | Score | Positive |
| Energy | Score, Character speed | Positive |
| Hunger | Score, Death, Growling | Negative |
| Sunburn | Score, Skin color | Negative |
| Temperature | Score, Death | Center |
| Time in the sun | Sunburn, Temperature | Center |
| Water | Score, Death | Center |

Day/night cycle
-------------
One day in the game is 12 real minutes. 1/3 is night, 1/6 is evening, 1/6 is morning and the rest is day time.

Weather
-------------
Currently there is no plan to include weather like rain, wind, snow, blizzards, tornados.

Visual style
-------------
Realistic

HUD
-------------
The heads up display will be very simple. The game will be too easy if we simply show the hero and environment stats. The HUD will only contain the current score. Everything else will be available to the player through visual and audio feedback.

Controls
-------------

### Floating
While in the water player can click on water to make the hero swim there, click on items to make the hero collect them and click on raft to make the hero board it.

### On the raft
While in the raft player can click water to make the hero go into the water and click items to interact (use/craft) with them.

### Diving
Not for this version.

### Crafting
Each item when clicked will allow the player to choose between currently possible crafting options. The rafting is not instant though so the player should choose wisely.

Fishing
-------------
The bigger bait used while fishing the better chance of success. The chances in the following table is the chance per second. Remember to use Time.deltaTime to balance of the update rate.

| Bait | Small fish | Medium fish | Large fish |
| ---- | ---- | ---- | ---- |
| None | 1 : 2,500 | 1 : 5,000 | 1 : 10,000 | 
| Small fish | 0 | 1 : 3,000 | 1 : 6,000 |
| Medium fish | 0 | 0 | 1 : 4,000 |
| Large fish | 0 | 0 | 0 |

The chance of losing the bait is 1 : 90.

Items
-------------

| Type | Usage | Description |
| ---- | ---- | ---- |
| Ditch bag | Open | Contains the most neccsary stuff found in life rafts |
| Water filter | Use, Disassemble (later) | Can filter ocean water into drinkable water (slow) or be used as parts (later) |
| Duct tape | Use | Can be used to craft new items (later) |
| Water bottle | Drink, Fill | Container for water |
| Paddle | Use | Used for paddling |
| Reflective blanket | Use | Can be used for shade during the day and keep warm (or less cold?) during the night |

