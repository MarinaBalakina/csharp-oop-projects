namespace Itmo.ObjectOrientedProgramming.Lab3.Units.Common;

public sealed class UnitType
{
    public string Name { get; }

    public string Description { get; }

    public int BaseAttack { get; }

    public int BaseHealth { get; }

    public UnitType(string name, string description, int baseAttack, int baseHealth)
    {
        Name = name ?? throw new ArgumentNullException("name");
        Description = description ?? throw new ArgumentNullException("description");

        if (baseAttack < 0)
        {
            throw new ArgumentOutOfRangeException("baseAttack", "Attack cannot be negative.");
        }

        if (baseHealth < 0)
        {
            throw new ArgumentOutOfRangeException("baseHealth", "Health cannot be negative.");
        }

        BaseAttack = baseAttack;
        BaseHealth = baseHealth;
    }
}