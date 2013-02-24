using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Enyim.Caching;
using Enyim.Caching.Memcached;
using ProtoBuf;
using System.IO;

namespace MemcachedTests
{
    [TestFixture]
    public class MemcachedTests
    {
        [Test]
        public void CanCacheString()
        {
            var client = new MemcachedClient();

            client.Store(StoreMode.Add, "a", "b");
            var result = client.Get<string>("a");

            Assert.AreEqual("b", result);
        }

        [Test]
        public void CanCacheList()
        {
            var client = new MemcachedClient();
            var list = new List<string> { "a", "b" };

            client.Store(StoreMode.Set, "d", list);

            var result = client.Get<List<string>>("d");
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("a", result[0]);
            Assert.AreEqual("b", result[1]);
        }

        [Test]
        public void CanCacheDTO()
        {
            var client = new MemcachedClient();
            var obj = new Dto
            {
                Name = "b",
                Title = "c"
            };

            client.Store(StoreMode.Set, "c", obj);
            var result = client.Get<Dto>("c");

            Assert.AreEqual("b", result.Name);
            Assert.AreEqual("c", result.Title);
        }

        
    }

    [ProtoContract]
    public class Dto
    {
        [ProtoMember(1)]
        public string Name;

        [ProtoMember(2)]
        public string Title;
    }
}
