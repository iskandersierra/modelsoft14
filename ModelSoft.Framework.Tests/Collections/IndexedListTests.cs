using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelSoft.Framework.Collections;

namespace ModelSoft.Framework.Tests.Collections
{
    [TestClass]
    public class IndexedListTests
    {
        [TestMethod]
        public void IndexedList_ConstructorEmpty()
        {
            var list = new SampleIndexedList<int, string>(str => str.Length);

            Assert.AreEqual(0, list.Count);
            Assert.IsTrue(new string[0].SameSequenceAs(list));
            Assert.AreEqual(0, list.Operations.Count);
        }

        [TestMethod]
        public void IndexedList_AddOne()
        {
            var list = new SampleIndexedList<int, string>(str => str.Length);
            var values = new[] { "Hello" };

            foreach (var value in values) list.Add(value); 

            Assert.AreEqual(1, list.Count, "list.Count");
            Assert.IsTrue(values.SameSequenceAs(list), "values.SameSequenceAs(list)");
            Assert.AreEqual(2, list.Operations.Count, "list.Operations.Count");
            Assert.AreEqual(new IndexedListOperation<int, string>("CanEnter", 5, "Hello", true), list.Operations[0], "list.Operations[0]");
            Assert.AreEqual(new IndexedListOperation<int, string>("OnEnter", 5, "Hello"), list.Operations[1], "list.Operations[1]");
        }

        [TestMethod]
        public void IndexedList_AddTwo()
        {
            var list = new SampleIndexedList<int, string>(str => str.Length);
            var values = new[] { "Hello", "World!" };

            foreach (var value in values) list.Add(value); 

            Assert.AreEqual(2, list.Count, "list.Count");
            Assert.IsTrue(values.SameSequenceAs(list), "values.SameSequenceAs(list)");
            Assert.AreEqual(4, list.Operations.Count, "list.Operations.Count");
            Assert.AreEqual(new IndexedListOperation<int, string>("CanEnter", 5, "Hello", true), list.Operations[0], "list.Operations[0]");
            Assert.AreEqual(new IndexedListOperation<int, string>("OnEnter", 5, "Hello"), list.Operations[1], "list.Operations[1]");
            Assert.AreEqual(new IndexedListOperation<int, string>("CanEnter", 6, "World!", true), list.Operations[2], "list.Operations[2]");
            Assert.AreEqual(new IndexedListOperation<int, string>("OnEnter", 6, "World!"), list.Operations[3], "list.Operations[3]");
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void IndexedList_AddDuplicatedKey()
        {
            var list = new SampleIndexedList<int, string>(str => str.Length);
            var values = new[] { "Hello", "World" };

            //foreach (var value in values) list.Add(value); 
        }

        


        public class SampleIndexedList<TKey, TValue> : 
            IndexedList<TKey, TValue>
        {
            public SampleIndexedList(Func<TValue, TKey> keySelector, IComparer<TKey> keyComparer = null) 
                : base(keySelector, keyComparer)
            {
                Operations = new List<IndexedListOperation<TKey, TValue>>();
            }

            public List<IndexedListOperation<TKey, TValue>> Operations { get; set; }
            public Func<TKey, TValue, bool> CanEnterFunc { get; set; }
            public Func<TKey, TValue, bool> CanLeaveFunc { get; set; }

            protected override bool CanEnter(TKey key, TValue value)
            {
                var result = base.CanEnter(key, value) && (CanEnterFunc == null || CanEnterFunc(key, value));
                Operations.Add(new IndexedListOperation<TKey, TValue>("CanEnter", key, value, result));
                return result;
            }

            protected override void OnEnter(TKey key, TValue value)
            {
                base.OnEnter(key, value);
                Operations.Add(new IndexedListOperation<TKey, TValue>("OnEnter", key, value));
            }

            protected override bool CanLeave(TKey key, TValue value)
            {
                var result = base.CanLeave(key, value) && (CanLeaveFunc == null || CanLeaveFunc(key, value));
                Operations.Add(new IndexedListOperation<TKey, TValue>("CanLeave", key, value, result));
                return result;
            }

            protected override void OnLeave(TKey key, TValue value)
            {
                base.OnLeave(key, value);
                Operations.Add(new IndexedListOperation<TKey, TValue>("OnLeave", key, value));
            }
        }

        public class IndexedListOperation<TKey, TValue>
        {
            public IndexedListOperation(string type, bool result)
            {
                Type = type;
                Result = result;
            }

            public IndexedListOperation(string type)
            {
                Type = type;
            }

            public IndexedListOperation(string type, TKey key, TValue value, bool result)
            {
                Type = type;
                Key = key;
                Value = value;
                Result = result;
            }

            public IndexedListOperation(string type, TKey key, TValue value)
            {
                Type = type;
                Key = key;
                Value = value;
            }

            public string Type { get; set; }
            
            public TKey Key { get; set; }

            public TValue Value { get; set; }

            public bool? Result { get; set; }

            protected bool Equals(IndexedListOperation<TKey, TValue> other)
            {
                return 
                    string.Equals(Type, other.Type) && 
                    EqualityComparer<TKey>.Default.Equals(Key, other.Key) && 
                    EqualityComparer<TValue>.Default.Equals(Value, other.Value) && 
                    Result.Equals(other.Result);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((IndexedListOperation<TKey, TValue>) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (Type != null ? Type.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ EqualityComparer<TKey>.Default.GetHashCode(Key);
                    hashCode = (hashCode*397) ^ EqualityComparer<TValue>.Default.GetHashCode(Value);
                    hashCode = (hashCode*397) ^ Result.GetHashCode();
                    return hashCode;
                }
            }

            public override string ToString()
            {
                return string.Format("Type: {0}, Key: {1}, Value: {2}, Result: {3}", Type, Key, Value, Result);
            }
        }
    }
}
