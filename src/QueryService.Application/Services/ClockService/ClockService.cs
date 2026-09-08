namespace Application.Services.ClockService;

public sealed class ClockService : IClockService
{
    public DateTime UtcNow => DateTime.UtcNow;
}