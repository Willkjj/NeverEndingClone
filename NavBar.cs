using System;
using Godot;

public partial class NavBar : CanvasLayer
{
    [Export]
    public NodePath ViewManagerPath;

    [Export]
    public NodePath MapButtonPath;

    [Export]
    public NodePath SettingsButtonPath;

    private ViewManager _viewManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _viewManager = GetNode<ViewManager>(ViewManagerPath);

        GetNode<Button>(MapButtonPath).Pressed += () => _viewManager.ShowView(AppView.Map);
        GetNode<Button>(SettingsButtonPath).Pressed += () =>
        {
            GD.Print("Settings Button Clicked");
            _viewManager.ShowView(AppView.Settings);
        };
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
