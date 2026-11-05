using System.Collections;
using Capstone.Core.Models.Books;
using SharpQuest.Contracts;

namespace Capstone.Core.Models
{
    public class Library: IItemStore, IEnumerable<IItem>
    {
        private List<Book> _items = new List<Book>();
        private readonly Dictionary<string, Book> _byId = new (StringComparer.Ordinal);

        public event Action<IItem>? ItemAdded;

        public event Action<BorrowedBook, string>? BookOverdue;

        public int Count => _items.Count;

        public IItem this[int index]
        {
            get
            {
                ArgumentOutOfRangeException.ThrowIfNegative(index);
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, _items.Count);
                return _items[index];
            }
        }

        public IItem? this[string id] => FindById(id);

        public IReadOnlyList<IItem> All()
        {
            List<IItem> newList = new List<IItem>(this.Count);

            _items.ForEach((book) => newList.Add(book));
            return newList;
        }

        public IReadOnlyList<string> DescribeAll()
        {
            List<string> descriptions = new List<string>();
            foreach (Book book in _items)
                descriptions.Add(book.Describe());
            
            return descriptions;
        }

        public IItem? FindById(string id)
        {
            return _items.Find(x => x.Id == id);
        }

        public bool TryFindByName(string name, out IItem? result)
        {
            result = null;
            foreach (var item in _items)
            {
                if (item.DisplayName == name)
                {
                    result = item;
                    break;
                }
            }
            return result != null;
        }

        public bool Remove(string id)
        {
            if (id == null)
                 throw new ArgumentNullException("Provided id was null");
            return _items.RemoveAll(x => x.Id == id) > 0;        
        }

        public void Add(IItem item)
        {
            ArgumentNullException.ThrowIfNull(item, "Tried adding a null item to the collection");
            List <String> bookIds = _items.Select(x => x.Id).ToList();
            if (bookIds.Contains(item.Id))
                throw new ArgumentException("The item you are trying to add is already part of the collection");
            if (item is Book book)
            {
                _items.Add(book);
                ItemAdded?.Invoke(book);
            }
        }

        public int AddAll(params IItem[] items)
        {
            int addedItemsCount = 0;
            foreach (var item in items)
            {
                var currentCount = _items.Count;
                this.Add(item);
                if (currentCount < _items.Count)
                    addedItemsCount++;
            }
            return addedItemsCount;
        }

        public IEnumerator<IItem> GetEnumerator()
        {
            foreach(var item in _items)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public IEnumerable<IItem> Matching(string text)
        {
            ArgumentNullException.ThrowIfNull(text);

            foreach(var item in _items)
            {
                if (item.DisplayName.Contains(text, StringComparison.OrdinalIgnoreCase))
                    yield return item;
            }
        }

        public void ForEach(Action<IItem> action)
        {
            ArgumentNullException.ThrowIfNull(action);

            foreach(var item in _items)
                action(item);
        }

        public IEnumerable<IItem> Where(Func<IItem, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            foreach(var item in _items)
                if (predicate(item))
                    yield return item;
        }

        public IEnumerable<string> Map(Func<IItem, string> mapFunc)
        {
            ArgumentNullException.ThrowIfNull(mapFunc);

            foreach (var item in _items)
                yield return mapFunc(item);
        }

        public void CheckForOverdueBooks()
        {
            foreach(var item in _items)
            {
                if (item is BorrowedBook borrowedBook)
                    BookOverdue?.Invoke(borrowedBook, borrowedBook.BorrowerId);

            }
        }
    }
}
