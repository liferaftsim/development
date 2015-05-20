Documentation
=============
This is the documentation for the life raft simulator.

Synopsis
-------------
You're floating in the open ocean with debris floating around you.
A life raft is also near by in which coincidently will where you will survive for the next 7 days.

Disclaimer
-------------
The game is not supposed to be easy. The aim is to get a good score. If the game was easy it would not be difficult to get the best score.

Phases
-------------
The game is split into three different phases.

### Phase 1 - Debris field
Loot is all around you but you don't have much time to choose what to take before it sinks or the raft floats away.
Phase 2 begins when there is no more loot to collect.

### Phase 2 - Alone
You're in the raft now make the best of it. Craft a hook to fish, put up some shade, filter some water, anything to make the stay in raft a little less unbearable.
Phase 3 starts when land is spotted.

### Phase 3 - Rescue
You must paddle to land.

Score
-------------
The score is based on the current character stats.

> **Example** 
>
Energy: 15
>
Comfort: -30
>
Hunger: +20 (positive is bad)
>
Total: -35

Character stats
-------------
Statistics are not only used to calculate the score as shown in the "Score" chapter but also to give visual and audio feedback to the player on how the character is doing, if not dead already.

Each stat ranges between -100 and 100.

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

Controls
-------------

### Floating
While in the water player can click on water to swim there, click on items to collect them and click on raft to board it.

### On the raft
While in the raft player can click water to jump into the water and click items to interact (use/craft) with them.

### Diving
Not for this version.

Items
-------------

| Name | Usage | Description |
| ---- | ---- | ---- |
| Ditch bag | Open | Contains the most neccsary stuff found in life rafts |
| Water filter | Use, Disassemble (later) | Can filter ocean water into drinkable water (slow) or be used as parts (later) |
| Duct tape | Use | Can be used to craft new items (later) |
| Water bottle | Drink, Fill | Container for water |


