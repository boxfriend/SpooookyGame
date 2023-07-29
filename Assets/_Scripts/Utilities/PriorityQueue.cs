using System;
using System.Collections.Generic;

namespace Boxfriend.Utilities
{
    public class PriorityQueue<T>
    {
        private readonly Dictionary<T, QueueNode> _contained = new();
        private QueueNode _head = null;

        public int Count => _contained.Count;

        public bool Dequeue(out T item)
        {
            item = default;
            if(_head == null)
                return false;
            
            var node = _head;
            node.Priority = node.BasePriority;
            UpdateNodePosition(node);
            item = node.Item;
            return true;
        }

        public bool TryEnqueue(T item, float basePriority)
        {
            if (_contained.ContainsKey(item))
                return false;

            var node = new QueueNode(item, basePriority, null, _head);
            if(_head  != null)
                _head.Previous = node;

            _head = node;
            _contained.Add(item, node);

            UpdateNodePosition(node);
            return true;
        }
        private void UpdateNodePosition(QueueNode node)
        {
            while (TryMoveLower(node));
            while (TryMoveHigher(node));
        }
        private bool TryMoveLower (QueueNode node)
        {
            var next = node.Next;
            if (next == null || next.Priority <= node.Priority)
                return false;

            if(node.Previous != null)
                node.Previous.Next = next;

            if (node == _head)
                _head = next;

            node.Next = next.Next;
            next.Next = node;
            next.Previous = node.Previous;
            node.Previous = next;

            return true;
        }

        private bool TryMoveHigher (QueueNode node)
        {
            var prev = node.Previous;
            if(prev == null || prev.Priority >= node.Priority)
                return false;

            if (prev == _head)
                _head = node;

            if(node.Next != null)
                node.Next.Previous = prev;

            node.Previous = prev.Previous;
            prev.Previous = node;
            prev.Next = node.Next;
            node.Next = prev;

            return true;
        }

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
