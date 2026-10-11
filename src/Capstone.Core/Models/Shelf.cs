using SharpQuest.Contracts;

namespace Capstone.Core.Models
{
    public class Shelf: IItemStore
    {
        private List<Book> _items = new List<Book>();

        public int Count => _items.Count;

        public IReadOnlyList<IItem> All()
        {
            List<IItem> newList = new List<IItem>(this.Count);

            _items.ForEach((book) => newList.Add(book));
            return newList;
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
            ArgumentNullException.ThrowIfNull(item, "Tried adding a null item to the collection");
            List <String> bookIds = _items.Select(x => x.Id).ToList();
            if (bookIds.Contains(item.Id))
                throw new ArgumentException("The item you are trying to add is already part of the collection");
            if(item is Book book)
                _items.Add(book);
        }
    }
}
