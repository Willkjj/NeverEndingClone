using Godot;

public class TileData
{
    public Vector2I Coords;
    public BiomeType Biome;
    public bool IsMountain;
    public bool IsWater;
    public TileResources Resources;
}

public class TileResources
{
    public int Wood;
    public int Stone;
}