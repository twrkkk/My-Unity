using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    List<Shelf> Shelfs = new List<Shelf>();
    SweaterShelf s = new SweaterShelf();
    TshirtShelf tshirt = new TshirtShelf();
    public void Start()
    {
        s.CurrentIndex = 2;
        tshirt.CurrentIndex = 10;

        Shelfs.Add(s);
        Shelfs.Add(tshirt);

        Shelfs[0].RemoveItem();
        Shelfs[1].RemoveItem();
    }
}
