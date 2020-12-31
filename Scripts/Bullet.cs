using Godot;
using System;

public class Bullet : Node2D
{
    int FooDir = 0;
    int IsLookingUpOrDown = 0;
    float X = 0, Y = 0;
    const int Left = 1, Right = 2, Up = 3, Down = 4;
    const float Speed = 320;


    public override void _Ready()
    {
    }

    public void Initialize(Vector2 PosInp, int FooDirInp, int IsLookingUpOrDownInp)
    {
        Position = PosInp;
        FooDir = FooDirInp;
        IsLookingUpOrDown = IsLookingUpOrDownInp;

        X = Position.x;
        Y = Position.y;
    }

    public override void _Process(float delta)
    {
        if (IsLookingUpOrDown == 0)
        {
            if (FooDir == Right)
            {
                X += Speed * delta;
            }
            else if (FooDir == Left)
            {
                X -= Speed * delta;
            }
        }
        else
        {
            if (IsLookingUpOrDown == Up)
            {
                Y -= Speed * delta;
            }
            else if (IsLookingUpOrDown == Down)
            {
                Y += Speed * delta;
            }
        }

        Position = new Vector2(X, Y);
    }
}
