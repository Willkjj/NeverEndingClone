public struct InhabitantData
{
	//genetic data
	public byte gender;
	public byte trait1;
	public byte trait2;
	public byte flaw;
	public byte ideal;

	//non-genetic data
	public ushort birthYear;
	public byte age;
	public byte job;
	public byte firstName;
	public byte lastName;

	//family data
	public uint id;
	public uint motherID;
	public uint fatherID;
}
