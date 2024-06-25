namespace MimeTypesMapper.Infrastructure;

public class MimeTypeException : Exception
{
    public MimeTypeException(string message)
        : base(message) { }

    public MimeTypeException(string message, Exception innerException)
        : base(message, innerException) { }
}
