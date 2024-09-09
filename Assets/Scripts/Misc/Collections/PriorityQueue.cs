using System;
using System.Collections.Generic;

namespace Assets.Scripts.Misc.Collections
{
    public class PriorityQueue<T>
    {
        private List<(T Value, int Priority)> elements = new List<(T, int)>();

        public void Enqueue(T value, int priority)
        {
            elements.Add((value, priority));
            int currentIndex = elements.Count - 1;

            // Восстановление порядка кучи
            while (currentIndex > 0)
            {
                int parentIndex = (currentIndex - 1) / 2;
                if (elements[currentIndex].Priority >= elements[parentIndex].Priority)
                    break;

                // Меняем местами текущий элемент с родительским
                (elements[currentIndex], elements[parentIndex]) = (elements[parentIndex], elements[currentIndex]);
                currentIndex = parentIndex;
            }
        }

        public T Dequeue()
        {
            if (elements.Count == 0)
                throw new InvalidOperationException("Очередь пуста.");

            // Сохраняем корень кучи
            var root = elements[0];

            // Перемещаем последний элемент на место корня
            elements[0] = elements[elements.Count - 1];
            elements.RemoveAt(elements.Count - 1);

            // Восстановление порядка кучи
            Heapify(0);

            return root.Value;
        }

        public bool IsEmpty()
        {
            return elements.Count == 0;
        }

        private void Heapify(int index)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int smallestIndex = index;

            if (leftChildIndex < elements.Count && elements[leftChildIndex].Priority < elements[smallestIndex].Priority)
            {
                smallestIndex = leftChildIndex;
            }

            if (rightChildIndex < elements.Count && elements[rightChildIndex].Priority < elements[smallestIndex].Priority)
            {
                smallestIndex = rightChildIndex;
            }

            if (smallestIndex != index)
            {
                // Меняем местами текущий элемент с наименьшим дочерним
                (elements[index], elements[smallestIndex]) = (elements[smallestIndex], elements[index]);
                Heapify(smallestIndex);
            }
        }
    }
}
