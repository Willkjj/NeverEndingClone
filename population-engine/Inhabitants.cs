using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;


public partial class Inhabitants : Node
{
	public InhabitantData[] Population { get; private set;} = [];

	public override void _Ready()
	{
		base._Ready();

		// Population = new InhabitantData[WorldState.Instance.startingPopulation];
		Population = GenerateRandomInhabitants(WorldState.Instance.StartingPopulation);
		Debug();
	}
	public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);
	}
	public void Debug()
	{
		GD.Print(GetSingleDisplayString(Population[0]));
	}

	public InhabitantData[] GenerateRandomInhabitants(uint quantity)
	{

		InhabitantData[] _dummyInhabitants = new InhabitantData[quantity];

		for (int i = 0; i < quantity; i++)
		{
			Random random = new Random();
			InhabitantData _dummyInhabitant = new InhabitantData();

			//genetic
			_dummyInhabitant.gender = (Gender)random.Next(0,2);
			_dummyInhabitant.trait1 = PickTrait1();
			_dummyInhabitant.trait2 = PickTrait2(_dummyInhabitant.trait1);
			_dummyInhabitant.flaw = PickFlaw(_dummyInhabitant.trait1, _dummyInhabitant.trait2);
			_dummyInhabitant.ideal = PickIdeal();

			//sets str, dex, con, int, wis, cha
			PickStat(ref _dummyInhabitant);


			//non genetic
			_dummyInhabitant.age = (byte)random.Next(0,80);
			_dummyInhabitant.birthYear = (byte)(0 - _dummyInhabitant.age);
			_dummyInhabitant.firstName = PickFirstName(ref _dummyInhabitant);
			_dummyInhabitant.lastName = PickLastName(ref _dummyInhabitant);

			//family data
			_dummyInhabitant.id = (uint)i;
			_dummyInhabitant.motherID = null;
			_dummyInhabitant.fatherID = null;

			_dummyInhabitants[i] = _dummyInhabitant;
		}

		return _dummyInhabitants;
	} 
	public TraitName PickTrait1()
	{
		TraitName traitName = (TraitName)Random.Shared.Next(0,TraitDatabase.Definitions.Count);
		TraitDefinition trait = TraitDatabase.Definitions[traitName];

		var possibleTraits = TraitDatabase.Definitions
			.Where(pair => !pair.Value.isFlaw)
			.Select(pair => pair.Key)
			.ToList();

		return possibleTraits[Random.Shared.Next(possibleTraits.Count)];
	}
	public TraitName PickTrait2(TraitName trait1)
	{

		var exclusiveList = TraitDatabase.Definitions[trait1].exclusiveWith ?? System.Array.Empty<byte>();
		var possibleTraits = TraitDatabase.Definitions
			.Where(pair =>
			pair.Key != trait1 &&
			!exclusiveList.Contains((byte)pair.Key) &&
			!pair.Value.isFlaw)
			.Select(pair => pair.Key)
			.ToList();

		return possibleTraits[Random.Shared.Next(possibleTraits.Count)];
		
	}
	public TraitName PickFlaw(TraitName trait1, TraitName trait2)
	{
		var exclusiveList = TraitDatabase.Definitions[trait1].exclusiveWith ?? System.Array.Empty<byte>()
			.Concat(TraitDatabase.Definitions[trait2].exclusiveWith);
		var possibleFlaws = TraitDatabase.Definitions
			.Where(pair =>
			pair.Value.isFlaw &&
			!exclusiveList.Contains((byte)pair.Key))
			.Select(pair => pair.Key)
			.ToList();

		return possibleFlaws[Random.Shared.Next(possibleFlaws.Count)];
	}
	public Ideal PickIdeal()
	{
		var possibleIdeals = IdealDatabase.Definitions
			.Select(pair => pair.Key)
			.ToList();

		return possibleIdeals[Random.Shared.Next(possibleIdeals.Count)];
	}
	public void PickStat(ref InhabitantData inhabitant)
	{
		byte[] stats = [7,7,7,7,7,7];
		int pointsAvailable = 20;
		int maxPoints = 13;

		while (pointsAvailable != 0)
		{
			int currentStat = Random.Shared.Next(stats.Count());

			int points = Random.Shared.Next(0,3);

			points = (points <= pointsAvailable) ? points : points =  pointsAvailable;
			points = (points + stats[currentStat] <= maxPoints) ? points : points =  maxPoints - stats[currentStat];

			stats[currentStat] += (byte)points;
			pointsAvailable -= points;
		}
		inhabitant.strength = stats[0];
		inhabitant.dexterity = stats[1];
		inhabitant.constitution = stats[2];
		inhabitant.intelligence = stats[3];
		inhabitant.wisdom = stats[4];
		inhabitant.charisma = stats[5];
		
	}
	public void PickStat(ref InhabitantData inhabitant, byte motherID, byte fatherID)
	{
	}
	public Name PickFirstName(ref InhabitantData inhabitant)
	{
		//2 accesss the neuter name dictionary
		int speciesByte = (int)inhabitant.species;
		int genderByte = (int)inhabitant.gender;

		var possibleNames = NameDatabase.Definitions[speciesByte][genderByte].Select(pair => pair.Key).ToList();

		possibleNames.AddRange(NameDatabase.Definitions[speciesByte][2].Select(pair => pair.Key).ToList());

		return possibleNames[Random.Shared.Next(possibleNames.Count())];

	}
	public Name PickLastName(ref InhabitantData inhabitant)
	{
		//3 accesses the last names dictionary
		int speciesByte = (int)inhabitant.species;

		var possibleNames = NameDatabase.Definitions[speciesByte][3].Select(pair => pair.Key).ToList();


		return possibleNames[Random.Shared.Next(possibleNames.Count())];

	}
	public string GetDisplayString()
	{
		//Displays the population in a human readable format


		StringBuilder stringBuilder = new StringBuilder("Population: \n\n");
		foreach (var inhabitant in Population)
		{
			stringBuilder.Append(GetSingleDisplayString(inhabitant));

		}
		return stringBuilder.ToString();
	}
	public string GetSingleDisplayString(InhabitantData inhabitant)
	{
		var traitDict = TraitDatabase.Definitions;
		var idealDict = IdealDatabase.Definitions;
			var nameDefinitions = NameDatabase.Definitions[(int)inhabitant.species];

			var firstNameDefinition = nameDefinitions[(int)inhabitant.gender]
					.TryGetValue(inhabitant.firstName, out var firstName)
						? firstName
						: nameDefinitions[2][inhabitant.firstName];

			var lastNameDefinition = nameDefinitions[3][inhabitant.lastName];
			

			return $"{inhabitant.gender}\n{traitDict[inhabitant.trait1].DisplayName}\n{traitDict[inhabitant.trait2].DisplayName}\n{traitDict[inhabitant.flaw].DisplayName}\n{idealDict[inhabitant.ideal].displayName}\nSTR:{inhabitant.strength}\nDEX:{inhabitant.dexterity}\nCON:{inhabitant.constitution}\nINT:{inhabitant.intelligence}\nWIS:{inhabitant.wisdom}\nCHA:{inhabitant.charisma}\nAGE:{inhabitant.age}\nBD:{inhabitant.birthYear}\n{inhabitant.job}\n{firstNameDefinition._displayName}\n{lastNameDefinition._displayName}\n{inhabitant.id}\nMother:{inhabitant.motherID}\nFather:{inhabitant.fatherID}\n\n";

	}
	public string GetByteString()
	{
		//Displays the raw byte string of each inhabitant in the population
		StringBuilder stringBuilder = new StringBuilder("Population:\n");
		foreach (var inhabitant in Population)
		{
			stringBuilder.Append($"{(byte)inhabitant.gender}{(byte)inhabitant.trait1}{(byte)inhabitant.trait2}{(byte)inhabitant.flaw}{(byte)inhabitant.ideal}{inhabitant.strength}{inhabitant.dexterity}{inhabitant.constitution}{inhabitant.intelligence}{inhabitant.wisdom}{inhabitant.charisma}{inhabitant.age}{inhabitant.birthYear}{(byte)inhabitant.job}{(ushort)inhabitant.firstName}{(ushort)inhabitant.lastName}{inhabitant.id}{inhabitant.motherID}{inhabitant.fatherID}\n");
		}
		return stringBuilder.ToString();
	}
}
