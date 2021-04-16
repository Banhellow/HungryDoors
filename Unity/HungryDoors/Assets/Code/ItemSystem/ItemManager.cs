using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Item> items;
    public List<ItemData> weaponPool;
    public List<ItemData> foodPool;
    public List<ItemData> propsPool;
    private List<int> indices;

    void Start()
    {
        indices = GenerateIndiciesArray(items.Count);
        BindPool(weaponPool);
        BindPool(foodPool);
        BindPool(propsPool);
    }

    void Update()
    {
    }

    public void BindPool(List<ItemData> pool)
    {
        var poolIndices = GenerateIndiciesArray(pool.Count);
        while(poolIndices.Count > 0)
        {
            int index = indices.RandomElement();
            int poolIndex = poolIndices.RandomElement();
            var item = items[index];
            item.data.relatedItem = pool[poolIndex];
            poolIndices.Remove(poolIndex);
            indices.Remove(index);
        }
    }

    List<ItemData> ExtractDataFromItems(List<Item> array)
    {
        var data = new List<ItemData>();
        for(int i = 0; i < array.Count;i++)
        {
            data.Add(array[i].data);
        }
        return data;
    }

    private List<int> GenerateIndiciesArray(int range)
    {
        List<int> array = new List<int>();
        for(int i = 0; i < range; i++)
        {
            array.Add(i);
        }
        return array;
    }
}
