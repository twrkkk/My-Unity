using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
    public List<T> prefab { get; }
    public bool autoExpand { get; set; }
    public Transform container { get; }
    public T lastElement { get; set; }

    public List<T> pool;

    public PoolMono(List<T> prefab, int count)
    {
        this.prefab = prefab;
        this.container = null;

        this.CreatePool(count);
    }

    public PoolMono(List<T> prefab, int count, Transform container)
    {
        this.prefab= prefab;
        this.container = container;
        this.CreatePool(count);
    }

    private void CreatePool(int count)
    {
        this.pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            this.CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        int index = Random.Range(0, prefab.Count);
        var createdObject = GameObject.Instantiate(prefab[index], container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElements(out T element)
    {
        bool result = false;
        element = null;

        //int index = Random.Range(0, pool.Count);

        //while (!result )
        //{
        //    if(!pool[index].gameObject.activeSelf)
        //    {
        //        element = pool[index];
        //        pool[index].gameObject.SetActive(true);
        //        result = true;
        //    }
        //    else
        //    {
        //        index = Random.Range(0, pool.Count);
        //    }

        //}

        foreach (var mono in pool)
        {
            if(!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(false);
                result = true;
            }
        }



        if(!result)
            element = null;

        return result;
    }

    public T GetFreeElement()
    {
        T result = null;
        if(HasFreeElements(out var element))    
            result = element;

        if (!result && autoExpand)
            result = CreateObject();

        if (result)
        {
            lastElement = result;
        }
        else
            throw new System.Exception($"There is no free elemnts {typeof(T)}");

        return result;
    }

    public void AddToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Add(obj);
    }

}
