using Capstone.Core.Models;
using Capstone.Core.Models.Books;
using SharpQuest.Contracts;

namespace Capstone.Core.Wiring;

public class Lab07Wiring : ILab07Wiring
{
    public Type ExtensionHost => typeof(ItemQueries);

    public IItem CreateSample(int seed) => new AvailableBook { Id = $"thing-{seed}", DisplayName = $"Thing number {seed}", ShelfName = " Shelf no {seed}" };

    public IItemStore CreateStore() => new Library();

    public IReadOnlyList<IItem> Longest(IEnumerable<IItem> items, int n)
    {
        return ItemQueries.Longest(items, n);
    }

    public IEnumerable<string> Names(IItemStore store)
    {
        return ((Library)store).Select(x => x.DisplayName);
    }

    public IReadOnlyList<IItem> Page(IEnumerable<IItem> items, int skip, int take)
    {
        return ItemQueries.Page(items, skip, take);
    }

    public IReadOnlyList<IItem> Sorted(IEnumerable<IItem> items)
    {
        return ItemQueries.Sorted(items);
    }

    public StoreStats Summary(IEnumerable<IItem> items)
    {
        return ItemQueries.Summary(items);
    }
}
