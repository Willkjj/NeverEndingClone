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
    public int Tin;
    public int Copper;
    public int Iron;
    public int Coal;
    public int FreshWater;
    public int Sand;
    public int Clay;
    public int Sticks;
    public int Bones;
    public int ForagedFruits;
    public int ForagedHerbs;
    public int ForagedMeat;
    public int Hides;
    public int Gems;
}
