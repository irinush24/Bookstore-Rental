using SharpQuest.Contracts;

namespace Capstone.Core.Models
{
    public class Shelf: IItemStore
    {
        private List<Book> _items = new List<Book>();

        public int Count => _items.Count;

        public IReadOnlyList<IItem> All()
        {
            if (_items.Count == 0) throw new NullReferenceException("The shelf is empty");

            return _items;
        }

        public IItem? FindById(string id)
        {
            return _items.Find(x => x.Id == id);
        }

        public bool Remove(string id)
        {
            if (id == null)
                 throw new ArgumentNullException("Provided id was null");
            return _items.RemoveAll(x => x.Id == id) > 0;        
        }

        public void Add(IItem item)
        {
            if (item == null) throw new ArgumentNullException("Tried adding a null item to the collection");
            if (_items.Contains(item)) throw new ArgumentException("The item you are trying to add is already inside the collection");
            if(item is Book book)
                _items.Add(book);
        }
    }
}
