namespace MimeTypesMapper.Infrastructure;

public class MimeTypesException : Exception
{
    public MimeTypesException(string message)
        : base(message) { }

    public MimeTypesException(string message, Exception innerException)
        : base(message, innerException) { }
}
