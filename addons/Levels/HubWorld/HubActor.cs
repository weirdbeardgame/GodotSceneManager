using Godot;
using System;

// This is intended for 2D level selects that don't nessacarily use the player controller
// Think a classic 2D or 3D mario overworld level select. These have different controlls and interact based on nodes or spaces
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
