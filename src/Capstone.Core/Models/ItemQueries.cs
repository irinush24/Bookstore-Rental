using SharpQuest.Contracts;

namespace Capstone.Core.Models;

public static class ItemQueries
{
    public static List<IItem> Longest(this IEnumerable<IItem> items, int count)
    {
        if(items == null)
            throw new ArgumentNullException("No items in store");
        if(count < 0)
            throw new ArgumentOutOfRangeException("Cannot use negative count");
        return items
            .OrderByDescending(i => i.DisplayName.Length)
            .ThenBy(i => i.Id, StringComparer.Ordinal)
            .Take(count)
            .ToList();
    }

    public static List<IItem> Sorted(IEnumerable<IItem> items)
    {
        return items.OrderBy(i => i.DisplayName, StringComparer.OrdinalIgnoreCase)
                    .ThenBy(i => i.Id, StringComparer.Ordinal).ToList();
    }

    public static List<IItem> Page(IEnumerable<IItem> items, int skip, int take)
    {
        if (items == null)
            throw new ArgumentNullException("No items were provided");

        if (skip < 0)
            throw new ArgumentOutOfRangeException("Cannot use negative skip value");
        if (take < 0)
            throw new ArgumentOutOfRangeException("Cannot use negative take value");

        return Sorted(items).Skip(skip)
                            .Take(take)
                            .ToList();
    }

    public static StoreStats Summary(IEnumerable<IItem> items)
    {
        if (items == null)
            throw new ArgumentNullException("Cannot summarize empty store");

        if (!items.Any())
            return new StoreStats(0, 0, null, 0.0);

        int itemsCount = items.Count();

        int noDistinctInitials = items.Select(x => char.ToLowerInvariant(x.DisplayName.FirstOrDefault())).Distinct().Count();
        string? longestName = items.MaxBy(x => x.DisplayName.Length)?.DisplayName;
        double averageNameLength = items.Average(x => x.DisplayName.Length);

        return new StoreStats(itemsCount, noDistinctInitials, longestName, averageNameLength);
    }
}
