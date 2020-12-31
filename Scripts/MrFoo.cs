using Godot;
using System;


public class MrFoo : KinematicBody2D
{
    Vector2 Velocity = new Vector2();
    float Speed = 64;
    float Gravity = 320;
    float JumpHeight = 128;
    Vector2 UpDir = new Vector2(0, -1);
    const int Left = 0;
    const int Right = 1;


    private float SmoothDecAbs(float Current, float Dec)
    {
        if (Current > 0)
        {
            Current -= Dec;
            if (Current <= 0)
            {
                Current = 0;
            }
        }
        else if (Current < 0)
        {
            Current += Dec;
            if (Current >= 0)
            {
                Current = 0;
            }
        }
        return Current;
    }


    private float SmoothIncAbs(float Current, float Inc, float Cap, float Dir)
    {
        Cap = Math.Abs(Cap);

        if (Current != Cap)
        {
            if (Dir == Right)
            {
                Current += Inc;
                if (Current >= Cap)
                {
                    Current = Cap;
                }
            }
            else if (Dir == Left)
            {
                Cap = -Cap;

                Current -= Inc;
                if (Current <= Cap)
                {
                    Current = Cap;
                }
            }
        }
        return Current;
    }


    public override void _Ready()
    {
        
    }


    public override void _Process(float delta)
    {
        Velocity.y += delta * Gravity;

        if (Input.IsActionPressed("Right"))
        {
            Velocity.x = SmoothIncAbs(Velocity.x, Speed * 20 * delta, Speed, Right);
        }
        else if (Input.IsActionPressed("Left"))
        {
            Velocity.x = SmoothIncAbs(Velocity.x, Speed * 20 * delta, Speed, Left);
        }
        else
        {
            Velocity.x = SmoothDecAbs(Velocity.x, Speed * 4 * delta);
        }

        if (IsOnFloor() && Input.IsActionPressed("Jump"))
            Velocity.y = -JumpHeight;

        Velocity = MoveAndSlide(Velocity, UpDir);
    }
}
