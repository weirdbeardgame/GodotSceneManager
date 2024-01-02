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
        backgroundPlayer = (AudioStreamPlayer)GetNode("BackgroundAudio");
        if (p != null)
        {
            Player = p;
        }

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
        Update();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        FixedUpdate();
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
