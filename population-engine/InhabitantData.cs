using System.Collections.Generic;

public struct InhabitantData
{
	//genetic data
	public Gender gender;
	public TraitName trait1;
	public TraitName trait2;
	public TraitName flaw;
	public byte ideal;
	public byte strength;
	public byte dexterity;
	public byte constitution;
	public byte intelligence;
	public byte wisdom;
	public byte charisma;
	//non-genetic data
	public ushort birthYear;
	public byte age;
	public Job job;
	public byte firstName;
	public byte lastName;

	//family data
	public uint id;
	public uint? motherID;
	public uint? fatherID;
}
