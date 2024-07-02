using System;
using System.Collections.Generic;

public class PriorityQueue<T> where T : IComparable<T>
{
    #region Public Properties

    /// <summary>
    /// The internal list used by the queue
    /// </summary>
    public readonly List<T> items;

    public bool Empty => items.Count == 0;

    public T First
    {
        get
        {
            if (items.Count > 1)
                return items[0];
            return items[items.Count - 1];
        }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    public PriorityQueue() => items = new List<T>();

    #endregion

    #region Public Methods

    public bool Contains(T item) => items.Contains(item);

    public void Push(T item)
    {
        lock(this)
        {
            items.Add(item);
            SiftDown(0, items.Count - 1);
        }
    }

    public T Pop()
    {
        lock(this)
        {
            T item;
            var last = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            if (items.Count > 0)
            {
                item = items[0];
                items[0] = last;
                SiftUp(0);
            }
            else
                item = last;

            return item;
        }
    }

    #endregion

    #region Private Methods

    protected int Compare(T A, T B) => A.CompareTo(B);

    protected void SiftDown(int startpos, int pos)
    {
        var newitem = items[pos];
        while(pos > startpos)
        {
            var parentpos = (pos - 1) >> 1;
            var parent = items[parentpos];
            if (Compare(parent, newitem) <= 0)
                break;
            items[pos] = parent;
            pos = parentpos;
        }
        items[pos] = newitem;
    }

    protected void SiftUp(int pos)
    {
        var endpos = items.Count;
        var startpos = pos;
        var newitem = items[pos];
        var childpos = 2 * pos + 1;
        while (childpos < endpos)
        {
            var rightpos = childpos + 1;
            if (rightpos < endpos && Compare(items[rightpos], items[childpos]) <= 0)
                childpos = rightpos;
            items[pos] = items[childpos];
            pos = childpos;
            childpos = 2 * pos + 1;
        }
        items[pos] = newitem;
        SiftDown(startpos, pos);
    }

    #endregion
}
