using Godot;
using System;

public class FooEnemy : KinematicBody2D
{
    Vector2 Velocity = new Vector2();
    Vector2 UpDir = new Vector2(0, -1);
    const float Speed = 64, Gravity = 320, JumpHeight = 128;


    public override void _Ready()
    {
        
    }

    public override void _Process(float delta)
    {
        Velocity.y += delta * Gravity;

        Velocity = MoveAndSlide(Velocity, UpDir);
    }
    
    private void _on_Area2D_area_entered(object area)
    {
        QueueFree();
    }
}
