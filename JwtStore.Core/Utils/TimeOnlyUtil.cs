namespace JwtStore.Core.Utils;

public static class TimeOnlyUtil
{
    public static TimeOnly UtcNow() =>
        TimeOnly.FromDateTime(DateTime.UtcNow); 
}