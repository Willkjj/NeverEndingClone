using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public class TraitDefinition
{
    public string DisplayName;
    public int id;
    public int[] exclusiveWith;

}


public static class TraitDatabase
{
    public static readonly List<TraitDefinition> Traits = ReadFromFile();

    static List<TraitDefinition> ReadFromFile()
    {
        string json = File.ReadAllText("./population-engine/traits.json");
        List<TraitDefinition> traits = JsonConvert.DeserializeObject<List<TraitDefinition>>(json);
        return traits;
    }
    
}
