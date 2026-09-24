using Godot;

public partial class WorldState : Node
{
	public static WorldState Instance { get; private set; }
	public TileData[,] Tiles;
	public uint StartingPopulation { get; private set;}
	public ushort CurrentYear { get; private set;}
	
	

	public override void _Ready()
	{
		Instance = this;
		Tiles = new TileData[256, 256];

		StartingPopulation = 250;
		CurrentYear = 0;

	}
}
