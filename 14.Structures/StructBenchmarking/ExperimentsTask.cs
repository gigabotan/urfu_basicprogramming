using System.Collections.Generic;

namespace StructBenchmarking;

public enum TaskKind
{
    Class,
    Struct
}

public interface ITaskFactory
{
    ITask CreateTask(int fieldCount, TaskKind kind);
}

public class ArrayCreationTaskFactory : ITaskFactory
{
    public ITask CreateTask(int fieldCount, TaskKind kind)
    {
        return kind == TaskKind.Class
            ? new ClassArrayCreationTask(fieldCount)
            : new StructArrayCreationTask(fieldCount);
    }
}

public class MethodCallTaskFactory : ITaskFactory
{
    public ITask CreateTask(int fieldCount, TaskKind kind)
    {
        return kind == TaskKind.Class
            ? new MethodCallWithClassArgumentTask(fieldCount)
            : new MethodCallWithStructArgumentTask(fieldCount);
    }
}

public class Experiments
{
    private static ChartData BuildChartData(
        IBenchmark benchmark,
        int repetitionsCount,
        ITaskFactory taskFactory,
        string title)
    {
        var classesTimes = new List<ExperimentResult>();
        var structuresTimes = new List<ExperimentResult>();

        foreach (var fieldCount in Constants.FieldCounts)
        {
            var classTime = benchmark.MeasureDurationInMs(
                taskFactory.CreateTask(fieldCount, TaskKind.Class),
                repetitionsCount);
            var structTime = benchmark.MeasureDurationInMs(
                taskFactory.CreateTask(fieldCount, TaskKind.Struct),
                repetitionsCount);

            classesTimes.Add(new ExperimentResult(fieldCount, classTime));
            structuresTimes.Add(new ExperimentResult(fieldCount, structTime));
        }

        return new ChartData
        {
            Title = title,
            ClassPoints = classesTimes,
            StructPoints = structuresTimes,
        };
    }

    public static ChartData BuildChartDataForArrayCreation(
        IBenchmark benchmark, int repetitionsCount)
    {
        return BuildChartData(
            benchmark,
            repetitionsCount,
            new ArrayCreationTaskFactory(),
            "Create array");
    }

    public static ChartData BuildChartDataForMethodCall(
        IBenchmark benchmark, int repetitionsCount)
    {
        return BuildChartData(
            benchmark,
            repetitionsCount,
            new MethodCallTaskFactory(),
            "Call method with argument");
    }
}
