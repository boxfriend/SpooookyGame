using System;
using System.Collections.Generic;

namespace Boxfriend.Utilities
{
    /// <summary>
    /// Queue based on a Linked List where object with highest priority is first out rather than FIFO.<br/>
    /// A dictionary is used to ensure each object in the queue is unique and to provide faster access to nodes when modifying them.<br/>
    /// Each operation is at most O(N), but it would really only get to O(2N) without the dict. And that is basically the same runtime complexity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T>
    {
        private readonly Dictionary<T, QueueNode> _contained = new();
        private QueueNode _head = null;

        /// <summary>
        /// Number of objects contained in the queue
        /// </summary>
        public int Count => _contained.Count;

        /// <summary>
        /// Gets highest priority item in the queue then assigns its priority to 0 or its base priority.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="assignBase">Whether to assign base priority back to currentPriority or reset to 0</param>
        /// <returns><see langword="false"/> if there is nothing to dequeue</returns>
        public bool Dequeue(out T item, bool assignBasePriority = false)
        {
            item = default;
            if(_head == null)
                return false;
            
            var node = _head;

            if(assignBasePriority)
                node.Priority = node.BasePriority;
            else
                node.Priority = 0;

            UpdateNodePosition(node);
            item = node.Item;
            return true;
        }

        /// <summary>
        /// Attempts to add an object to the queue.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="basePriority">Initial priority, can be optionally reassigned to this when dequeued</param>
        /// <returns><see langword="true"/> if successful. <br/><see langword="false"/> if object exists in the queue already.</returns>
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

        /// <summary>
        /// Adds a priority modifier to the <paramref name="item"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="priorityModifier">Amount to add to priority</param>
        public void ModifyPriority(T item, float priorityModifier)
        {
            if (_contained.TryGetValue(item, out var node))
            {
                node.Priority += priorityModifier;
                UpdateNodePosition(node);
            }
        }

        private void UpdateNodePosition(QueueNode node)
        {
            //I don't particularly like the way this is handled. The empty while loops feel weird
            while (TryMoveLower(node));
            while (TryMoveHigher(node));
        }

        /// <summary>
        /// Assigns a new priority to the <paramref name="item"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="newPriority"></param>
        public void AssignNewPriority (T item, float newPriority)
        {
            if (_contained.TryGetValue(item, out var node))
            {
                node.Priority = newPriority;
                UpdateNodePosition(node);
            }
        }

        /// <summary>
        /// Removes a node from the Linked List and Dictionary
        /// </summary>
        /// <param name="item">The specific item to remove.</param>
        public void Remove (T item)
        {
            if(_head == null)
                return;

            if(_contained.TryGetValue(item, out var node))
            {
                Remove(node);
                _contained.Remove(item);
            }

        }

        private void Remove(QueueNode node)
        {
            var prev = node.Previous;
            var next = node.Next;

            if(prev != null)
                prev.Next = next;

            if(next != null)
                next.Previous = prev;
        }

        //Checks if node can move lower in priority, if it can it swaps places
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

        //Same as previous, just checks if it can go higher
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
