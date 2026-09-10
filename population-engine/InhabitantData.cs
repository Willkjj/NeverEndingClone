using System.Collections.Generic;

public struct InhabitantData
{
	//genetic data
	public byte gender;
	public byte trait1;
	public byte trait2;
	public byte flaw;
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
