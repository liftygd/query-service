namespace Application.Options;

public sealed class WorkerOptions
{
    public const string SectionName = "QueryWorker";
    public int ProcessingDurationSeconds { get; set; } = 60;
    public int WorkerPollIntervalSeconds { get; set; } = 1;
    public int WorkerBatchSize { get; set; } = 20;
}