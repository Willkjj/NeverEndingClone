using System;
using System.Collections;
using Godot;


public partial class Inhabitants : Node
{
	public InhabitantData[] Population { get; private set;}

	public override void _Ready()
	{
		base._Ready();

		// Population = new InhabitantData[WorldState.Instance.startingPopulation];
		Population = GenerateRandomInhabitants(1);
		// GD.Print("population:", Population);
	}

	public InhabitantData[] GenerateRandomInhabitants(int quantity)
	{

		InhabitantData[] _dummyInhabitants = [];

		for (int i = 0; i < quantity; i++)
		{
			Random random = new Random();
			InhabitantData _dummyInhabitant = new InhabitantData();

			// public byte trait1;
			// public byte trait2;
			// public byte flaw;
			// public byte ideal;

			// //non-genetic data
			// public ushort birthYear;
			// public byte age;
			// public byte job;
			// public byte firstName;
			// public byte lastName;

			// //family data
			// public uint id;
			// public uint motherID;
			// public uint fatherID;

			_dummyInhabitant.gender = (byte)random.Next(0,1);
			_dummyInhabitant.trait1 = (byte)random.Next(0,15);
			_dummyInhabitant.trait2 = (byte)random.Next(0,15);
			_dummyInhabitant.flaw = (byte)random.Next(0,1);
			_dummyInhabitant.ideal = (byte)random.Next(0,1);
			_dummyInhabitant.ideal = (byte)random.Next(0,1);

			_dummyInhabitant.age = (byte)random.Next(0,80);
		}

		return _dummyInhabitants;
	} 
}
