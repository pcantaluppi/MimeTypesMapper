using System.Collections.Concurrent;
using MimeTypesMapper.Infrastructure;

namespace MimeTypesMapper;

public sealed class MimeTypesHandler
{
    private static readonly Lazy<MimeTypesHandler> _lazyLoader =
        new(() => new MimeTypesHandler(), LazyThreadSafetyMode.ExecutionAndPublication);
    private readonly ConcurrentDictionary<string, string> _mimeTypes;

    private MimeTypesHandler()
    {
        _mimeTypes = new ConcurrentDictionary<string, string>(
            StringComparer.InvariantCultureIgnoreCase
        );
        LoadDefaultMimeTypes();
    }

    public static MimeTypesHandler Instance => _lazyLoader.Value;

    private void LoadDefaultMimeTypes()
    {
        // Default MIME types
        _mimeTypes.TryAdd(".pdf", "application/pdf");
        _mimeTypes.TryAdd(".jpg", "image/jpeg");
        _mimeTypes.TryAdd(".jpeg", "image/jpeg");
        _mimeTypes.TryAdd(".png", "image/png");
        _mimeTypes.TryAdd(".doc", "application/msword");
        _mimeTypes.TryAdd(
            ".docx",
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
        );
        _mimeTypes.TryAdd(".xls", "application/vnd.ms-excel");
        _mimeTypes.TryAdd(
            ".xlsx",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        );

        // Additional MIME types
        _mimeTypes.TryAdd(".html", "text/html");
        _mimeTypes.TryAdd(".htm", "text/html");
        _mimeTypes.TryAdd(".css", "text/css");
        _mimeTypes.TryAdd(".js", "application/javascript");
        _mimeTypes.TryAdd(".json", "application/json");
        _mimeTypes.TryAdd(".xml", "application/xml");
        _mimeTypes.TryAdd(".txt", "text/plain");
        _mimeTypes.TryAdd(".csv", "text/csv");
        _mimeTypes.TryAdd(".zip", "application/zip");
        _mimeTypes.TryAdd(".tar", "application/x-tar");
        _mimeTypes.TryAdd(".rar", "application/x-rar-compressed");
        _mimeTypes.TryAdd(".gif", "image/gif");
        _mimeTypes.TryAdd(".bmp", "image/bmp");
        _mimeTypes.TryAdd(".tif", "image/tiff");
        _mimeTypes.TryAdd(".tiff", "image/tiff");
        _mimeTypes.TryAdd(".mp3", "audio/mpeg");
        _mimeTypes.TryAdd(".wav", "audio/wav");
        _mimeTypes.TryAdd(".mp4", "video/mp4");
        _mimeTypes.TryAdd(".avi", "video/x-msvideo");
        _mimeTypes.TryAdd(".mov", "video/quicktime");
        _mimeTypes.TryAdd(".wmv", "video/x-ms-wmv");
        _mimeTypes.TryAdd(".flv", "video/x-flv");
    }

    public string GetMimeType(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new MimeTypeException("File path cannot be null or empty.");
        }

        var extension = Path.GetExtension(filePath).ToLowerInvariant();
        return _mimeTypes.GetValueOrDefault(extension, "application/octet-stream");
    }

    public void AddOrUpdateMimeType(string extension, string mimeType)
    {
        if (string.IsNullOrWhiteSpace(extension))
        {
            throw new MimeTypeException("Extension cannot be null or empty.");
        }

        if (string.IsNullOrWhiteSpace(mimeType))
        {
            throw new MimeTypeException("MIME type cannot be null or empty.");
        }

        _mimeTypes.AddOrUpdate(extension.ToLowerInvariant(), mimeType, (key, oldValue) => mimeType);
        Logger.Log($"MIME type for {extension} updated to {mimeType}");
    }

    public void LoadMimeTypesFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new MimeTypeException($"The file {filePath} does not exist.");
        }

        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length == 2)
            {
                AddOrUpdateMimeType(parts[0], parts[1]);
            }
        }
        Logger.Log($"MIME types loaded from {filePath}");
    }

    public void SaveMimeTypesToFile(string filePath)
    {
        var lines = new List<string>();
        foreach (var kvp in _mimeTypes)
        {
            lines.Add($"{kvp.Key},{kvp.Value}");
        }
        File.WriteAllLines(filePath, lines);
        Logger.Log($"MIME types saved to {filePath}");
    }
}
