using Godot;

public partial class TileSelector : Node2D
{
    [Export]
    public NodePath TerrainGeneratorPath;

    [Export]
    public NodePath HoverHighlightPath;

    [Export]
    public NodePath SelectedHighlightPath;

    [Export]
    public NodePath SidebarPath;

    private TileMapLayer _terrain;
    private TileHighlight _hoverHighlight;
    private TileHighlight _selectedHighlight;
    private Vector2I _lastHoverCell = new Vector2I(-1, -1);

    public override void _Ready()
    {
        _terrain = GetNode<TileMapLayer>(TerrainGeneratorPath);
        _hoverHighlight = GetNode<TileHighlight>(HoverHighlightPath);
        _selectedHighlight = GetNode<TileHighlight>(SelectedHighlightPath);

        Vector2 tileSize = _terrain.TileSet.TileSize;
        _hoverHighlight.SetTileSize(tileSize);
        _selectedHighlight.SetTileSize(tileSize);

        _hoverHighlight.Visible = false;
        _selectedHighlight.Visible = false;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion)
        {
            UpdateHover();
        }
        else if (
            @event is InputEventMouseButton mb
            && mb.Pressed
            && mb.ButtonIndex == MouseButton.Left
        )
        {
            SelectHoveredTile();
        }
    }

    private Vector2I GetCellUnderMouse()
    {
        Vector2 localPos = _terrain.ToLocal(GetGlobalMousePosition());
        return _terrain.LocalToMap(localPos);
    }

    private bool IsValidCell(Vector2I cell)
    {
        return cell.X >= 0 && cell.X < 256 && cell.Y >= 0 && cell.Y < 256;
    }

    private void UpdateHover()
    {
        Vector2I cell = GetCellUnderMouse();

        if (!IsValidCell(cell))
        {
            _hoverHighlight.Visible = false;
            return;
        }

        if (cell == _lastHoverCell)
            return;

        _lastHoverCell = cell;
        _hoverHighlight.Visible = true;
        _hoverHighlight.GlobalPosition = _terrain.ToGlobal(_terrain.MapToLocal(cell));
    }

    private void SelectHoveredTile()
    {
        Vector2I cell = GetCellUnderMouse();
        if (!IsValidCell(cell))
            return;

        _selectedHighlight.Visible = true;
        _selectedHighlight.GlobalPosition = _terrain.ToGlobal((_terrain.MapToLocal(cell)));

        TileData tile = WorldState.Instance.Tiles[cell.X, cell.Y];
        //TODO - Update Sidebar Info
    }
}
