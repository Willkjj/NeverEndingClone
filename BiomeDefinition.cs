using System;
using System.Collections.Generic;
using Godot;

public class BiomeDefinition
{
    public string DisplayName;
    public Vector2I AtlasCoord;
}

public static class BiomeDatabase
{
    public static readonly Dictionary<BiomeType, BiomeDefinition> Definitions = new()
    {
        [BiomeType.Tundra] = new BiomeDefinition
        {
            DisplayName = "Tundra",
            AtlasCoord = new Vector2I(0, 0),
        },
        [BiomeType.Taiga] = new BiomeDefinition
        {
            DisplayName = "Taiga",
            AtlasCoord = new Vector2I(1, 0),
        },
        [BiomeType.SnowyForest] = new BiomeDefinition
        {
            DisplayName = "Snowy Forest",
            AtlasCoord = new Vector2I(2, 0),
        },
        [BiomeType.Plains] = new BiomeDefinition
        {
            DisplayName = "Plains",
            AtlasCoord = new Vector2I(3, 0),
        },
        [BiomeType.Grassland] = new BiomeDefinition
        {
            DisplayName = "Grassland",
            AtlasCoord = new Vector2I(4, 0),
        },
        [BiomeType.Forest] = new BiomeDefinition
        {
            DisplayName = "Forest",
            AtlasCoord = new Vector2I(5, 0),
        },
        [BiomeType.Desert] = new BiomeDefinition
        {
            DisplayName = "Desert",
            AtlasCoord = new Vector2I(6, 0),
        },
        [BiomeType.Savanna] = new BiomeDefinition
        {
            DisplayName = "Savanna",
            AtlasCoord = new Vector2I(7, 0),
        },
        [BiomeType.Jungle] = new BiomeDefinition
        {
            DisplayName = "Jungle",
            AtlasCoord = new Vector2I(8, 0),
        },
    };
}
