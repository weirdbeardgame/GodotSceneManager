using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[Tool]
public partial class LevelDockScript : Control
{
    SceneManager SceneManager;
    FileDialog Dialog;

    Button AddSceneButton;
    Button NewSceneButton;

    [Export]
    PackedScene SceneButton;

    List<LevelSelectorButton> SceneChanger;

    string CurrentScene;
    List<string> SceneNames;

    VBoxContainer Container;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Dialog = GetNode<FileDialog>("Panel/FileDialog");
        SceneManager = SceneManager.Manager;

        AddSceneButton = GetNode<Button>("Panel/AddScene");
        NewSceneButton = GetNode<Button>("Panel/NewScene");

        AddSceneButton.Pressed += AddScene_Button;
        NewSceneButton.Pressed += NewScene_Button;

        Dialog.FileSelected += AddScene;

        Container = GetNode<VBoxContainer>("Panel/ScrollContainer/VBoxContainer");

        SceneChanger = new List<LevelSelectorButton>();
        SceneNames = new List<string>();

        UpdateList();
    }

    void AddScene_Button()
    {
        Dialog.Popup();
    }

    void AddScene(string path)
    {
        GD.Print("Path: " + path);
        PackedScene Scene = (PackedScene)ResourceLoader.Load(path);
        if (SceneManager.Add(Scene))
        {
            GD.Print("Scene Added");
            UpdateList();
        }
    }

    void NewScene_Button()
    {
        SceneManager.New();
        // ToDo: Level editor right after lads
    }

    void ChangeScene()
    {

    }

    void RemoveScene_Button()
    {
        SceneManager.Remove(CurrentScene);
    }

    void UpdateList()
    {
        if (SceneManager.SceneNames != null)
        {
            if (SceneNames != SceneManager.SceneNames && SceneManager.SceneNames.Count > 0)
            {
                SceneNames = SceneManager.SceneNames;
                foreach (var scene in SceneNames)
                {
                    LevelSelectorButton SelectorButton = SceneButton.Instantiate<LevelSelectorButton>();
                    SelectorButton.CreateButton(scene);
                    SceneChanger.Add(SelectorButton);
                    Container.AddChild(SelectorButton);
                }
            }
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
