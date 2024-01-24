using Godot;
using System;

[Tool]
[GlobalClass]
public partial class SceneManagerData : Resource
{
    [Export] PackedScene _NewGameScene;

    [Export]
    Godot.Collections.Dictionary<string, PackedScene> _Levels;

    [Export]
    string CurrentLevel = "Test1";

    public SceneManagerData() => _Levels = new Godot.Collections.Dictionary<string, PackedScene>();
    public SceneManagerData(Godot.Collections.Dictionary<string, PackedScene> lev) => _Levels = lev;

    // Public Getters
    public PackedScene NewGameScene => _NewGameScene;
    public Godot.Collections.Dictionary<string, PackedScene> Levels => _Levels;

    // Returns the instantiated packed scene as a Level.
    public LevelCommon Level(string levelName) => _Levels[levelName].Instantiate<LevelCommon>();


#if TOOLS
    public bool Add(PackedScene Scene)
    {
        LevelCommon Level = Scene.Instantiate<LevelCommon>();

        if (string.IsNullOrEmpty(Level.LevelName))
        {
            GD.PrintErr("Level has no name!");
        }

        if (!_Levels.ContainsKey(Level.LevelName))
        {
            _Levels.Add(Level.LevelName, Scene);
            return true;
        }
        else
        {
            GD.PrintErr("Scene already exists");
        }
        return false;
    }

    public bool Remove(string SceneName)
    {
        if (_Levels.ContainsKey(SceneName))
        {
            _Levels.Remove(SceneName);
            return true;
        }
        GD.PrintErr("Scene does not exist in manager");
        return false;
    }

    public void SetNewGameScene(string SceneName)
    {
        if (_Levels.ContainsKey(SceneName))
        {
            _NewGameScene = _Levels[SceneName];
        }
    }
#endif

};