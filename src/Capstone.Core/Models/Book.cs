using SharpQuest.Contracts;
using Capstone.Core.Models.Enums;

namespace Capstone.Core.Models
{
    public class Book : IItem
    {
        public required string Id { get; init; }

        public string DisplayName { get; init; } = null!;

        public BookType Category { get; set; }

        public double Price { get; set; }
    }
}
