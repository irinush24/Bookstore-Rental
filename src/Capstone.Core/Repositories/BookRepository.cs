using Capstone.Core.Models;
using Capstone.Core.Models.DTOs;
using SharpQuest.Contracts;
using System.Text.Json;

namespace Capstone.Core.Repositories;

public class BookRepository
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
    };

    public static async Task SaveAsync(IItemStore store, String path)
    {
        if (store == null)
            throw new ArgumentNullException("The store is empty");
        if (path == null)
            throw new ArgumentException("The path for saving is empty");

        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
            throw new InvalidOperationException("Cannot save to incomplete path");

        var rows = store.All().Select(BookDTO.FromDomain).ToList();

        var tempPath = path + ".tmp";
        await using (var stream = File.Create(tempPath))
        {
            await JsonSerializer.SerializeAsync(stream, rows, Options);
        }

        File.Move(tempPath, path, overwrite: true);
    }

    public static async Task<IItemStore> LoadAsync(String path)
    {
        if (String.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException("Cannot write to an empty path");

        var library = new Library();
        if (File.Exists(path) == false)
            return library;

        try
        {
            await using var stream = File.OpenRead(path);
            var rows = await JsonSerializer.DeserializeAsync<List<BookDTO>>(stream, Options);

            if (rows is null)
                throw new InvalidDataException("The JSON file contained null instead of a list books");

            foreach (var row in rows)
                library.Add(row.ToDomain());

            return library;
        }
        catch (JsonException ex)
        {
            throw new InvalidDataException($"The file at '{path}' is not valid JSON.", ex);
        }
    }
}
