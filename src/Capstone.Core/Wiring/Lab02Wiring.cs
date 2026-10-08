using Capstone.Core.Models;
using SharpQuest.Contracts;

namespace Capstone.Core.Wiring
{
    public sealed class Lab02Wiring : ILab02Wiring
    {
        public IItemStore CreateStore() => new Shelf();
        public IItemMapper CreateMapper() => new BookDTO();
        public IItem CreateSample(int seed) => new Book { Id = $"thing-{seed}", DisplayName = $"Thing number {seed}" };
    }
}
