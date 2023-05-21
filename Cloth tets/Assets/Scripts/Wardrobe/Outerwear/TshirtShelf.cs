using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TshirtShelf : Shelf
{
    public void PutTshirt()
    {
        PutItem(new GameObject());
    }
    public void RemoveTshirt()
    {
        CurrentIndex = 10;
        //RemoveItem(CurrentIndex);
        Debug.Log(CurrentIndex);
    }
}
