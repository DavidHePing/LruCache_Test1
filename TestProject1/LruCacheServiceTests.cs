using FluentAssertions;
using LruCache_Test1;
using NUnit.Framework;

namespace TestProject1;

public class LruCacheServiceTests
{
    [Test]
    public void Get_Get_Exist_Value_With_Same_Key()
    {
        var lruCache = new LruCache(5);

        lruCache.Put("0", "0");
        lruCache.Put("0", "1");
        lruCache.Put("0", "2");
        lruCache.Put("0", "3");
        lruCache.Put("0", "4");
        lruCache.Put("0", "5");

        var result = lruCache.Get("0");
        result.Should().Be("5");
    }
    
    [Test]
    public void Get_Get_Exist_Value_With_Different_Key()
    {
        var lruCache = new LruCache(5);

        lruCache.Put("0", "0");
        lruCache.Put("1", "1");
        lruCache.Put("2", "2");
        lruCache.Put("3", "3");
        lruCache.Put("4", "4");

        var result = lruCache.Get("0");
        result.Should().Be("0");
    }
    
    [Test]
    public void Get_Remove_Oldest_Key()
    {
        var lruCache = new LruCache(5);

        lruCache.Put("0", "0");
        lruCache.Put("1", "1");
        lruCache.Put("2", "2");
        lruCache.Put("3", "3");
        lruCache.Put("4", "4");
        
        
        lruCache.Get("0");
        lruCache.Put("5", "5");
        
        var result = lruCache.Get("1");
        result.Should().Be(null);
        
        result = lruCache.Get("0");
        result.Should().Be("0");
    }

    [Test]
    public void Delete_Get_Null_If_Key_Is_Delete()
    {
        var lruCache = new LruCache(5);
        lruCache.Put("0", "0");
        lruCache.Delete("0");
        
        var result = lruCache.Get("0");
        result.Should().Be(null);
        
        lruCache.Put("0", "0");
        lruCache.Put("1", "1");
        lruCache.Delete("1");
        
        result = lruCache.Get("0");
        result.Should().Be("0");
        
        result = lruCache.Get("1");
        result.Should().Be(null);
    }

    [Test]
    public void All()
    {
        var lruCache = new LruCache(5);
        
        lruCache.Put("0", "0");
        lruCache.Put("1", "1");
        lruCache.Put("2", "2");
        lruCache.Put("3", "3");
        lruCache.Put("4", "4");
        
        lruCache.Delete("3");
        lruCache.Put("5", "5");
        
        lruCache.Get("0");
        lruCache.Get("1");
        lruCache.Put("6", "6");
        
        var result = lruCache.Get("2");
        result.Should().Be(null);
    }
}