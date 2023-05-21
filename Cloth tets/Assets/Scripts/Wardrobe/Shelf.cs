using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Shelf
{
    private List<GameObject> _items;

    public int CurrentIndex { get; set; }

    public virtual void PutItem(GameObject go)
    {
        _items.Add(go);
    }

    public virtual GameObject GetItem() => _items[CurrentIndex];

    public virtual bool RemoveItem()
    {
        if (CurrentIndex >= _items.Count) return false;
        _items.RemoveAt(CurrentIndex);
        return true;    
    }

    public virtual GameObject GetNext() => _items[++CurrentIndex % _items.Count];

    public virtual GameObject GetPrevious() => _items[--CurrentIndex % _items.Count];
}

