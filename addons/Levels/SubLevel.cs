using Godot;
using System;

public partial class SubLevel : LevelCommon
{

    Level parentLevel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public override void EnterSubLevel(Player p, Level parent)
    {
        Player = p;
        parentLevel = parent;
    }


    public override void ExitSubLevel()
    {
        Player = null;
        GetTree().Root.RemoveChild(this);
        GetTree().Root.AddChild(parentLevel);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(double delta)
    //  {
    //      
    //  }
}
