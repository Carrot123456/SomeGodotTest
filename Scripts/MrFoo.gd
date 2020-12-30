extends KinematicBody2D


var Velocity = Vector2()
var Speed = 64
var Gravity = 320
var JumpHeight = 128
var Dir = Vector2(0, -1)
const Left = 0
const Right = 1


func _ready():
    pass


func SmoothDecAbs(Current, Dec):
    if Current > 0:
        Current -= Dec
        if Current <= 0:
            Current = 0

    else: if Current < 0:
        Current += Dec
        if Current >= 0:
            Current = 0

    return Current


func SmoothIncAbs(Current, Inc, Cap, Dir):
    Cap = abs(Cap)
    print(Current)

    if Current != Cap:
        if Dir == Right:
            Current += Inc
            if Current >= Cap:
                Current = Cap

        else: if Dir == Left:
            Cap = -Cap

            Current -= Inc
            if Current <= Cap:
                Current = Cap

    return Current


func _process(delta):
    Velocity.y += delta * Gravity

    if Input.is_action_pressed("Right"):
        Velocity.x = SmoothIncAbs(Velocity.x, Speed * 20 * delta, Speed, Right)
    else: if Input.is_action_pressed("Left"):
        Velocity.x = SmoothIncAbs(Velocity.x, Speed * 20 * delta, Speed, Left)
    else: 
        Velocity.x = SmoothDecAbs(Velocity.x, Speed * 4 * delta)

    if is_on_floor() && Input.is_action_pressed("Jump"):
        Velocity.y = -JumpHeight

    Velocity = move_and_slide(Velocity, Dir)
