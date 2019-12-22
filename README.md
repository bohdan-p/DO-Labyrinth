# Darkorbit Frozen Labyrinth Pathfinder

Allows to find the shortest path in DO event map.

Type in the position you are currently in and final destination like *Auriga TR*, just *Auriga* or just *TR*
and you will get a list of actions to get to destination.

### List of maps:
- Persei
- Sadatoni
- Sirius
- Volantis
- ATLAS A
- ATLAS B
- ATLAS C
- Aquila
- Alcyone
- Orion
- Auriga
- Maia
- Bootes
- Cygni
- Eridani
- Helvetios

### Sections:
- TL = Top Left
- TR = Top Right
- BR = Bottom Right
- BL = Bottom Left

### Sample runs:
```
Auriga TR
Where you want to go:
BR

 Shortest path:
--[]->Auriga TR--[TR]->Orion BR

Hit enter to search path again or type Q and hit enter to quit:

Your position:
Auriga TR
Where you want to go:
ATLAS B TR

 Shortest path:
--[]->Auriga TR--[BR]->Bootes BR--[BR]->Bootes TL--[TL]->ATLAS B TR

Hit enter to search path again or type Q and hit enter to quit:

Your position:
Auriga TR
Where you want to go:
Maia

 Shortest path:
--[]->Auriga TR--[TL]->Maia BL
```
