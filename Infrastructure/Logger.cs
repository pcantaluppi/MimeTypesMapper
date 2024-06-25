namespace MimeTypesMapper.Infrastructure;

public static class Logger
{
    public static void Log(string message)
    {
        // Here you can add more complex logging, e.g., to a file or logging system
        Console.WriteLine($"{DateTime.Now}: {message}");
    }
}
