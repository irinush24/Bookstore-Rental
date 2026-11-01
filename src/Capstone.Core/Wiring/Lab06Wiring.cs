using Capstone.Core.Models;
using Capstone.Core.Models.Books;
using SharpQuest.Contracts;

namespace Capstone.Core.Wiring;

public class Lab06Wiring : ILab06Wiring
{
    public Type PublisherType => typeof(Library);

    public IItem CreateSample(int seed) => new AvailableBook { Id = $"thing-{seed}", DisplayName = $"Thing number {seed}", ShelfName = " Shelf no {seed}" };

    public IItemStore CreateStore() => new Library();

    public void ForEach(IItemStore store, Action<IItem> action) => ((Library)store).ForEach(action);

    public IReadOnlyList<string> Map(IItemStore store, Func<IItem, string> describe) => ((Library)store).Map(describe).ToList();

    public Action Subscribe(IItemStore store, Action<IItem> handler)
    {
        var library = (Library)store;

        library.ItemAdded += handler;

        return () => library.ItemAdded -= handler;
    }

    public IReadOnlyList<IItem> Where(IItemStore store, Func<IItem, bool> keep) => ((Library)store).Where(keep).ToList();
}
