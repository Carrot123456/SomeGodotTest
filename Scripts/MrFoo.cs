using Godot;
using System;


public class MrFoo : KinematicBody2D
{
    Vector2 Velocity = new Vector2();
    Vector2 UpDir = new Vector2(0, -1);
    PackedScene BulletScene = new PackedScene();
    int FooDir = Right;
    int IsLookingUpOrDown = 0;
    float ShootTimer = 0;
    const float ShootCooldown = (float)0.2;
    const int Left = 1, Right = 2, Up = 3, Down = 4;
    const float Speed = 64, Gravity = 320, JumpHeight = 128;


    public override void _Ready()
    {
        BulletScene = GD.Load<PackedScene>("res://Scenes/Bullet.tscn");
    }


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


    public override void _Process(float delta)
    {
        Velocity.y += delta * Gravity;


        if (Input.IsActionPressed("Right"))
        {
            Velocity.x = SmoothIncAbs(Velocity.x, Speed * 20 * delta, Speed, Right);
            FooDir = Right;
        }
        else if (Input.IsActionPressed("Left"))
        {
            Velocity.x = SmoothIncAbs(Velocity.x, Speed * 20 * delta, Speed, Left);
            FooDir = Left;
        }
        else
        {
            Velocity.x = SmoothDecAbs(Velocity.x, Speed * 4 * delta);
        }

        if (IsOnFloor() && Input.IsActionPressed("Jump"))
            Velocity.y = -JumpHeight;

        Velocity = MoveAndSlide(Velocity, UpDir);


        if (Input.IsActionPressed("Up"))
        {
            IsLookingUpOrDown = Up;
        }
        else if (Input.IsActionPressed("Down"))
        {
            IsLookingUpOrDown = Down;
        }
        else
        {
            IsLookingUpOrDown = 0;
        }


        ShootTimer -= delta;
        if (ShootTimer <= 0)
        {
            ShootTimer = 0;
        }
        if (Input.IsActionPressed("Shoot") && ShootTimer == 0)
        {
            Bullet NewNode = (Bullet)BulletScene.Instance();
            NewNode.Initialize(Position, FooDir, IsLookingUpOrDown);
            GetParent().AddChild(NewNode);

            ShootTimer = ShootCooldown;
        }
    }
}
