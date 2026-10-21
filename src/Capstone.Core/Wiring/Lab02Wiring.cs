using Capstone.Core.Models.Books;
using Capstone.Core.Models.DTOs;
using Capstone.Core.Models;
using SharpQuest.Contracts;

namespace Capstone.Core.Wiring;

public sealed class Lab02Wiring : ILab02Wiring
{
    public IItemStore CreateStore() => new Library();
    public IItemMapper CreateMapper() => new BookDTO();
    public IItem CreateSample(int seed) => new AvailableBook { Id = $"thing-{seed}", DisplayName = $"Thing number {seed}", ShelfName = " Shelf no {seed}" };
}
