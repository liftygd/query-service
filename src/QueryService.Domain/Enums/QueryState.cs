namespace QueryService.Domain.Enums;

/// <summary>
/// Типы состояний, в котором может находится запрос.
/// </summary>
public enum QueryState
{
    NotSet = 0,
    Pending = 1,
    Completed = 2,
    Failed = 3
}