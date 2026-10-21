using SharpQuest.Contracts;
using Capstone.Core.Models.Enums;

namespace Capstone.Core.Models.Books;

public abstract class Book : IItem
{
    public required string Id { get; init; }

    public string DisplayName { get; init; } = null!;

    public BookType Category { get; set; }

    public double Price { get; set; }

    public abstract string Describe();

    public static string Classify(IItem item) => item switch
    {
        null => throw new ArgumentNullException(nameof(item)),
        BorrowedBook => "out",
        AvailableBook => "available",
        _ => "something else",
    };
}
