using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class NameDefinition
{
    public string Name;
    public string NamePosition;
    public string Gender;
}

public static class NameDatabase
{
    public static readonly List<NameDefinition> Names = ReadFromFile();
    static List<NameDefinition> ReadFromFile()
    {
        string json = File.ReadAllText("./population-engine/names.json");
        List<NameDefinition> names = JsonConvert.DeserializeObject<List<NameDefinition>>(json);
        return names;
    }
}