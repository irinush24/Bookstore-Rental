using Capstone.Core.Models;
using Capstone.Core.Models.Books;
using Capstone.Core.Models.Enums;
using Capstone.Core.Repositories;
using SharpQuest.Contracts;

namespace Capstone.Core.Wiring;

public class Lab08Wiring : ILab08Wiring
{
    private static readonly BookType[] Categories = Enum.GetValues<BookType>();
    public IReadOnlyList<Type> KindTypes => [typeof(AvailableBook), typeof(BorrowedBook)];

    public IItem CreateOfKind(int kindIndex, int seed)
    {
        var category = Categories[Math.Abs(seed) % Categories.Length];
        var price = 10.0 + seed;

        return kindIndex switch
        {
            0 => new AvailableBook
            {
                Id = $"avail-{seed}",
                DisplayName = $"Available Book {seed}",
                Category = category,
                Price = price,
                ShelfName = $"Shelf-{seed}"
            },
            1 => new BorrowedBook
            {
                Id = $"borrowed-{seed}",
                DisplayName = $"Borrowed Book {seed}",
                Category = category,
                Price = price,
                BorrowerId = $"borrower-{seed}"
            },
            _ => throw new ArgumentOutOfRangeException(nameof(kindIndex), $"Unknown kind index: {kindIndex}")
        };
    }

    public IItem CreateSample(int seed) => CreateOfKind(0, seed);

    public IItemStore CreateStore() => new Library();

    public Task<IItemStore> LoadAsync(string path) => BookRepository.LoadAsync(path);

    public Task SaveAsync(IItemStore store, string path) => BookRepository.SaveAsync(store, path);
}
