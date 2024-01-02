using Godot;
using System;

public partial class HubActor : Node2D
{

    // This just holds the active refrence but doesn't allow the Player to act.
    Player _player;

    // Playable sprite bobs up and down.
    AnimationPlayer animation;

    public Player Player
    {
        get
        {
            return _player;
        }
    }

    public void Activate(Player p)
    {
        _player = p;
        _player.Visible = false;
    }

    public void Deactivate()
    {
        _player.Visible = true;
    }

    // Play animations to the call of the Hubworld / active path making character walk.
    public void Move()
    {

    }
}
