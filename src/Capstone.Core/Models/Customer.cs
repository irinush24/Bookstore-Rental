using Capstone.Core.Models.Interfaces;
using SharpQuest.Contracts;

namespace Capstone.Core.Models;

public class Customer : IUser
{
    public string UserName { get; set; } = null!;

    public List<Book> borrowedBooks = new List<Book>();

    public void Borrow(IItem item)
    {
        if (item == null)
            throw new ArgumentNullException("Cannot borrow a null book");
        if(item is Book book)
            borrowedBooks.Add(book);
    }

    public void Return(IItem item)
    {
        if (item == null) throw new ArgumentNullException("Cannot return a null book");
        if(item is Book book)
            borrowedBooks.Remove(book);
    }
}
