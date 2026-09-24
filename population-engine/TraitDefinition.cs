using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public class TraitDefinition
{
    public string DisplayName;
    public bool isFlaw;
    public byte[] exclusiveWith;

}


public static class TraitDatabase
{
    public static readonly Dictionary<TraitName,TraitDefinition> Definitions = new() {
        [TraitName.Wicked] = new TraitDefinition
        {
            DisplayName = "Wicked",
            isFlaw = true,
            exclusiveWith = [1,2]
        },
        [TraitName.Righteous] = new TraitDefinition
        {
            DisplayName = "Righteous",
            isFlaw = false,
            exclusiveWith = [0]
        },
        [TraitName.Kind] = new TraitDefinition
        {
            DisplayName = "Kind",
            isFlaw = false,
            exclusiveWith = [0]
        },
        [TraitName.Intelligent] = new TraitDefinition
        {
            DisplayName = "Intelligent",
            isFlaw = false,
            exclusiveWith = [4]
        },
        [TraitName.DullMinded] = new TraitDefinition
        {
            DisplayName = "Dull-minded",
            isFlaw = false,
            exclusiveWith = [3]
        },
        [TraitName.Quiet] = new TraitDefinition
        {
            DisplayName = "Quiet",
            isFlaw = false,
            exclusiveWith = [6]
        },
        [TraitName.Talkative] = new TraitDefinition
        {
            DisplayName = "Talkative",
            isFlaw  = false,
            exclusiveWith = [5]
        },
        [TraitName.Brave] = new TraitDefinition
        {
            DisplayName = "Brave",
            isFlaw = false,
            exclusiveWith = [8]
        },
        [TraitName.Cowardly] = new TraitDefinition
        {
            DisplayName = "Cowardly",
            isFlaw = true,
            exclusiveWith = [7]
        },
        [TraitName.Determined] = new TraitDefinition
        {
            DisplayName = "Determined",
            isFlaw = false,
            exclusiveWith = [10]
        },
        [TraitName.Noncommittal] = new TraitDefinition
        {
            DisplayName = "Non-committal",
            isFlaw = false,
            exclusiveWith = [9]
        },
        [TraitName.Calm] = new TraitDefinition
        {
            DisplayName = "Calm",
            isFlaw = false,
            exclusiveWith = [12]
        },
        [TraitName.Excitable] = new TraitDefinition
        {
            DisplayName = "Excitable",
            isFlaw = false,
            exclusiveWith = [11]
        },
        [TraitName.Curious] = new TraitDefinition
        {
            DisplayName = "Curious",
            isFlaw = false,
            exclusiveWith = []
        },
        [TraitName.Friendly] = new TraitDefinition
        {
            DisplayName = "Friendly",
            isFlaw = false,
            exclusiveWith = [15]
        },
        [TraitName.Loner] = new TraitDefinition
        {
            DisplayName = "Loner",
            isFlaw = false,
            exclusiveWith = [14]
        }
    };
}
