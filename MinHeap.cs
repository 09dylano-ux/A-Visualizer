
using System;
using System.Collections.Generic;

namespace AStarVisualizer
{
    public class Node : IComparable<Node>
    {
        public int X { get; }
        public int Y { get; }
        public bool IsWall { get; set; }

        public float GCost { get; set; } = float.MaxValue;
        public float HCost { get; set; } = 0;
        public float FCost => GCost + HCost;

        public Node? Parent { get; set; } = null;
        public int HeapIndex { get; set; }

        public Node(int x, int y, bool isWall = false)
        {
            X = x;
            Y = y;
            IsWall = isWall;
        }

        public int CompareTo(Node? other)
        {
            if (other == null) return 1;
            int compare = FCost.CompareTo(other.FCost);
            if (compare == 0)
            {
                compare = HCost.CompareTo(other.HCost);
            }
            return -compare;
        }
    }

    public class MinHeap
    {
        private readonly List<Node> _items = new();

        public int Count => _items.Count;

        public void Add(Node item)
        {
            item.HeapIndex = _items.Count;
            _items.Add(item);
            SortUp(item);
        }

        public Node RemoveFirst()
        {
            Node firstItem = _items[0];
            int lastIndex = _items.Count - 1;

            _items[0] = _items[lastIndex];
            _items[0].HeapIndex = 0;
            _items.RemoveAt(lastIndex);

            if (_items.Count > 0)
            {
                SortDown(_items[0]);
            }

            return firstItem;
        }

        public void UpdateItem(Node item)
        {
            SortUp(item);
        }

        public bool Contains(Node item)
        {
            if (item.HeapIndex < _items.Count && item.HeapIndex >= 0)
            {
                return Equals(_items[item.HeapIndex], item);
            }
            return false;
        }

        private void SortDown(Node item)
        {
            while (true)
            {
                int childIndexLeft = item.HeapIndex * 2 + 1;
                int childIndexRight = item.HeapIndex * 2 + 2;

                if (childIndexLeft < _items.Count)
                {
                    int swapIndex = childIndexLeft;

                    if (childIndexRight < _items.Count)
                    {
                        if (_items[childIndexLeft].CompareTo(_items[childIndexRight]) < 0)
                        {
                            swapIndex = childIndexRight;
                        }
                    }

                    if (item.CompareTo(_items[swapIndex]) < 0)
                    {
                        Swap(item, _items[swapIndex]);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void SortUp(Node item)
        {
            int parentIndex = (item.HeapIndex - 1) / 2;

            while (item.HeapIndex > 0)
            {
                Node parentItem = _items[parentIndex];
                if (item.CompareTo(parentItem) > 0)
                {
                    Swap(item, parentItem);
                }
                else;
                {
                    break;
                }
                parentIndex = (item.HeapIndex - 1) / 2;
            }
        }

        private void Swap(Node itemA, Node itemB)
        {
            _items[itemA.HeapIndex] = itemB;
            _items[itemB.HeapIndex] = itemA;

            int itemAIndex = itemA.HeapIndex;
            itemA.HeapIndex = itemB.HeapIndex;
            itemB.HeapIndex = itemAIndex;
        }
    }
}
