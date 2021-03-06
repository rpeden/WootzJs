#region License

//-----------------------------------------------------------------------
// <copyright>
// The MIT License (MIT)
// 
// Copyright (c) 2014 Kirk S Woll
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------

#endregion

namespace System.Collections.Generic
{
    public class HashSet<T> : ISet<T>
    {
        private Dictionary<T, T> storage;

        public HashSet()
        {
            storage = new Dictionary<T, T>();
        }

        public HashSet(IEnumerable<T> source) : this()
        {
            foreach (var item in source)
                Add(item);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1"/> class that is empty and uses the specified equality comparer for the set type.
        /// </summary>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> implementation to use when comparing values in the set, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1"/> implementation for the set type.</param>
        public HashSet(IEqualityComparer<T> comparer)
        {
            if (comparer == null)
                comparer = EqualityComparer<T>.Default;
            storage = new Dictionary<T, T>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1"/> class that uses the specified equality comparer for the set type, contains elements copied from the specified collection, and has sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new set.</param><param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> implementation to use when comparing values in the set, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1"/> implementation for the set type.</param><exception cref="T:System.ArgumentNullException"><paramref name="collection"/> is null.</exception>
        public HashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer) : this(comparer)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            foreach (var item in collection)
                Add(item);
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return storage.Count; }
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public bool IsSynchronized
        {
            get { return true; }
        }

        public void CopyTo(Array array, int index)
        {
            var i = 0;
            foreach (var item in this)
            {
                array[i++] = item;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return storage.Keys.GetEnumerator();
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        void ICollection<T>.Add(T item)
        {
            storage[item] = item;
        }

        public void Clear()
        {
            storage.Clear();
        }

        public bool Contains(T item)
        {
            return storage.ContainsKey(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var i = 0;
            foreach (var item in this)
            {
                array[i++] = item;
            }
        }

        public bool Remove(T item)
        {
            return storage.Remove(item);
        }

        public bool Add(T item)
        {
            if (!storage.ContainsKey(item))
            {
                storage[item] = item;
                return true;
            }
            return false;
        }

        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }
    }
}