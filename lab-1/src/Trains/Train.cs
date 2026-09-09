using Itmo.ObjectOrientedProgramming.Lab1.Results;

namespace Itmo.ObjectOrientedProgramming.Lab1.Trains;

public class Train
{
    public double Weight { get; }

    public double Speed { get; private set; }

    public double Acceleration { get; private set; }

    public double MaxAllowableForce { get; }

    public double TimeStep { get; }

    public Train(double weight, double force, double timeStep)
    {
        if (weight <= 0)
        {
            throw new ArgumentOutOfRangeException("weight", "must be > 0");
        }

        if (force <= 0)
        {
            throw new ArgumentOutOfRangeException("force", "must be > 0");
        }

        if (timeStep <= 0)
        {
            throw new ArgumentOutOfRangeException("timeStep", "must be > 0");
        }

        Weight = weight;
        Speed = 0;
        Acceleration = 0;
        MaxAllowableForce = force;
        TimeStep = timeStep;
    }

    public Result ApplyForce(double force)
    {
        if (Math.Abs(force) > MaxAllowableForce)
        {
            return Result.Fail("Applied force exceeds the maximum allowable limit");
        }

        Acceleration = force / Weight;
        return Result.Success();
    }

    public ResultWithValue<double> Travel(double distance)
    {
        double travelTime = 0;
        double remains = distance;

        if (distance <= 0)
        {
            return ResultWithValue<double>.Success(0);
        }

        if (Speed == 0 && Acceleration == 0)
        {
            return ResultWithValue<double>.Fail("The train can't move: the speed and acceleration are zero");
        }

        while (remains > 0)
        {
            double resultingSpeed = Speed + (Acceleration * TimeStep);
            if (resultingSpeed < 0)
            {
                return ResultWithValue<double>.Fail("The speed has become negative");
            }

            double distanceTraveled = resultingSpeed * TimeStep;
            if (distanceTraveled <= 0)
            {
                return ResultWithValue<double>.Fail("No progress: resulting speed is zero");
            }

            remains -= distanceTraveled;
            travelTime += TimeStep;
            Speed = resultingSpeed;
        }

        return ResultWithValue<double>.Success(travelTime);
    }

    public void ResetAcceleration()
    {
        Acceleration = 0;
    }
}