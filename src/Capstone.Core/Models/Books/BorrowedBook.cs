namespace Capstone.Core.Models.Books;

public class BorrowedBook : Book
{    
    public required string BorrowerId { get; set; }

    public override string Describe()
    {
        return "Borrowed Book";
    }
}
