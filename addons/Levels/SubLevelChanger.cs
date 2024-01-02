using Godot;
using System;

public partial class SubLevelChanger : Area2D
{
    SceneManager scenes;
    Level currentLevel;

    [Export]
    string subLevel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        scenes = (SceneManager)GetNode("/root/GameManager/SceneManager");
        currentLevel = (Level)SceneManager.CurrentScene;
    }

    public void Teleport(object body)
    {

    }
}
