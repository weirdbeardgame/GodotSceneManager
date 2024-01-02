using Godot;
using System;

// The idea would be to take the Player back to the other spot they entered or left from like a door
// Or just take a Player to the associated level / screen that's attached to this exit.

public partial class Exit : Node2D
{
    [Export]
    PackedScene SubScene;

    [Export]
    Exit ConnectedExit;

    LevelCommon currentLevel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        currentLevel = (LevelCommon)Owner;
    }

    public void OnExit(object body)
    {
        if (body is Player)
        {
            SceneManager.ChangeSceneWithExit(SubScene, (Player)currentLevel.GetNode("Player"), this);
        }
    }
}
