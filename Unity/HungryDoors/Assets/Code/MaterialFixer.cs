using NaughtyAttributes;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MaterialFixer : MonoBehaviour
{
    public Transform parent;

    public HashSet<string> materialsHashSet;
    public List<Material> materialList;

    public List<Material> newMaterials;
    public Material errorPink;
    char[] delimiterChars = { ' ' };//, ',', '.', ':', '\t' };

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

                if (materialsHashSet.Add(mr[i].materials[j].name))
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
            Material[] m = new Material[mr[i].materials.Length];

            for (int j = 0; j < mr[i].materials.Length; j++)
            {
                m[j] = GetMaterialByName(mr[i].materials[j].name);
            }
            mr[i].materials = m;

            //if ( mr[i].materials.Length == 1)
            //{
            //    mr[i].material = GetMaterialByName(mr[i].material.name);
            //}
        }

    }

    private Material GetMaterialByName(string name)
    {
        string trueName = name.Split(delimiterChars)[0];

        for (int i = 0; i < newMaterials.Count; i++)
        {
            if (newMaterials[i].name == trueName)
            {
                //   return newMaterials[i];

                Material newMat = (Material)AssetDatabase.LoadAssetAtPath($"Assets/Materials/Enviro/{trueName}.mat", typeof(Material));
                return newMat;
            }
        }

        Debug.Log($"No material: {trueName}");
        return errorPink;
    }
}
