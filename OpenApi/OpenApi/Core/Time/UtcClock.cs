namespace OpenApi.Core.Time;

public class UtcClock : IClock
{
    public DateTime Current()
    {
        return DateTime.UtcNow;
    }
}