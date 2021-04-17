using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using Zenject.ReflectionBaking.Mono.Cecil.Mdb;

public class MaterialFixer : MonoBehaviour
{
    public Transform parent;

    public HashSet<string> materialsHashSet;
    public List<Material> materialList;

    public List<Material> newMaterials;
    public Material errorPink;

    [Button]
    public void FindAllMaterials()
    {
        MeshRenderer[] mr = parent.GetComponentsInChildren<MeshRenderer>();

        materialsHashSet = new HashSet<string>();
        materialList = new List<Material>();
        for (int i = 0; i < mr.Length; i++)
        {
            for (int j = 0; j < mr[i].materials.Length; j++)
            {

                if(materialsHashSet.Add(mr[i].materials[j].name))
                    materialList.Add(mr[i].materials[j]);
            }
        }

    }

    [Button]
    public void SetNewMaterials()
    {
        MeshRenderer[] mr = parent.GetComponentsInChildren<MeshRenderer>();

        materialsHashSet = new HashSet<string>();
        materialList = new List<Material>();
        for (int i = 0; i < mr.Length; i++)
        {
            for (int j = 0; j < mr[i].materials.Length; j++)
            {
                mr[i].materials[j] = GetMaterialByName(mr[i].materials[j].name);
            }
        }

    }

    private Material GetMaterialByName(string name)
    {
        for (int i = 0; i < newMaterials.Count; i++)
        {
            if (newMaterials[i].name == name)
                return newMaterials[i];
        }

        Debug.LogError($"No material: {name}");
        return errorPink;
    }
}
