using Itmo.ObjectOrientedProgramming.Lab1.Results;
using Itmo.ObjectOrientedProgramming.Lab1.Routs;
using Itmo.ObjectOrientedProgramming.Lab1.Trains;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

public class TrainRouteTests
{
    [Fact]
    public void Test1_ShouldSucceed_WhenTrainAcceleratesAndPassesPlainPath()
    {
        var train = new Train(weight: 1000, force: 5000, timeStep: 0.1);

        var segments = new List<IRouteSegment>
        {
            new PoweredPath(length: 100, force: 2000),
            new PlainPath(length: 200),
        };

        var route = new Route(segments, finishSpeedLimit: 300);
        ResultWithValue<double> result = route.Start(train);

        Assert.True(result.IsSuccess, result.Message);

        double expectedTime = 20.0;
        Assert.InRange(result.Value, expectedTime, expectedTime + 0.2);
    }

    [Fact]
    public void Test2_ShouldFail_WhenTrainExceedsRouteSpeedLimit()
    {
        var train = new Train(weight: 1000, force: 5000, timeStep: 0.1);

        var segments = new List<IRouteSegment>
        {
            new PoweredPath(length: 100, force: 5000),
            new PlainPath(length: 200),
        };

        var route = new Route(segments, finishSpeedLimit: 20);
        ResultWithValue<double> result = route.Start(train);

        Assert.False(result.IsSuccess);
        Assert.Equal("Finish speed limit exceeded", result.Message);
    }

    [Fact]
    public void Test3_ShouldSucceed_WhenTrainPassesStationWithinLimits()
    {
        var train = new Train(weight: 1000, force: 5000, timeStep: 0.1);

        var segments = new List<IRouteSegment>
        {
            new PoweredPath(length: 100, force: 2000),
            new PlainPath(length: 200),
            new Station(unloadTime: 5, loadTime: 10, speedLimit: 25),
            new PlainPath(length: 200),
        };

        var route = new Route(segments, finishSpeedLimit: 30);
        ResultWithValue<double> result = route.Start(train);

        Assert.True(result.IsSuccess, result.Message);

        double expectedTime = 45.0;
        Assert.InRange(result.Value, expectedTime, expectedTime + 0.2);
    }

    [Fact]
    public void Test4_ShouldFail_WhenTrainExceedsStationSpeedLimit()
    {
        var train = new Train(weight: 1000, force: 5000, timeStep: 0.1);

        var segments = new List<IRouteSegment>
        {
            new PoweredPath(length: 100, force: 5000),
            new Station(unloadTime: 5, loadTime: 10, speedLimit: 1),
            new PlainPath(length: 200),
        };

        var route = new Route(segments, finishSpeedLimit: 50);
        ResultWithValue<double> result = route.Start(train);

        Assert.False(result.IsSuccess);
        Assert.Equal("Station speed limit exceeded", result.Message);
    }

    [Fact]
    public void Test5_ShouldFail_WhenTrainPassesStationButExceedsRouteLimit()
    {
        var train = new Train(weight: 1000, force: 5000, timeStep: 0.1);

        var segments = new List<IRouteSegment>
        {
            new PoweredPath(length: 100, force: 5000),
            new PlainPath(length: 200),
            new Station(unloadTime: 5, loadTime: 10, speedLimit: 35),
            new PlainPath(length: 100),
        };

        var route = new Route(segments, finishSpeedLimit: 30);
        ResultWithValue<double> result = route.Start(train);

        Assert.False(result.IsSuccess);
        Assert.Equal("Finish speed limit exceeded", result.Message);
    }

    [Fact]
    public void Test6_ShouldSucceed_WhenTrainSlowsBeforeStationAndBeforeFinish()
    {
        var train = new Train(weight: 1000, force: 5000, timeStep: 0.1);

        var segments = new List<IRouteSegment>
        {
            new PoweredPath(length: 100, force: 5000),
            new PlainPath(length: 100),
            new PoweredPath(length: 50, force: -5000),
            new Station(unloadTime: 5, loadTime: 10, speedLimit: 25),
            new PlainPath(length: 100),
            new PoweredPath(length: 100, force: 5000),
            new PlainPath(length: 100),
            new PoweredPath(length: 90, force: -5000),
        };

        var route = new Route(segments, finishSpeedLimit: 25);
        ResultWithValue<double> result = route.Start(train);

        Assert.True(result.IsSuccess, result.Message);

        double expectedTime = 40.0;
        Assert.InRange(result.Value, expectedTime - 0.3, expectedTime + 0.3);
    }

    [Fact]
    public void Test7_ShouldFail_WhenTrainStartsOnPlainPathWithoutForce()
    {
        var train = new Train(weight: 1000, force: 5000, timeStep: 0.1);

        var segments = new List<IRouteSegment>
        {
            new PlainPath(length: 100),
        };

        var route = new Route(segments, finishSpeedLimit: 50);
        ResultWithValue<double> result = route.Start(train);

        Assert.False(result.IsSuccess);
        Assert.Equal("The train can't move: the speed and acceleration are zero", result.Message);
    }

    [Fact]
    public void Test8_ShouldFail_WhenTrainSpeedBecomesNegativeOnPoweredPath()
    {
        var train = new Train(weight: 1000, force: 5000, timeStep: 0.1);

        var segments = new List<IRouteSegment>
        {
            new PoweredPath(length: 100, force: 2000),
            new PoweredPath(length: 100, force: -4000),
        };

        var route = new Route(segments, finishSpeedLimit: 100);
        ResultWithValue<double> result = route.Start(train);

        Assert.False(result.IsSuccess);
        Assert.Equal("The speed has become negative", result.Message);
    }
}