using System;
using Godot;

public partial class TileInfoSidebar : CanvasLayer
{
    [Export]
    public NodePath PanelPath;

    [Export]
    public NodePath CoordsDisplayPath;

    [Export]
    public NodePath BiomeDisplayPath;

    [Export]
    public float SlideDuration = 0.25f;

    private Control _panel;
    private Label _coordsDisplay;
    private Label _biomeDisplay;
    private float _hiddenX;
    private float _visibleX;
    private Tween _tween;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _panel = GetNode<Control>(PanelPath);
        _coordsDisplay = GetNode<Label>(CoordsDisplayPath);
        _biomeDisplay = GetNode<Label>(BiomeDisplayPath);

        _visibleX = _panel.Position.X;
        _hiddenX = _visibleX + _panel.Size.X;
        _panel.Position = new Vector2(_hiddenX, _panel.Position.Y);
    }

    public void UpdateTileInfo(TileData tile)
    {
        var biomeDisplay = tile.IsMountain
            ? "Mountain"
            : (tile.IsWater ? "Water" : BiomeDatabase.Definitions[tile.Biome].DisplayName);
        _coordsDisplay.Text = $"Selected Tile: {tile.Coords.X}, {tile.Coords.Y}";
        _biomeDisplay.Text = $"Biome: {biomeDisplay}";
        SlideIn();
    }

    private void SlideIn()
    {
        _tween?.Kill();
        _tween = CreateTween();
        _tween
            .TweenProperty(_panel, "position:x", _visibleX, SlideDuration)
            .SetTrans(Tween.TransitionType.Cubic)
            .SetEase(Tween.EaseType.Out);
    }

    private void SlideOut()
    {
        _tween?.Kill();
        _tween = CreateTween();
        _tween
            .TweenProperty(_panel, "position:x", _hiddenX, SlideDuration)
            .SetTrans(Tween.TransitionType.Cubic)
            .SetEase(Tween.EaseType.In);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (
            @event.IsActionPressed("ui_cancel")
            || (
                @event is InputEventMouseButton mb
                && mb.Pressed
                && mb.ButtonIndex == MouseButton.Right
            )
        )
        {
            SlideOut();
        }
    }
}
