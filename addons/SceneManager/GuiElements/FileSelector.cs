using Godot;
using System;

[Tool]
public partial class FileSelector : Node
{
    string path;
    [Export] string BrowseForLabel;

    FileDialog Dialog;
    public Button BrowseButton;
    RichTextLabel label;
    RichTextLabel PathField;

    // Called when the node enters the scene tree for the first time.
    public override void _EnterTree()
    {
        Dialog = GetNode<FileDialog>("FileDialog");
        BrowseButton = GetNode<Button>("Browse");
        label = GetNode<RichTextLabel>("SelectLabel");
        PathField = GetNode<RichTextLabel>("PathLabel");
    }

    public void GetPath(string p)
    {
        path = p;
        PathField.Text = path;
        Dialog.Hide();
        Dialog.FileSelected -= GetPath;
    }

    public string Open()
    {
        Dialog.Show();
        Dialog.FileSelected += GetPath;
        return path;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        label.Text = BrowseForLabel;
    }
}
