using Capstone.Core.Models;
using SharpQuest.Contracts;
using Capstone.Core.Tools;

namespace Capstone.Core.Wiring;

public class Lab03Wiring : ILab03Wiring
{
    public IItem CreateSample(int seed) => new Book { Id = $"thing-{seed}", DisplayName = $"Thing number {seed}" };

    public IItemStore CreateStore() => new Shelf();

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
