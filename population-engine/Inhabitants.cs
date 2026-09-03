using System;
using System.Collections;
using Godot;

public partial class Inhabitants : Node
{
	public InhabitantData[] Population { get; private set;}

	public override void _Ready()
	{
		base._Ready();

        Population = new InhabitantData[250];
	}
}
