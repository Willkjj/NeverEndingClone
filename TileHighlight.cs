using System;
using Godot;

public partial class TileHighlight : Node2D
{
    [Export]
    public Color BorderColor = Colors.Yellow;

    [Export]
    public int BorderSize = 2;

    private Vector2 _tileSize;

    public void SetTileSize(Vector2 size)
    {
        _tileSize = size;
        QueueRedraw();
    }

    public override void _Draw()
    {
        var rect = new Rect2(-_tileSize / 2, _tileSize);
        DrawRect(rect, BorderColor, filled: false, width: BorderSize);
    }
}
