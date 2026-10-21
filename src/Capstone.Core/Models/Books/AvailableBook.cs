namespace Capstone.Core.Models.Books;

public class AvailableBook : Book
{
    public required string ShelfName { get; set; }

    public override string Describe()
    {
        return "Available book";
    }
}
