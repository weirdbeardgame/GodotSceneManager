using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[Tool]
public partial class SceneManagerData : Resource
{
    [Export]
    Godot.Collections.Dictionary<string, PackedScene> _Levels;

    [Export]
    string CurrentLevel = "Test1";

    public SceneManagerData()
    {
        _Levels = new Godot.Collections.Dictionary<string, PackedScene>();
    }

    public SceneManagerData(Godot.Collections.Dictionary<string, PackedScene> lev) => _Levels = lev;

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
#endif

    public Godot.Collections.Dictionary<string, PackedScene> Levels
    {
        get
        {
            return _Levels;

        }
    }

    public LevelCommon Level(string levelName)
    {
        return _Levels[levelName].Instantiate<LevelCommon>();
    }
};

[Tool]
public partial class SceneManager : EditorPlugin
{
    [Export] private static LevelCommon _CurrentScene;

    [Export] private PackedScene _NewGameScene;

    [Export] PackedScene NewLevel;
    [Export] PackedScene PlayerScene;

    public static Action StartNewGame;
    public static Action<string, Player> ChangeScene;
    public static Action<PackedScene, Player, Exit> ChangeSceneWithExit;
    public static Action ResetLevel;

    Control EditorDock;
    static SceneManagerData ManagerData;

    [Export]
    string SceneManagerPath = "res://SceneManagerData.tres";


    public static LevelCommon CurrentScene
    {
        get
        {
            return _CurrentScene;
        }
    }

    private static SceneManager _SceneManager;

    public static SceneManager Manager
    {
        get
        {
            if (_SceneManager == null)
            {
                _SceneManager = new SceneManager();
            }
            return _SceneManager;
        }
    }

    public override void _Ready()
    {
        // Initialization of the plugin goes here.

        StartNewGame += NewGame;
        ChangeScene += SwitchLevel;
        ChangeSceneWithExit += LoadSubScene;
        ResetLevel += Reset;
        if (ResourceLoader.Exists(SceneManagerPath))
        {
            //ManagerData = ResourceLoader.Load<Resource>(SceneManagerPath) as SceneManagerData;
        }

    }

    void Reset()
    {
        CurrentScene.ResetLevel();
    }

    // Play level changing animation.
    // Load new scene and set it as current
    public void SwitchLevel(string scene, Player Player)
    {
        if (CurrentScene is LevelCommon)
        {
            _CurrentScene = (LevelCommon)GetTree().CurrentScene;
        }

        if (ManagerData.Levels.ContainsKey(scene))
        {
            LevelCommon sceneToLoad = ManagerData.Level(scene);
            CallDeferred(nameof(CallDefferedSwitch), sceneToLoad, Player, (int)sceneToLoad.LevelType);
        }
        else
        {
            GD.PrintErr("INVALID LEVEL SELECTED: " + scene);
        }
    }

    public Player CreatePlayer()
    {
        return PlayerScene.Instantiate<Player>();
    }

    public void DestroyPlayer(Player p)
    {
        p.Dispose();
    }

#if TOOLS

    // Initialization of the plugin goes here.
    public override void _EnterTree()
    {
        base._EnterTree();
        GD.Print("EnterTree");

        if (!ResourceLoader.Exists(SceneManagerPath))
        {
            GD.Print("No SceneManager Data!");
            ManagerData = new SceneManagerData();
        }
        else
        {
            var temp = ResourceLoader.Load<Resource>(SceneManagerPath);
            GD.Print(temp.GetType());
            ManagerData = (SceneManagerData)temp;
        }

        EditorDock = GD.Load<PackedScene>("res://addons/SceneManager/LevelDock.tscn").Instantiate<Control>();
        AddControlToDock(DockSlot.LeftUl, EditorDock);
    }

    public bool Add(PackedScene Scene) => ManagerData.Add(Scene);

    public void Remove(string SceneName) => ManagerData.Remove(SceneName);

    public bool New()
    {
        LevelCommon Level = (LevelCommon)NewLevel.Instantiate();
        return false;
    }

    public List<string> SceneNames
    {
        get
        {
            if (ManagerData != null && ManagerData.Levels != null)
            {
                return ManagerData.Levels.Keys.ToList<string>();
            }
            return null;
        }
    }

    public void ChangeSceneInEditor(string SceneName)
    {
        if (ManagerData.Levels != null)
        {
            EditorInterface.Singleton.OpenSceneFromPath(ManagerData.Levels[SceneName].ResourcePath);
        }
    }

    public override void _ExitTree()
    {
        StartNewGame -= NewGame;
        ChangeScene -= SwitchLevel;
        ChangeSceneWithExit -= LoadSubScene;
        ResetLevel -= Reset;

        RemoveControlFromDocks(EditorDock);
        EditorDock.Free();
        GD.Print(ResourceSaver.Save(ManagerData, SceneManagerPath));
    }
#endif

    public LevelCommon GetLevel(string LevelName)
    {
        return ManagerData.Level(LevelName);
    }

    public void LoadSubScene(PackedScene subscene, Player p, Exit exit)
    {
        CallDeferred(nameof(CallDeferredSub), subscene.Instantiate<LevelCommon>(), p);
    }

    void CallDeferredSub(SubLevel toLoad, Player Player, LevelType type)
    {
        CurrentScene.ExitLevel();
        GetTree().Root.RemoveChild(CurrentScene);
        //activeSubScene = toLoad;
        //GetTree().Root.AddChild(activeSubScene);
        //activeSubScene.EnterLevel(Player, type);
    }

    void CallDefferedSwitch(LevelCommon toLoad, Player Player, LevelType type)
    {
        if (CurrentScene != null)
        {
            CurrentScene.ExitLevel();
            GetTree().Root.RemoveChild(CurrentScene);
            CurrentScene.Free();
        }
        else
        {
            GetTree().Root.RemoveChild(GetTree().CurrentScene);
        }
        _CurrentScene = toLoad;
        GetTree().Root.AddChild(CurrentScene);
        GetTree().CurrentScene = _CurrentScene;
        _CurrentScene.EnterLevel(Player);
    }

    void NewGame()
    {
        LevelCommon scene = _NewGameScene.Instantiate<LevelCommon>();
        SwitchLevel(scene.LevelName, null);
        StartNewGame -= NewGame;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }


}
