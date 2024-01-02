using Godot;
using System;

public partial class DeathBoundry : Area2D
{

    public override void _Ready()
    {
        BodyEntered += OnTouch;
    }


    public void OnTouch(Node2D body)
    {
        if (body is Player)
        {
            GD.Print("Death to the player!!!");
            Player p = (Player)body;
        }
    }

}
