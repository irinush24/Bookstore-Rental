using Capstone.Core.Models;
using Capstone.Core.Models.Books;
using Capstone.Core.Services;

class Program
{
    static void Main(string[] args)
    {
        var library = new Library();
        var emailApp = new EmailNotificationService();

        library.BookOverdue += emailApp.SendEmail;

        var dailyTimer = new System.Timers.Timer(5000);

        dailyTimer.Elapsed += (sender, e) =>
        {
            library.CheckForOverdueBooks();
        };

        dailyTimer.AutoReset = true;
        dailyTimer.Start();

        Console.WriteLine("Bookstore system is running. Press Enter to exit...");

         library.Add(new BorrowedBook { Id = "123", BorrowerId = "user1", DueDate = DateTime.Now.AddDays(-1) });

        Console.ReadLine();
    }
}