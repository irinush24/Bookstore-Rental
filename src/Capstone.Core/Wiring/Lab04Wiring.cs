using Capstone.Core.Models;
using Capstone.Core.Models.Books;
using SharpQuest.Contracts;

namespace Capstone.Core.Wiring;

public class Lab04Wiring : ILab04Wiring
{
    public Type BaseType => typeof(Book);

    public IReadOnlyList<Type> KindTypes => new List<Type> { typeof(BorrowedBook), typeof(AvailableBook)};

    public int AddAll(IItemStore store, params IItem[] items)
    {
        return ((Library)store).AddAll(items);
    }

    public string Classify(IItem item)
    {
        return Book.Classify(item);
    }

    public IItem CreateOfKind(int kindIndex, int seed)
    {
        switch (kindIndex)
        {
            case 1: { return new AvailableBook { Id = $"borrowed-{seed}", DisplayName = $"Thing number {seed}", ShelfName = $" Shelf no {seed}" }; }
            default:
                {
                    return new BorrowedBook { Id = $"available-{seed}", DisplayName = $"Thing number {seed}", BorrowerId = $"client#{seed}" };
                }
        }
    }

    public IItemStore CreateStore() => new Library();

    public string Describe(IItem item)
    {
        return ((Book)item).Describe();
    }

    public IReadOnlyList<string> DescribeAll(IItemStore store)
    {
        return ((Library)store).DescribeAll();
    }

    public bool TryFindByName(IItemStore store, string name, out IItem? found)
    {
        return ((Library)store).TryFindByName(name, out found);
    }
}
