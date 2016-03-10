﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    public const int InitialCapacity = 16;
    public const float LoadFactor = 0.75f;

    private LinkedList<KeyValue<TKey, TValue>>[] slots;
    public int Count { get; private set; }

    public int Capacity
    {
        get { return this.slots.Length; }
    }

    public HashTable(int capacity = InitialCapacity)
    {
        this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        this.Count = 0;
    }

    public void Add(TKey key, TValue value)
    {
        GrowIfNeeded();
        int slotNumber = this.FindSlotNumber(key);

        if (this.slots[slotNumber] == null)
        {
            this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var element in this.slots[slotNumber])
        {
            if (element.Key.Equals(key))
            {
                throw new ArgumentException("Element with such key already exists!");
            }
        }

        var newElement = new KeyValue<TKey, TValue>(key, value);
        this.slots[slotNumber].AddLast(newElement);
        this.Count++;

    }

    private void GrowIfNeeded()
    {
        if ((float) (this.Count + 1) / this.Capacity > LoadFactor)
        {
            this.Grow();
        }
    }

    private void Grow()
    {
        var newHashTable = new HashTable<TKey, TValue>(2 * this.Capacity);
        foreach (var element in this)
        {
            newHashTable.Add(element.Key, element.Value);
        }
        this.slots = newHashTable.slots;
        this.Count = newHashTable.Count;
    }

    private int FindSlotNumber(TKey key)
    {
        var slotNumber = Math.Abs(key.GetHashCode())%this.slots.Length;
        return slotNumber;
    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        GrowIfNeeded();
        int slotNumber = this.FindSlotNumber(key);

        if (this.slots[slotNumber] == null)
        {
            this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
        }
        foreach (var element in this.slots[slotNumber])
        {
            if (element.Key.Equals(key))
            {
                element.Value = value;
                return false;
            }
        }
        var newElement = new KeyValue<TKey, TValue>(key, value);
        this.slots[slotNumber].AddLast(newElement);
        this.Count++;
        return true;
    }

    public TValue Get(TKey key)
    {
        var element = Find(key);
        if (element == null)
        {
            throw new KeyNotFoundException("not such element");
        }
        else
        {
            return element.Value;
        }
    }

    public TValue this[TKey key]
    {
        get
        {
            throw new NotImplementedException();
            // Note: throw an exception on missing key
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var element = Find(key);
        if (element != null)
        {
            value = element.Value;
            return true;
        }
        else
        {
            value = default(TValue);
            return false;
        }
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        int slotNumber = this.FindSlotNumber(key);
        var elements = this.slots[slotNumber];
        if (elements == null)
        {
            return null;
        }
        foreach (var element in elements)
        {
            if (element.Key.Equals(key))
            {
                return element;
            }
        }

        return null;
    }

    public bool ContainsKey(TKey key)
    {
        var element = Find(key);
        if (element != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Remove(TKey key)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TKey> Keys
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public IEnumerable<TValue> Values
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (var elements in this.slots)
        {
            if (elements != null)
            {
                foreach (var element in elements)
                {
                    yield return element;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
