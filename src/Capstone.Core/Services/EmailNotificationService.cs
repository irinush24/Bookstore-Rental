using Capstone.Core.Models.Books;

namespace Capstone.Core.Services;

public class EmailNotificationService
{
    public void SendEmail(BorrowedBook book, string borrowerId)
    {
        Console.WriteLine($"Email: Hey user {borrowerId}, your copy of '{book.DisplayName}' is overdue!");
    }
}
