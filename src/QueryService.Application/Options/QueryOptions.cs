namespace Application.Options;

public sealed class QueryOptions
{
    public const string SectionName = "QueryWorker";
    public int ProcessingDurationMS { get; set; } = 60000;
    public int WorkerPollIntervalSeconds { get; set; } = 1;
    public int WorkerBatchSize { get; set; } = 20;
}