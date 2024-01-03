using Godot;
using System;
using Godot.Collections;

[Tool]
public partial class Level : LevelCommon
{

    // Take note. SubLevels or scene's can be anything from screens on a 2D tilemap to rooms in a 3D space
    // They're Sub or attached leves or spaces to the currently loaded main one.
    [Export]
    Dictionary<string, PackedScene> sublevels;

    [Export]
    public Array<Exit> exits;

    // Optional Checkpoint.
    protected Checkpoint currentCheckpoint;

    SceneManager scenes;

    LevelCommon ActiveSubScene;

    // Currently active subScene. Otherwise null
    Level subScene;

    Camera2D camera;

    Rect2 mapLimits;
    Vector2 mapCellsize;

    public override void EnterLevel(Player p)
    {
        base.EnterLevel(p);
        AddChild(Player);
        CreateAudioStream();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ResetLevel()
    {
        ExitLevel();
        EnterLevel(Player);
    }

    public void Checkpoint(Checkpoint newCheckpoint)
    {
        if (currentCheckpoint != null)
        {
            currentCheckpoint.Deactivate();
        }
        newCheckpoint.isActive = true;
        currentCheckpoint = newCheckpoint;
    }

    // Clear the enemies and other data from the scene.
    // Ensure the scene closes properly before changing.
    public override void ExitLevel()
    {
        RemoveChild(Player);
    }
}
