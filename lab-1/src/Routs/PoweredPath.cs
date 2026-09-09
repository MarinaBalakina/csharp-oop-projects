using Itmo.ObjectOrientedProgramming.Lab1.Results;
using Itmo.ObjectOrientedProgramming.Lab1.Trains;

namespace Itmo.ObjectOrientedProgramming.Lab1.Routs;

public class PoweredPath : IRouteSegment
{
    public double Length { get; }

    public double Force { get; }

    public PoweredPath(double length, double force)
    {
        if (length <= 0)
        {
            throw new ArgumentOutOfRangeException("length", "must be > 0");
        }

        Length = length;
        Force = force;
    }

    public ResultWithValue<double> TravelSegment(Train train)
    {
        Result result = train.ApplyForce(Force);
        if (!result.IsSuccess)
        {
            return ResultWithValue<double>.Fail(result.Message);
        }

        return train.Travel(Length);
    }
}