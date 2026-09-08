namespace Application.Services.ClockService;

public class ClockService : IClockService
{
    public DateTime UtcNow => DateTime.UtcNow;
}