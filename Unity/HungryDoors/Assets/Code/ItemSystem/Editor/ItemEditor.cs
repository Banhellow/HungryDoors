using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
public class ItemEditor : EditorWindow
{
    GUILayoutOption options;
    [MenuItem("Window/ItemWindow")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ItemEditor));
    }

    public void OnGUI()
    {
        GUILayout.Space(20f);
        GUILayout.Label("Choose item type",EditorStyles.centeredGreyMiniLabel);
        if(GUILayout.Button("AddFoodItem"))
        {
            MarkAsItem(ItemType.Food);
        }
        GUILayout.Space(2f);
        if (GUILayout.Button("AddSimpleItem"))
        {
            MarkAsItem(ItemType.Shoot);
        }
        GUILayout.Space(10f);
        GUILayout.Label("Objects selected: " + Selection.objects.Length);
    }
    public void MarkAsItem(ItemType type)
    {
        var selected = Selection.objects;
        foreach(Object selectedObj in selected)
        {
            var selectedGO = selectedObj as GameObject;
            SetItemType(selectedGO, type);
        }
    }


    private void SetItemType(GameObject go, ItemType type)
    {
        switch(type)
        {
            case ItemType.Food:
                go.AddComponent<Food>();
                break;
            default:
                go.AddComponent<Item>();
                break;
        }
    }
    
}
#endif