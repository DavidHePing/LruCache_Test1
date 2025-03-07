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
            SetNodeToFirst(_map[key]);

            return _map[key].Value;
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

            if (_first != null)
            {
                _first.Previous = null;
            }
        }
        
        if (node == _last)
        {
            SetLastNode(_last.Previous);
        }

        if (node.Previous != null)
        {
            node.Previous.Next = node.Next;
        }
    }
    
    public void Put(string key, string value)
    {
        if (_map.ContainsKey(key))
        {
            _map[key].Value = value;

            SetNodeToFirst(_map[key]);
            return;
        }

        if (_map.Count >= _length)
        {
            _map.Remove(_last.Key);
            SetLastNode(_last.Previous);
        }

        var newNode = new Node()
        {
            Key = key,
            Value = value
        };
        
        _map.Add(key, newNode);
        SetNodeToFirst(newNode);

        if (_last == null)
        {
            SetLastNode(newNode);
        }
    }

    private void SetNodeToFirst(Node node)
    {
        if (node.Previous != null)
        {
            node.Previous.Next = node.Next;
        }

        if (_first != null)
        {
            _first.Previous = node;
            node.Next = _first;
            
            if (node == _last )
            {
                SetLastNode(_last.Previous);
            }
        }

        node.Previous = null;
        _first = node;
    }

    private void SetLastNode(Node? node)
    {
        _last = node;

        if (_last != null)
        {
            _last.Next = null;
        }
    }
}