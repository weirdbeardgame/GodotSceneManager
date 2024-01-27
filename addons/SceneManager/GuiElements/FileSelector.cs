using Godot;
using System;

[Tool]
public partial class FileSelector : Node
{
    public Action FileSelected;

    string _Path;
    [Export] string BrowseForLabel;
    FileDialog Dialog;
    public Button BrowseButton;
    RichTextLabel label;
    RichTextLabel PathField;

    public string Path
    {
        get
        {
            GD.Print("Path Getter: ", _Path);
            return _Path;
        }
    }

    public void SetPathField(string path)
    {
        PathField.Text = path;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _EnterTree()
    {
        Dialog = GetNode<FileDialog>("FileDialog");
        BrowseButton = GetNode<Button>("Browse");
        label = GetNode<RichTextLabel>("SelectLabel");
        PathField = GetNode<RichTextLabel>("PathLabel");
    }

    public bool IsOpen() => Dialog.Visible;

    public void GetPath(string p)
    {
        _Path = p;
        PathField.Text = _Path;
        Dialog.Hide();
        FileSelected.Invoke();
    }

    public void Open(string RootFolder = "", string[] FileFilters = null)
    {
        if (FileFilters != null)
        {
            Dialog.Filters = FileFilters;
        }
        Dialog.RootSubfolder = RootFolder;
        Dialog.Show();
        Dialog.FileSelected += GetPath;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        label.Text = BrowseForLabel;
    }
}
