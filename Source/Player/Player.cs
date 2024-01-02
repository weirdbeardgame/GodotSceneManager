using Godot;
using System;

public partial class Player : Node2D
{
    Area2D body;
    public Sprite2D playerSprite;
    public AnimationPlayer AnimationPlayer;
    public Vector2 direction = Vector2.Right;

    Camera2D camera;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playerSprite = (Sprite2D)GetNode("CenterContainer/Player");
        AnimationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");

        SceneManager.StartNewGame += NewGame;
        camera = (Camera2D)GetNode("Camera2D");
    }

    public void NewGame()
    {
        SceneManager.StartNewGame -= NewGame;
    }

    public void ActivateCamera()
    {
        camera.Enabled = true;
        camera.MakeCurrent();
    }

    public void DeactivateCamera()
    {
        camera.Enabled = false;
    }

    public void CenterCamera()
    {
        Vector2 CameraPosition = camera.GlobalPosition;
        CameraPosition.X = GlobalPosition.X;
        camera.Position = CameraPosition;
    }


    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        //MoveAndSlide();
    }
}
