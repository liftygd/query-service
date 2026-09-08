namespace Application.Services.ClockService;

/// <summary>
/// Интерфейс для создания единой точки входа при получении текущего времени.
/// </summary>
public interface IClockService
{
    /// <summary>
    /// Текущее время UTC.
    /// </summary>
    DateTime UtcNow { get; }
}