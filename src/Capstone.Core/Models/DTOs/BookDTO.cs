using Capstone.Core.Models.Books;
using Capstone.Core.Models.Enums;
using SharpQuest.Contracts;
using System.Text.Json;

namespace Capstone.Core.Models.DTOs;

public record BookDTO : IItemMapper
{
    public string Id { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public BookType Category{  get; set; }

    public double Price { get; set; }

    public string? BorrowerId { get; set; }

    public string? ShelfName { get; set; }

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
        }
        catch (JsonException)
        {
            return null;
        }

        if(dto is null)
            return null;
        if (string.IsNullOrWhiteSpace(dto.Id)) return null;
        if(dto.ShelfName == null) return new BorrowedBook { BorrowerId = dto.BorrowerId?? throw new ArgumentNullException("Cannot instantiate an object with a null ID attribute"), Category = dto.Category, Price = dto.Price, DisplayName = dto.DisplayName, Id = dto.Id};

        return new AvailableBook { Id = dto.Id, DisplayName = dto.DisplayName, Category = dto.Category, Price = dto.Price, ShelfName = dto.ShelfName };
    }
}
