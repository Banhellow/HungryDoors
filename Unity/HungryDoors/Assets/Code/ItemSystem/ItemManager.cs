using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Zenject;
public class ItemManager : MonoBehaviour
{
    //Injections
    private ItemFactory _itemFactory;


    [Header("Elements that spawn randomly")]
    public List<Item> items;
    public List<ItemData> weaponPool;
    public List<ItemData> foodPool;
    public List<ItemData> propsPool;
    private List<int> indices;

    [Header("Elements that have special Conditions")]
    public Item specialItem;
    public float spawnChance;

    [Header("Enemies with items")]
    public List<Item> activeEnemyItems;

    [Inject]
    public void Init(ItemFactory factory)
    {
        _itemFactory = factory;
        Debug.Log("Factory injected");
    }

    void Start()
    {
        activeEnemyItems = new List<Item>();
        indices = GenerateIndiciesArray(items.Count);
        BindPool(weaponPool);
        BindPool(foodPool);
        BindPool(propsPool);
    }

    public Item InstantiateItem(Item item, Vector3 pos, Quaternion rot, Transform parent = null)
    {
        var newItem = _itemFactory.Create(item, pos, rot, parent);
        return newItem;
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

    public Item GetQuestItemWithProbability()
    {
        if (activeEnemyItems.Contains(specialItem))
            return null;
        else
        {
            return Random.Range(0f, 1f) < spawnChance ? specialItem : null;
        }
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

    [Button]
    public void AddRigidBodyToItems()
    {
        int newRbCounter = 0;
        int unbindCounter = 0;
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].gameObject.GetComponent<Rigidbody>() == null)
            {
                newRbCounter++;
                items[i].gameObject.AddComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                items[i].itemRB = items[i].GetComponent<Rigidbody>();
                unbindCounter++;
            }

        }
        Debug.Log("RigidBodies added: " + newRbCounter);
        Debug.Log("RigidBodies binded: " + unbindCounter);
    }
}
