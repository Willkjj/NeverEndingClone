using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Godot;


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
	public override void _Process(double delta)
	{
		base._Process(delta);

		if (Input.IsKeyPressed(Key.Ctrl) && Input.IsKeyPressed(Key.Shift) && Input.IsKeyPressed(Key.D))
		{
			GD.Print("hi");
			
		}
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
			_dummyInhabitant.flaw = (byte)random.Next(0,1);
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
		Random random = new Random();
		return (TraitName)random.Next(0,TraitDatabase.Traits.Count);
	}
	public TraitName PickTrait2(TraitName trait1)
	{
		Random random = new Random();

		byte[] _exclusiveList = TraitDatabase.Definitions[trait1].exclusiveWith;
		int _randomTraitIndex = random.Next(TraitDatabase.Traits.Count);
		TraitDefinition _trait2 = TraitDatabase.Traits[_randomTraitIndex];

		while (_exclusiveList.Contains((byte)_randomTraitIndex) || _trait2.isFlaw)
		{
			_randomTraitIndex = random.Next(TraitDatabase.Traits.Count);	
			_trait2 = TraitDatabase.Traits[_randomTraitIndex];
		}

		return (TraitName)_randomTraitIndex;
	}

	public string GetDisplayString()
	{
		//Displays the population in a human readable format

		StringBuilder stringBuilder = new StringBuilder("Population: \n\n");
		foreach (var inhabitant in Population)
		{
			stringBuilder.Append($"{inhabitant.gender}\n{inhabitant.trait1}\n{inhabitant.trait2}\n{inhabitant.flaw}\n{inhabitant.ideal}\n{inhabitant.strength}\n{inhabitant.dexterity}\n{inhabitant.constitution}\n{inhabitant.intelligence}\n{inhabitant.wisdom}\n{inhabitant.charisma}\n{inhabitant.age}\n{inhabitant.birthYear}\n{inhabitant.job}\n{inhabitant.firstName}\n{inhabitant.lastName}\n{inhabitant.id}\n{inhabitant.motherID}\n{inhabitant.fatherID}\n\n");
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
