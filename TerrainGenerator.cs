using System;
using Godot;

public partial class TerrainGenerator : Godot.TileMapLayer
{
    int width = 256;
    int height = 256;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var tempNoise = new FastNoiseLite();
        tempNoise.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
        tempNoise.Frequency = 0.015f;
        tempNoise.Seed = 789431;

        var moistureNoise = new FastNoiseLite();
        moistureNoise.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
        moistureNoise.Frequency = 0.015f;
        moistureNoise.Seed = 243188;

        var mountainNoise = new FastNoiseLite();
        mountainNoise.FractalType = FastNoiseLite.FractalTypeEnum.Ridged;
        mountainNoise.Frequency = 0.01f;
        mountainNoise.Seed = 234452738;

        var landNoise = new FastNoiseLite();
        landNoise.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
        landNoise.Frequency = 0.01f;
        landNoise.Seed = 54250807;

        float[,] temperatureMap = new float[width, height];
        float[,] moistureMap = new float[width, height];
        bool[,] isLand = new bool[width, height];

        float islandRadius = 0.98f;
        float falloffPower = 3f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float nx = (x / (float)width) * 2f - 1f;
                float ny = (y / (float)height) * 2f - 1f;
                float distFromCenter = Mathf.Sqrt(nx * nx + ny * ny);

                float raw = landNoise.GetNoise2D(x, y);
                float land = (raw + 1f) / 2f;

                float falloff = Mathf.Pow(
                    Mathf.Clamp(distFromCenter / islandRadius, 0f, 1f),
                    falloffPower
                );
                land -= falloff;

                isLand[x, y] = land >= 0.15f;

                float mountainVal = (mountainNoise.GetNoise2D(x, y * 0.4f) + 1f) / 2f;

                float rawTemp = tempNoise.GetNoise2D(x, y);
                var temp = (rawTemp + 1f) / 2f;
                temperatureMap[x, y] = temp;

                float rawMoist = moistureNoise.GetNoise2D(x, y);
                var moist = (rawMoist + 1f) / 2f;
                moistureMap[x, y] = moist;

                var biome = GetTileForClimate(temp, moist);

                var tile = new TileData
                {
                    Coords = new Vector2I(x, y),
                    Biome = biome,
                    IsMountain = mountainVal > 0.85f,
                    IsWater = !isLand[x, y],
                    Resources = GetResourcesForBiome(biome, mountainVal > 0.85f, !isLand[x, y])
                };

                WorldState.Instance.Tiles[x, y] = tile;

                Vector2I atlasCoord;

                if (tile.IsWater)
                {
                    atlasCoord = new Vector2I(10, 0);
                }
                else if (tile.IsMountain)
                {
                    atlasCoord = new Vector2I(9, 0);
                }
                else
                {
                    atlasCoord = BiomeDatabase.Definitions[tile.Biome].AtlasCoord;
                }

                SetCell(new Vector2I(x, y), 0, atlasCoord);
            }
        }
    }

    private BiomeType GetTileForClimate(float temp, float moist)
    {
        int tempBand = temp < 0.33f ? 0 : (temp < 0.66f ? 1 : 2);
        int moistureBand = moist < 0.33f ? 0 : (moist < 0.66f ? 1 : 2);

        Console.WriteLine($"{temp}, {moist}");

        return (tempBand, moistureBand) switch
        {
            (0, 0) => BiomeType.Tundra, // cold, dry
            (0, 1) => BiomeType.Taiga, // cold, med   -> taiga
            (0, 2) => BiomeType.SnowyForest, // cold, wet   -> snowy forest
            (1, 0) => BiomeType.Plains, // mild, dry   -> plains
            (1, 1) => BiomeType.Grassland, // mild, med   -> grassland
            (1, 2) => BiomeType.Forest, // mild, wet   -> forest
            (2, 0) => BiomeType.Desert, // hot, dry    -> desert
            (2, 1) => BiomeType.Savanna, // hot, med    -> savanna
            (2, 2) => BiomeType.Jungle, // hot, wet    -> jungle
            _ => BiomeType.Plains,
        };
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    private TileResources GetResourcesForBiome(BiomeType biome, bool isMountain, bool isWater)
    {
        Random rnd = new Random();
        if (isWater){
            return new TileResources{
                Wood = 0,
                Stone = 0
            };
        }
        if (isMountain){
            return new TileResources{
                Wood = 0,
                Stone = rnd.Next(7500, 10000)
            };
        }

        switch (biome) {
            case BiomeType.Tundra:
                return new TileResources{
                    Wood = rnd.Next(300, 500),
                    Stone = rnd.Next(0, 0)
                };
            case BiomeType.Taiga:
                return new TileResources{
                    Wood = rnd.Next(300, 500),
                    Stone = rnd.Next(0, 0)
                };
            case BiomeType.SnowyForest:
                return new TileResources{
                    Wood = rnd.Next(300, 500),
                    Stone = rnd.Next(0, 0)
                };
            case BiomeType.Plains:
                return new TileResources{
                    Wood = rnd.Next(300, 500),
                    Stone = rnd.Next(0, 0)
                };
            case BiomeType.Grassland:
                return new TileResources{
                    Wood = rnd.Next(300, 500),
                    Stone = rnd.Next(0, 0)
                };
            case BiomeType.Forest:
                return new TileResources{
                    Wood = rnd.Next(300, 500),
                    Stone = rnd.Next(0, 0)
                };
            case BiomeType.Desert:
                return new TileResources{
                    Wood = rnd.Next(300, 500),
                    Stone = rnd.Next(0, 0)
                };
            case BiomeType.Savanna:
                return new TileResources{
                    Wood = rnd.Next(300, 500),
                    Stone = rnd.Next(0, 0)
                };
            case BiomeType.Jungle:
                return new TileResources{
                    Wood = rnd.Next(300, 500),
                    Stone = rnd.Next(0, 0)
                };
            default:
                GD.PrintErr("Couldn't Find Biome");
                return new TileResources{
                    Wood = rnd.Next(0, 0),
                    Stone = rnd.Next(0, 0)
                };
        }
    }
}
