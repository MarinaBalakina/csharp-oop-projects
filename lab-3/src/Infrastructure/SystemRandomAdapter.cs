using System.Security.Cryptography;

namespace Itmo.ObjectOrientedProgramming.Lab3.Infrastructure;

public class SystemRandomAdapter : IRandom
{
    public SystemRandomAdapter() { }

    public int PickIndex(int maxValue)
    {
        if (maxValue < 0)
            throw new ArgumentOutOfRangeException("maxValue", maxValue, "maxValue must be greater than or equal to 0");

        return RandomNumberGenerator.GetInt32(maxValue);
    }
}
