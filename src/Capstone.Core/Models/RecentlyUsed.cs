namespace Capstone.Core.Models;

public class RecentlyUsed<T>
{
    private readonly List<T> _items = new List<T>();
    private readonly int _capacity;

    public RecentlyUsed(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1);
        _capacity = capacity;
    }

    public IReadOnlyList<T> Items => _items.ToArray();

    public void Push(T item)
    {
        var existing = _items.FindIndex(i => EqualityComparer<T>.Default.Equals(i, item));
        if(existing >= 0)
            _items.RemoveAt(existing);
        else if (_items.Count == _capacity)
            _items.RemoveAt(_items.Count - 1);

        _items.Insert(0, item);
    }
}
