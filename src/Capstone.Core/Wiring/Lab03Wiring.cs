using Capstone.Core.Models;
using Capstone.Core.Models.Books;
using Capstone.Core.Tools;
using SharpQuest.Contracts;

namespace Capstone.Core.Wiring;

public class Lab03Wiring : ILab03Wiring
{
    public IItem CreateSample(int seed) => new AvailableBook { Id = $"thing-{seed}", DisplayName = $"Thing number {seed}", ShelfName = " Shelf no {seed}" };

    public IItemStore CreateStore() => new Library();

    public IReadOnlyList<IItem> Search(IItemStore store, string query)
    {
        List<IItem> matches = new List<IItem>();
        foreach (var item in store.All())
        {
            if (MySearchMatcher.Matches(item.DisplayName, query))
                matches.Add(item);
        }
        return matches;
    }
}
