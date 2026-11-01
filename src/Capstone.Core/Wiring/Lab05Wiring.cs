using Capstone.Core.Models;
using Capstone.Core.Models.Books;
using SharpQuest.Contracts;

namespace Capstone.Core.Wiring;

public class Lab05Wiring : ILab05Wiring
{
    public Type CollectionType => typeof(Library);

    public Type GenericDefinition => typeof(RecentlyUsed<>);

    public IItem At(IItemStore store, int index) => ((Library)store)[index];
    
    public IItem? ById(IItemStore store, string id) => ((Library)store)[id];

    public IItem CreateSample(int seed) => new AvailableBook { Id = $"thing-{seed}", DisplayName = $"Thing number {seed}", ShelfName = " Shelf no {seed}" };

    public IItemStore CreateStore() => new Library();
    
    public IReadOnlyList<IItem> EachInOrder(IItemStore store)
    {
        var items = new List<IItem>();
        foreach(var item in (Library)store)
            items.Add(item);

        return items;
    }

    public IEnumerable<IItem> Matching(IItemStore store, string text) => ((Library)store).Matching(text);

    public IReadOnlyList<T> Recent<T>(int capacity, params T[] pushed)
    {
        var recent = new RecentlyUsed<T>(capacity);

        foreach (var item in pushed)
            recent.Push(item);

        return recent.Items;
    }
}
