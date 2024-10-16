# Isometric2DGame
 
 A sample repository showing my use of git,and my solutions to a set of tasks asked for a internship interview.

 ## Task 1: create a git repo

1.  <b>Create a unity 2D project</b>:
    using the 2D URP unity template and any LTS 2022.3+ version
2. <b>Create a git repo</b>

## Task 2: Isometric player movement and Input system

1. <b>Install unitys new input system:</b>

 set up controlls and movement
- The player should move diagonally (isometric movement):

    - W/Up Arrow: Moves the player northeast.
    - S/Down Arrow: Moves the player southwest.
    - A/Left Arrow: Moves the player northwest.
    - D/Right Arrow: Moves the player southeast.  

2. <b>Set up initial collision</b>: 
    - add collider 2D to the player
3. <b>Set up an isometric camera</b>
    - add a camera to the scene and set it to isometric view
    - make sure the camera follows the player

### checklist:
- [x] add unity input system
- [ ] set up movement using WASD
    - [ ] add WASD to input system
    - [ ] add player asset to the scene
    - [ ] create movement monobehaviour for player
    - [ ] add rigid2Dbody to player game object
- [ ] set up isometric camera
- [ ] test movements


## Task 3: AI State Machine for Enemy Behavior

1. <b>Create enemy game object</b>
2. <b>Set up FMS:</b>

Set up a Finite state machine to controll the 'enemy' game object actions.
- Idle: The enemy remains stationary when the player is out of range.
- Patrol: The enemy patrols between two points when the player is not
nearby.
- Chase: The enemy chases the player if they enter a specific detection
range.
- Attack: The enemy attacks the player when it gets close enough.

3. <b>Add state transitions</b>
    Use triggers, raycasts, or colliders to detect the player’s position and trigger state transitions
- Transition logic:
    - Switch to Chase when the player enters the detection range.
    - Switch to Attack when the player is close enough.
    - Return to Idle or Patrol when the player leaves the detection range.
4. <b>Implement AI behaviour</b>
- The enemy should patrol between two points when in the Patrol state.
- The enemy should move towards the player when in the Chase state.
- The enemy should print a simple "Attacking" debug message or decrease the
player's health when in the Attack state.

## Task 4: Implement a Basic Combat System
(selected among 3 options)
1. Implement player attack
2. Implement melee attacks and projectiles
3. implement HP system

    