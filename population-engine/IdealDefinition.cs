using System.Collections.Generic;
using Godot;

public class IdealDefinition
{
    public string displayName;
    public string description;

}
public static class IdealDatabase
{
    public static readonly Dictionary<Ideal, IdealDefinition> Definitions = new()
    {
        [Ideal.Perfection] = new IdealDefinition
        {
            displayName = "Perfection",
            description = "I must strive to become the best version of myself I can be"
        },
        [Ideal.Ambition] = new IdealDefinition
        {
            displayName = "Ambition",
            description = "I will become better than all the rest"
        },
        [Ideal.Unity] = new IdealDefinition
        {
            displayName = "Unity",
            description = "The world is better when we all work together"
        },
        [Ideal.Greed] = new IdealDefinition
        {
            displayName = "Greed",
            description = "The sole purpose in life is to acrue as many material posessions as possible"
        },
        [Ideal.People] = new IdealDefinition
        {
            displayName = "People",
            description = "I must protect those who cannot protect themselves"
        },
        [Ideal.Destiny] = new IdealDefinition
        {
            displayName = "Destiny",
            description = "I am destined for greater things"
        },
        [Ideal.Might] = new IdealDefinition
        {
            displayName = "Might",
            description = "The strong rule the weak, and I must be a ruler"
        },
        [Ideal.Sincerity] = new IdealDefinition
        {
            displayName = "Sincerity",
            description = "Truth is the only pure emotion we can express to each other. I must be truthful, and promote truths"
        },
        [Ideal.Logic] = new IdealDefinition
        {
            displayName = "Logic",
            description = "There is much to be gained in this world by avoiding excess emotion and focusing on truthful and unwavering logic"
        },
        [Ideal.Glory] = new IdealDefinition
        {
            displayName = "Glory",
            description = "I will bring glory and renown to me and mine own"
        },
        [Ideal.Freedom] = new IdealDefinition
        {
            displayName = "Freedom",
            description = "All good people in this world have the right to express themselves, and live their for their own will"
        }
    };
}
