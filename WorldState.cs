using Godot;

public partial class WorldState : Node
{
  public static WorldState Instance { get; private set; }
  public TileData[,] Tiles;

  public override void _Ready()
  {
    Instance = this;
    Tiles = new TileData[256, 256];

    Clock.Instance.Tick += OnClockTick;
  }

  private void OnClockTick(int tickCount)
  {
    GD.Print($"Clock tick fired: {tickCount}");
  }

  // Remove the event listener if worldstate is ever unloaded (it shouldn't)
  public override void _ExitTree()
  {
    if (Clock.Instance != null)
    {
      Clock.Instance.Tick -= OnClockTick;
    }
  }
}
