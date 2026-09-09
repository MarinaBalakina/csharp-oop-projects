using Itmo.ObjectOrientedProgramming.Lab1.Results;
using Itmo.ObjectOrientedProgramming.Lab1.Trains;

namespace Itmo.ObjectOrientedProgramming.Lab1.Routs;

public class Station : IRouteSegment
{
    public double UnloadTime { get; }

    public double LoadTime { get; }

    public double SpeedLimit { get; }

    public Station(double unloadTime, double loadTime, double speedLimit)
    {
        if (unloadTime < 0)
        {
            throw new ArgumentOutOfRangeException("unloadTime", "must be >= 0");
        }

        if (loadTime < 0)
        {
            throw new ArgumentOutOfRangeException("loadTime", "must be >= 0");
        }

        if (speedLimit < 0)
        {
            throw new ArgumentOutOfRangeException("speedLimit", "must be >= 0");
        }

        UnloadTime = unloadTime;
        LoadTime = loadTime;
        SpeedLimit = speedLimit;
    }

    public ResultWithValue<double> TravelSegment(Train train)
    {
        if (train.Speed > SpeedLimit)
        {
            return ResultWithValue<double>.Fail("Station speed limit exceeded");
        }

        double stationTime = UnloadTime + LoadTime;
        train.ResetAcceleration();

        return ResultWithValue<double>.Success(stationTime);
    }
}