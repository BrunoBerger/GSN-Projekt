# Games-spn

### Relevant Files:
(All in ```./Assets/```)

Lane-Jumping:  
* ```JumpBetweenLanes.cs``` -> Attach to Player and assign ```PlayerJumpPreset.asset```  
as JumpPreset. Then assign the tag ```lane``` to any GameObject you want
* ```PlayerJumpPreset.asset``` -> Configures Player-Jump characteristics.
  * Both Curves have to go continuously  from 0 to 1 on the x-axis
  * VerticalMovement-Curve has to start and end at y=0
  * Horitontal-Curve has to start a y=0 and end at y=1
  * Everything in between is totally free
* ```JumpPreset.cs``` -> For constructing new Jump presets through ScriptableObjekts

Day-Night Cycle:
1. Set ```Lighting/Environment/Environment Lighting/Source``` to ```Color```
2. (similar to lane jumping setup)
