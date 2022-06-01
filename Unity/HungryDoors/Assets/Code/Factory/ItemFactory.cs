using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class ItemFactory : PlaceholderFactory<Item,Vector3,Quaternion,Transform,Item>
{
    DiContainer _container;

    public ItemFactory(DiContainer container)
    {
        _container = container;
    }
    public override Item Create(Item param1, Vector3 pos, Quaternion rot, Transform parent = null)
    {
        var Item = _container.InstantiatePrefab(param1);
        Item.transform.position = pos;
        Item.transform.rotation = rot;
        Item.transform.SetParent(parent);
        return Item.GetComponent<Item>();
    }
}
