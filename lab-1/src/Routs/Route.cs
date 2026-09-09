using Itmo.ObjectOrientedProgramming.Lab1.Results;
using Itmo.ObjectOrientedProgramming.Lab1.Trains;

namespace Itmo.ObjectOrientedProgramming.Lab1.Routs;

public class Route
{
    public IReadOnlyList<IRouteSegment> Segments { get; }

    public double FinishSpeedLimit { get; }

    public Route(IReadOnlyList<IRouteSegment> segments, double finishSpeedLimit)
    {
        if (segments == null || segments.Count == 0)
        {
            throw new ArgumentNullException("segments", "Route must have at least one segment");
        }

        if (finishSpeedLimit < 0)
        {
            throw new ArgumentOutOfRangeException("finishSpeedLimit", "must be >= 0");
        }

        Segments = segments;
        FinishSpeedLimit = finishSpeedLimit;
    }

    public ResultWithValue<double> Start(Train train)
    {
        if (train == null)
        {
            return ResultWithValue<double>.Fail("Train is null");
        }

        double totalTime = 0;

        foreach (IRouteSegment currentSegment in Segments)
        {
            ResultWithValue<double> currentSuccess = currentSegment.TravelSegment(train);

            if (!currentSuccess.IsSuccess)
            {
                return ResultWithValue<double>.Fail(currentSuccess.Message);
            }
            else
            {
                totalTime += currentSuccess.Value;
            }
        }

        if (train.Speed > FinishSpeedLimit)
        {
            return ResultWithValue<double>.Fail("Finish speed limit exceeded");
        }

        return ResultWithValue<double>.Success(totalTime);
    }
}