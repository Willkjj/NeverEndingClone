using System.Collections.Generic;
using Godot;

public class IdealDefinition
{
    public string displayName;
    public string description;

    public byte[] exclusiveWith; //Traits/flaws it is exclusive with. See the TraitDatabase
}
public static class IdealDatabase
{
    public static readonly Dictionary<Ideal,IdealDefinition> Definitions = new()
    {
        [Ideal.Perfection] = new IdealDefinition
        {
            displayName = "Perfection",
            description = "I must strive to become the best version of myself I can be"
        }
    };
}