using System;
using System.Collections;
using System.Collections.Generic;

namespace LinqStudy {
    public class MyArray<T> : IEnumerable<T>, IList<T> {
        public const int MAX_COUNT = 100;
        private T[] array = new T[0];

        public MyArray() {
        }

        public MyArray(int count) {
            if (MAX_COUNT < count) {
                throw new ArgumentException(nameof(MAX_COUNT) + " < " + nameof(count));
            }
            if (count < 0) {
                throw new ArgumentNullException(nameof(count) + " < 0");
            }
            array = new T[count];
        }

        public T this[int index] {
            get {
                if (array == null) {
                    throw new InvalidOperationException(nameof(array) + " == null");
                }
                if (index < 0 || array.Length <= index) {
                    throw new ArgumentException(nameof(index) + " < 0 || " + nameof(array) + ".Length <= " + nameof(index));
                }
                return array[index];
            }

            set {
                if (array == null) {
                    throw new InvalidOperationException(nameof(array) + " == null");
                }
                if (index < 0 || array.Length <= index) {
                    throw new ArgumentNullException(nameof(index) + " < 0 || " + nameof(array) + ".Length <= " + nameof(index));
                }
                array[index] = value;
            }
        }

        public int Count {
            get {
                if (array == null) {
                    throw new InvalidOperationException(nameof(array) + " == null");
                }
                return array.Length;
            }
        }

        public bool IsReadOnly {
            get {
                return false;
            }
        }

        public void Add(T item) {
            if (array == null) {
                throw new InvalidOperationException(nameof(array) + " == null");
            }
            if (MAX_COUNT <= array.Length) {
                throw new ArgumentException(nameof(MAX_COUNT) + " <= " + nameof(array) + ".Length");
            }
            T[] temporary = new T[array.Length + 1];
            array.CopyTo(temporary, 0);
            temporary[temporary.Length - 1] = item;
            array = temporary;
        }

        public void Clear() {
            array = new T[0];
        }

        public bool Contains(T item) {
            if (array == null) {
                throw new InvalidOperationException(nameof(array) + " == null");
            }
            if (item == null) {
                throw new ArgumentNullException(nameof(item) + " == null");
            }
            return Array.IndexOf(array, item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex) {
            if (this.array == null) {
                throw new InvalidOperationException("this." + nameof(array) + " == null");
            }
            if (array == null) {
                throw new ArgumentNullException(nameof(array) + " == null");
            }
            if (arrayIndex < 0 || array.Length <= arrayIndex) {
                throw new ArgumentException(nameof(arrayIndex) + " < 0 || " + nameof(array) + ".Length <= " + nameof(arrayIndex));
            }
            this.array.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator() {
            foreach (var item in array) {
                yield return item;
            }
        }

        public int IndexOf(T item) {
            if (array == null) {
                throw new ArgumentNullException(nameof(array) + " == null");
            }
            return Array.IndexOf(array, item);
        }

        public void Insert(int index, T item) {
            if (array == null) {
                throw new InvalidOperationException(nameof(array) + " == null");
            }
            if (index < 0 || array.Length <= index) {
                throw new ArgumentException(nameof(index) + " < 0 || " + nameof(array) + ".Length <= " + nameof(index));
            }
            if (item == null) {
                throw new ArgumentNullException(nameof(item) + " == null");
            }
            if (MAX_COUNT <= array.Length + 1) {
                throw new ArgumentException(nameof(MAX_COUNT) + " <= " + nameof(array) + ".Length");
            }
            T[] temporary = new T[array.Length + 1];
            Array.Copy(array, 0, temporary, 0, index);
            Array.Copy(array, index, temporary, index + 1, array.Length - index);
            temporary[index] = item;
            array = temporary;
        }

        public bool Remove(T item) {
            if (array == null) {
                throw new InvalidOperationException(nameof(array) + " == null");
            }
            if (item == null) {
                throw new ArgumentNullException(nameof(item) + " == null");
            }
            if (array.Length == 0) {
                throw new ArgumentNullException(nameof(array) + ".Length == 0");
            }
            int index = Array.BinarySearch(array, item);
            if (index < 0) {
                return false;
            }

            RemoveAt(index);

            return true;
        }

        public void RemoveAt(int index) {
            if (array == null) {
                throw new InvalidOperationException(nameof(array) + " == null");
            }
            if (index < 0 || array.Length <= index) {
                throw new ArgumentException(nameof(index) + " < 0 || " + nameof(array) + ".Length <= " + nameof(index));
            }
            T[] temporary = new T[array.Length - 1];
            Array.Copy(array, 0, temporary, 0, index);
            Array.Copy(array, index + 1, temporary, index, array.Length - index - 1);
            array = temporary;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

    }
}

