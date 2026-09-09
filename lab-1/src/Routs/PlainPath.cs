using Itmo.ObjectOrientedProgramming.Lab1.Results;
using Itmo.ObjectOrientedProgramming.Lab1.Trains;

namespace Itmo.ObjectOrientedProgramming.Lab1.Routs;

public class PlainPath : IRouteSegment
{
    public double Length { get; }

    public PlainPath(double length)
    {
        if (length <= 0)
        {
            throw new ArgumentOutOfRangeException("length", "must be > 0");
        }

        Length = length;
    }

    public ResultWithValue<double> TravelSegment(Train train)
    {
        train.ResetAcceleration();
        return train.Travel(Length);
    }
}