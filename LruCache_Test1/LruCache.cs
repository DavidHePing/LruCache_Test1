namespace LruCache_Test1;

public class LruCache
{
    private readonly int _length;
    private Node? _first;
    private Node? _last;
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

            SetNodeToFirst(node);

            return node.Value;
        }
    }

    public void Delete(string key)
    {
        if (!_map.ContainsKey(key))
        {
            return;
        }
        
        var node = _map[key];
        _map.Remove(key);

        if (node == _first)
        {
            _first = _first.Next;
        }

        node.Previous.Next = node.Next;
    }
    
    public void Put(string key, string value)
    {
        if (_map.ContainsKey(key))
        {
            var node = _map[key];
            node.Value = value;

            SetNodeToFirst(node);
            return;
        }

        if (_map.Count >= _length)
        {
            _map.Remove(_last.Key);
            _last.Previous.Next = null;
            _last = _last.Previous;
        }

        _map.Add(key, new Node()
        {
            Previous = _last,
            Key = key,
            Value = value
        });

        _last = _map[key];
    }

    private void SetNodeToFirst(Node node)
    {
        if (node != _first)
        {
            node.Previous.Next = node.Next;
            node.Next = _first;
            _first = node;
        }
    }
}