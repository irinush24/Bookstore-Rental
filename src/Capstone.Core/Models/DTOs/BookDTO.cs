using Capstone.Core.Models.Books;
using Capstone.Core.Models.Enums;
using SharpQuest.Contracts;
using System.Text.Json;

namespace Capstone.Core.Models.DTOs;

public record BookDTO : IItemMapper
{
    public string Kind { get; set; } = null!;
    public string Id { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public BookType Category { get; set; }
    public double Price { get; set; }
    public string? BorrowerId { get; set; }
    public string? ShelfName { get; set; }

    public string ToJson(IItem item)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        string JsonString;
        try
        {
            JsonString = JsonSerializer.Serialize(item, item.GetType(), options);
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
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        BookDTO? dto;

        try
        {
            dto = JsonSerializer.Deserialize<BookDTO>(json);
        }
        catch (JsonException)
        {
            return null;
        }
        if (dto is null || string.IsNullOrWhiteSpace(dto.Id))
            return null;

        if (dto.BorrowerId != null)
            return new BorrowedBook
            {
                Id = dto.Id,
                BorrowerId = dto.BorrowerId,
                Category = dto.Category,
                Price = dto.Price,
                DisplayName = dto.DisplayName ?? "Unknown"
            };

        return new AvailableBook
        {
            Id = dto.Id,
            DisplayName = dto.DisplayName ?? "Unknown",
            Category = dto.Category,
            Price = dto.Price,
            ShelfName = dto.ShelfName ?? "Unknown shelf"
        };
    }

    public static BookDTO FromDomain(IItem item)
    {
        return item switch
        {
            BorrowedBook b => new BookDTO
            {
                Kind = nameof(BorrowedBook),
                Id = b.Id,
                DisplayName = b.DisplayName,
                Category = b.Category,
                Price = b.Price,
                BorrowerId = b.BorrowerId,
            },
            AvailableBook a => new BookDTO
            {
                Kind = nameof(AvailableBook),
                Id = a.Id,
                DisplayName = a.DisplayName,
                Category = a.Category,
                Price = a.Price,
                ShelfName = a.ShelfName,
            },
            _ => throw new InvalidDataException($"Unknown item type: {item.GetType().Name}")
        };
    }

    public IItem ToDomain()
    {
        if (string.IsNullOrWhiteSpace(Id))
            throw new InvalidDataException("Book entry is missing an Id");

        return Kind switch
        {
            nameof(BorrowedBook) => new BorrowedBook
            {
                Id = Id,
                DisplayName = DisplayName,
                Category = Category,
                Price = Price,
                BorrowerId = BorrowerId ?? string.Empty
            },
            nameof(AvailableBook) => new AvailableBook
            {
                Id = Id,
                DisplayName = DisplayName,
                Category = Category,
                Price = Price,
                ShelfName = ShelfName ?? string.Empty
            },
            _ => throw new InvalidDataException($"Unknown book kind: '{Kind}'.")
        };
    }
}
