using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Godot;


public partial class Inhabitants : Node
{
	public InhabitantData[] Population { get; private set;} = [];

	public override void _Ready()
	{
		base._Ready();

		// Population = new InhabitantData[WorldState.Instance.startingPopulation];
		Population = GenerateRandomInhabitants(WorldState.Instance.StartingPopulation);
		DisplayPopulation();
	}

	public InhabitantData[] GenerateRandomInhabitants(uint quantity)
	{

		InhabitantData[] _dummyInhabitants = new InhabitantData[quantity];

		for (int i = 0; i < quantity; i++)
		{
			Random random = new Random();
			InhabitantData _dummyInhabitant = new InhabitantData();

			//genetic
			_dummyInhabitant.gender = (byte)random.Next(0,2);
			_dummyInhabitant.trait1 = (byte)random.Next(0,TraitDatabase.Traits.Count + 1);
			_dummyInhabitant.trait2 = (byte)random.Next(0,15);
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

	public void DisplayPopulation()
	{
		GD.Print("Population: \n");
		for (int i = 0; i < Population.Count(); i++)
		{
			InhabitantData inhabitant = Population[i];
			GD.Print($"Inhabitant {inhabitant.id}:",inhabitant.lastName, inhabitant.firstName, inhabitant.gender, inhabitant.age, inhabitant.birthYear, inhabitant.constitution, inhabitant.trait1 );
			GD.Print("\n");
		}
	}
}
