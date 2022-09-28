namespace LruCache_Test1;

public class LruCache
{
    private readonly int _length;
    private Node _first;
    private readonly Dictionary<string, Node> _map = new();

    public LruCache(int length)
    {
        _length = length;
    }

    public string? Get(string key)
    {
        if (!_map.ContainsKey(key))
        {
            return null;
        }
        else
        {
            var node = _map[key];

            if (node.Previous != null)
            {
                node.Previous.Next = node.Next;
                node.Next = _first;
                _first = node;
            }


            return node.Value;
        }
    }
}