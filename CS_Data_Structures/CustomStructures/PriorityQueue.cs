using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;

namespace CustomStructures
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> data;

        public PriorityQueue()
        {
            this.data = new List<T>();
        }

        public int Count()
        {
            return data.Count;
        }

        public void Enqueue(T itemToAdd)
        {
            data.Add(itemToAdd);
            int childIndex = data.Count - 1;
            while (childIndex > 0)
            {
                int parentIndex = (childIndex - 1) / 2;
                if (data[childIndex].CompareTo(data[parentIndex]) >= 0)
                    break;
                T tmp = data[childIndex]; data[childIndex] = data[parentIndex]; data[parentIndex] = tmp;
                childIndex = parentIndex;
            }
        }

        public T Dequeue()
        {
            // Assumes pq isn't empty
            int lastIndex = data.Count - 1;
            T frontItem = data[0];
            data[0] = data[lastIndex];
            data.RemoveAt(lastIndex);

            --lastIndex;
            int parentIndex = 0;
            while (true)
            {
                int firstChild = parentIndex * 2 + 1;
                if (firstChild > lastIndex) break;
                int secondChild = firstChild + 1;
                if (secondChild <= lastIndex && data[secondChild].CompareTo(data[firstChild]) < 0)
                    firstChild = secondChild;
                if (data[parentIndex].CompareTo(data[firstChild]) <= 0) break;
                T tmp = data[parentIndex]; data[parentIndex] = data[firstChild]; data[firstChild] = tmp;
                parentIndex = firstChild;
            }
            return frontItem;
        }

        public void Sort()
        {
            int lastIndex = Count();
            for (int i = lastIndex / 2 - 1; i >= 0; i--)
                heapify(data, lastIndex, i);
            for (int i = lastIndex - 1; i >= 0; i--)
            {
                T temp = data[0];
                data[0] = data[i];
                data[i] = temp;
                heapify(data, i, 0);
            }
        }
        void heapify(List<T> arr, int lastIndex, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < lastIndex && arr[left].CompareTo( arr[largest])==1)
            {
                largest = left;
            }

            if (right < lastIndex && arr[right].CompareTo(arr[largest]) == 1)
            {
                largest = left;
            }

            if (largest != i)
            {
                T swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;
                heapify(arr, lastIndex, largest);
            }
        }
    }
}