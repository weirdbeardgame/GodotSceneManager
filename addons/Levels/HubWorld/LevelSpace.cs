using Godot;
using System;

public partial class LevelSpace : Control
{
    [Export]
    string LevelName;

    Level Scene;
    SceneManager Scenes;

    Label TextLabel;

    public override void _Ready()
    {
        TextLabel = GetNode<Label>("LevelName");
        Scenes = GetNode<SceneManager>("/root/SceneManager");
        Scene = (Level)Scenes.GetLevel(LevelName);
        TextLabel.Text = Scene.LevelName;
    }

    public void ActivateLevel(Player p)
    {
        GD.Print("SUBMIT!");
        SceneManager.ChangeScene(LevelName, p);
    }
}
