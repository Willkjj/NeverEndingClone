using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
		GD.Print(GetDisplayString());
	}
	public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);
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
			_dummyInhabitant.ideal = (byte)random.Next(0,1);

			_dummyInhabitant.strength = (byte)random.Next(0,21);
			_dummyInhabitant.dexterity = (byte)random.Next(0,21);
			_dummyInhabitant.constitution = (byte)random.Next(0,21);
			_dummyInhabitant.intelligence = (byte)random.Next(0,21);
			_dummyInhabitant.wisdom = (byte)random.Next(0,21);
			_dummyInhabitant.charisma = (byte)random.Next(0,21);

			//non genetic
			_dummyInhabitant.age = (byte)random.Next(0,80);
			_dummyInhabitant.birthYear = (byte)(0 - _dummyInhabitant.age);
			_dummyInhabitant.job = (Job)random.Next(0,Enum.GetValues<Job>().Length);
			_dummyInhabitant.firstName = (byte)random.Next(0,NameDatabase.Names.Count + 1);
			_dummyInhabitant.lastName = (byte)random.Next(0,NameDatabase.Names.Count + 1);

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

	public string GetDisplayString()
	{
		//Displays the population in a human readable format

		StringBuilder stringBuilder = new StringBuilder("Population: \n\n");
		foreach (var inhabitant in Population)
		{
			stringBuilder.Append($"{inhabitant.gender}\n{inhabitant.trait1}\n{inhabitant.trait2}\n{inhabitant.flaw}\n{inhabitant.ideal}\nSTR:{inhabitant.strength}\nDEX:{inhabitant.dexterity}\nCON:{inhabitant.constitution}\nINT:{inhabitant.intelligence}\nWIS:{inhabitant.wisdom}\nCHA:{inhabitant.charisma}\nAGE:{inhabitant.age}\nBD:{inhabitant.birthYear}\n{inhabitant.job}\n{inhabitant.firstName}\n{inhabitant.lastName}\n{inhabitant.id}\n{inhabitant.motherID}\n{inhabitant.fatherID}\n\n");
		}
		return stringBuilder.ToString();
	}
	public string GetByteString()
	{
		//Displays the raw byte string of each inhabitant in the population
		StringBuilder stringBuilder = new StringBuilder("Population:\n");
		foreach (var inhabitant in Population)
		{
			stringBuilder.Append($"{(byte)inhabitant.gender}{(byte)inhabitant.trait1}{(byte)inhabitant.trait2}{inhabitant.flaw}{inhabitant.ideal}{inhabitant.strength}{inhabitant.dexterity}{inhabitant.constitution}{inhabitant.intelligence}{inhabitant.wisdom}{inhabitant.charisma}{inhabitant.age}{inhabitant.birthYear}{(byte)inhabitant.job}{inhabitant.firstName}{inhabitant.lastName}{inhabitant.id}{inhabitant.motherID}{inhabitant.fatherID}\n");
		}
		return stringBuilder.ToString();
	}
}
