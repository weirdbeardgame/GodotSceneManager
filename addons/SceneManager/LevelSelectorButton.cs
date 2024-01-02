using Godot;
using System;

[Tool]
public partial class LevelSelectorButton : Button
{
    SceneManager SceneManager;
    string SceneName;

    // Called when the node enters the scene tree for the first time.
    public override void _EnterTree()
    {
        base._EnterTree();
        SceneManager = SceneManager.Manager;
        Pressed += ChangeScene;
    }

    public void CreateButton(string name)
    {
        SceneName = name;
        Text = SceneName;
    }

    void ChangeScene()
    {
        SceneManager.ChangeSceneInEditor(SceneName);
    }
}
