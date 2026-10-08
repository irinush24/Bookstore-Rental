using SharpQuest.Contracts;

namespace Capstone.Core.Models
{
    public class Book : IItem
    {
        public required string Id { get; set; }

        public string DisplayName { get; set; } = null!;

        public string Kind { get; set;  } = string.Empty;

        public double Price { get; set; }
    }
}
