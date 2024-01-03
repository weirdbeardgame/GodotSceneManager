using Godot;
using System;
using Godot.Collections;

public enum LevelType { DEFAULT, GRASS, ISLAND, ICE, WATER }

[Tool]
public partial class LevelCommon : Node2D
{
    [Export]
    public string LevelName;

    protected Player Player;

    AudioStreamPlayer backgroundPlayer;

    [Export] Resource audioFile;

    [Export] protected LevelType type;

    bool unlocked;
    bool complete;

    // Use this for non static refs or event calls. IE. Needs to spawn player.
    // Scenes = GetNode<SceneManager>("/root/SceneManager");
    protected SceneManager Scenes;

    public bool isComplete
    {
        get
        {
            return complete;
        }
    }

    public bool isUnlocked
    {
        get
        {
            return unlocked;
        }
    }

    public LevelType LevelType
    {
        get
        {
            return type;
        }
    }

    public virtual void EnterLevel(Player p)
    {
        // Audioooooo
        backgroundPlayer = (AudioStreamPlayer)GetNode("BackgroundAudio");

        // This is the case where the player is not in the scene yet but he exists.
        if (p != null && Player == null)
        {
            Player = p;
        }
        // The player is already in the scene, this is a level reset.
        else if ((Player = (Player)GetNode("Player")) != null)
        {
            GD.Print("Player Found");
        }
    }

    public void CreateAudioStream()
    {
        backgroundPlayer.Stream = GD.Load<AudioStream>(audioFile.ResourcePath);
        backgroundPlayer.Play();
    }

    public void CompleteLevel()
    {
        complete = true;
        ExitLevel();
    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        // Since these are Tool scripts IE. Allowed to run in the editor, we need to ensure logic that's borked outside the editor doesn't run
#if !(TOOLS)
        Update();
#endif
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
#if !(TOOLS)
        FixedUpdate();
#endif

    }

    public virtual void ExitLevel()
    {

    }

    public virtual void ResetLevel()
    {

    }

    public virtual void EnterSubLevel(Player Player, Level parent)
    {

    }

    public virtual void ExitSubLevel()
    {

    }
}
