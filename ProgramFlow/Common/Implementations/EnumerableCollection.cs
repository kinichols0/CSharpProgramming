using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.TypesClasses.Implementations
{
    public class EnumerableCollection<T> : IEnumerable
    {
        private T[] collection;

        public EnumerableCollection() : this(new T[0])
        {
        }

        public EnumerableCollection(T[] _collection)
        {
            if (_collection != null)
            {
                // copy the array
                collection = new T[_collection.Length];
                for (int i = 0; i < _collection.Length; i++)
                {
                    collection[i] = _collection[i];
                }
            }
            else
                collection = new T[0];
        }

        /// <summary>
        /// Return the IEnumerator for this collection
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return new CollectionEnumerator<T>(collection);
        }

        /// <summary>
        /// My Add implementation
        /// </summary>
        /// <param name="obj"></param>
        public void Add(T obj)
        {
            // initialize our new array with an additional index than our current
            T[] array = new T[collection.Length + 1];

            // copy current contents to new array
            for(int i = 0; i < collection.Length; i++)
            {
                array[i] = collection[i];
            }

            // place new obj at the end of the array
            array[array.Length - 1] = obj;

            // point our current collection to the new array
            collection = array;
        }

        /// <summary>
        /// My Foreach implementation
        /// </summary>
        /// <param name="function"></param>
        public void ForEach(Action<T> function)
        {
            for(int i = 0; i < collection.Length; i++)
            {
                function(collection[i]);
            }
        }

        /// <summary>
        /// My first or default implementation
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public T FirstOrDefault(Func<T, bool> function)
        {
            for(int i = 0; i < collection.Length; i++)
            {
                if (function(collection[i]))
                    return collection[i];
            }
            return default(T);
        }
    }

    public class CollectionEnumerator<T> : IEnumerator
    {
        public T[] generics;

        // enumerators start at the -1 position until MoveNext() is invoked.
        int position = -1;

        public CollectionEnumerator(T[] _generics)
        {
            generics = _generics;
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        /// <summary>
        /// T item we are currently visiting
        /// </summary>
        public T Current
        {
            get
            {
                try
                {
                    return generics[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Move to the next position
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            position++;
            return position < generics.Length;
        }

        /// <summary>
        /// Go back to starting position
        /// </summary>
        public void Reset()
        {
            position = -1;
        }
    }
}
