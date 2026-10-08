using SharpQuest.Contracts;
using System.Text.Json;

namespace Capstone.Core.Models;

public record BookDTO : IItemMapper
{
    public string Id { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;

    public string Kind {  get; set; } = string.Empty;

    public double Price { get; set; }

    public string ToJson(IItem item)
    {
        string JsonString;
        try 
        {
            JsonString = JsonSerializer.Serialize(item); 
    }
        catch (JsonException e)
        {
            Console.WriteLine(e.Message);
            return String.Empty;
        }
        return JsonString;
    }

    public IItem? FromJson(string json)
    {
        BookDTO? dto;

        try
        {
            dto = JsonSerializer.Deserialize<BookDTO>(json);
            if (dto == null)
            {
                throw new ArgumentNullException("The JSON contains an empty object");
            }
            return new Book { Id = dto.Id, DisplayName = dto.DisplayName, Price = dto.Price, Kind = dto.Kind };
        }
        catch (JsonException)
        {
            return null;
        }
    }
}
