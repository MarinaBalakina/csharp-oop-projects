using Itmo.ObjectOrientedProgramming.Lab1.Results;
using Itmo.ObjectOrientedProgramming.Lab1.Trains;

namespace Itmo.ObjectOrientedProgramming.Lab1.Routs;

public interface IRouteSegment
{
    ResultWithValue<double> TravelSegment(Train train);
}