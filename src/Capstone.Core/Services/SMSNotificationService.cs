using Capstone.Core.Models.Books;

namespace Capstone.Core.Services;

public class SMSNotificationService
{
    public void SendText(BorrowedBook book, string borrowerId)
    {
        Console.WriteLine($"SMS: User {borrowerId}, please return {book.DisplayName} to the library");
    }
}
