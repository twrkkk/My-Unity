using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweaterShelf : Shelf
{
    public void PutSweater()
    {
        PutItem(new GameObject());
    }
    public void RemoveSweater()
    {
        RemoveItem();
    }
}
