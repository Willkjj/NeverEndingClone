using System;
using Godot;

public partial class TileHighlight : Node2D
{
    [Export]
    public Color BorderColor = Colors.Yellow;

    [Export]
    public float ScreenSpaceThickness = 2f;

    private Vector2 _tileSize;
    private float _currentZoom = 1f;

    public void SetTileSize(Vector2 size)
    {
        _tileSize = size;
        QueueRedraw();
    }

    public void SetZoom(float zoom)
    {
        _currentZoom = zoom;
        QueueRedraw();
    }

    public override void _Draw()
    {
        var rect = new Rect2(-_tileSize / 2, _tileSize);
        float worldThickness = ScreenSpaceThickness / _currentZoom;
        DrawRect(rect, BorderColor, filled: false, width: worldThickness, antialiased: true);
    }
}
