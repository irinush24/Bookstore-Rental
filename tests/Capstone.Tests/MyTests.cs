using Xunit;
using Capstone.Core.Models;
using Capstone.Core.Models.Enums;

namespace Capstone.Core.Tests
{
    public class MyTests
    {
        [Fact]
        public void Shelf_Add_DuplicateId_ThrowsArgumentException()
        {
            var shelf = new Shelf();
            var book1 = new Book { Id = "123", DisplayName = "Book 1" };
            var book2 = new Book { Id = "123", DisplayName = "Book 2" };
            shelf.Add(book1);

            var exception = Assert.Throws<ArgumentException>(() => shelf.Add(book2));
            Assert.Equal("The item you are trying to add is already part of the collection", exception.Message);
        }

        [Fact]
        public void Customer_Borrow_Null_ThrowsArgumentNullException()
        {
            var customer = new Customer { UserName = "TestUser" };

            Assert.Throws<ArgumentNullException>(() => customer.Borrow(null!));
        }

        [Fact]
        public void Customer_Borrow_AddsBookToBorrowedBooks()
        {
            var customer = new Customer { UserName = "JohnDoe" };
            var book = new Book { Id = "b1", DisplayName = "The Hobbit" };

            customer.Borrow(book);

            Assert.Contains(book, customer.borrowedBooks);
            Assert.Single(customer.borrowedBooks);
        }

        [Fact]
        public void Customer_Return_RemovesBookFromBorrowedBooks()
        {
            var customer = new Customer { UserName = "JaneDoe" };
            var book = new Book { Id = "b2", DisplayName = "1984" };
            customer.Borrow(book);

            customer.Return(book);

            Assert.DoesNotContain(book, customer.borrowedBooks);
            Assert.Empty(customer.borrowedBooks);
        }

        [Theory]
        [InlineData("100", true)]
        [InlineData("999", false)]
        public void Shelf_FindById_ReturnsCorrectItemOrNull(string searchId, bool shouldExist)
        {
            var shelf = new Shelf();
            var book = new Book { Id = "100", DisplayName = "Moby Dick" };
            shelf.Add(book);

            var result = shelf.FindById(searchId);

            if (shouldExist)
            {
                Assert.NotNull(result);
                Assert.Equal(searchId, result.Id);
            }
            else
            {
                Assert.Null(result);
            }
        }

        [Fact]
        public void Shelf_Remove_DeletesItemAndReturnsTrue()
        {
            var shelf = new Shelf();
            var book = new Book { Id = "555", DisplayName = "Dune" };
            shelf.Add(book);

            bool isRemoved = shelf.Remove("555");

            Assert.True(isRemoved);
            Assert.Equal(0, shelf.Count);
        }
    }
}