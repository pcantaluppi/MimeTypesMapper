using System.Collections.Concurrent;
using MimeTypesMapper.Infrastructure;

namespace MimeTypesMapper;

public sealed class MimeTypeManager
{
    private static readonly Lazy<MimeTypeManager> _lazyLoader =
        new(() => new MimeTypeManager(), LazyThreadSafetyMode.ExecutionAndPublication);
    private readonly ConcurrentDictionary<string, string> _mimeTypes;

    private MimeTypeManager()
    {
        _mimeTypes = new ConcurrentDictionary<string, string>(
            StringComparer.InvariantCultureIgnoreCase
        );
        LoadDefaultMimeTypes();
    }

    public static MimeTypeManager Instance => _lazyLoader.Value;

    private void LoadDefaultMimeTypes()
    {
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

        _mimeTypes.AddOrUpdate(extension.ToLowerInvariant(), mimeType,
            (key, oldValue) => mimeType);
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
