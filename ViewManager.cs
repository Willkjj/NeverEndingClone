using System;
using Godot;

public enum AppView
{
    Map,
    Stats,
    Settings,
}

public partial class ViewManager : Control
{
    [Export]
    public NodePath MapViewPath;

    [Export]
    public NodePath StatsViewPath;

    [Export]
    public NodePath SettingsVewPath;

    private Node _mapView;
    private Node _settingsView;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _mapView = GetNode(MapViewPath);
        _settingsView = GetNode(SettingsVewPath);

        ShowView(AppView.Map);
    }

    public void ShowView(AppView view)
    {
        SetActive(_mapView, view == AppView.Map);
        SetActive(_settingsView, view == AppView.Settings);
    }

    private void SetActive(Node view, bool active)
    {
        GD.Print($"{view.Name}: setting active={active}, is CanvasItem={view is CanvasItem}");
        if (view is CanvasItem canvasItem)
        {
            canvasItem.Visible = active;
            GD.Print($"{view.Name}: Visible is now {canvasItem.Visible}");
        }

        view.ProcessMode = active ? Node.ProcessModeEnum.Inherit : Node.ProcessModeEnum.Disabled;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
