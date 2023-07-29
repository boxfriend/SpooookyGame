using System;
using System.Collections.Generic;

namespace Boxfriend.Utilities
{
    public class PriorityQueue<T>
    {
        private readonly Dictionary<T, QueueNode> _contained = new();
        private QueueNode _head = null;

        public int Count => _contained.Count;
        private class QueueNode
        {
            public T Item { get; }
            public float BasePriority { get; }
            public float Priority { get; set; }

            public QueueNode Previous { get; set; }
            public QueueNode Next { get; set; }

            public QueueNode(T item, float basePriority)
            {
                Item = item;
                Priority = basePriority;
                BasePriority = basePriority;
            }

            public QueueNode (T item, float basePriority, QueueNode previous, QueueNode next) : this(item, basePriority)
            {
                Previous = previous;
                Next = next;
            }
        }
    }
}
