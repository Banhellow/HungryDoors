using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
public class GUIDReplacer : EditorWindow
{
    string oldGUIDpath;
    string scenePath;
    string newGUIDPath;
    [MenuItem("Window/GUIDEditor")]
    public static void StartWindow()
    {
        GetWindow(typeof(GUIDReplacer));
    }

    private void OnGUI()
    {
        GUILayout.Label("Scene path", EditorStyles.miniLabel);
        scenePath = GUILayout.TextArea(scenePath);
        GUILayout.Space(5f);
        GUILayout.Label("Old GUID file path", EditorStyles.miniLabel);
        oldGUIDpath = GUILayout.TextArea(oldGUIDpath);
        GUILayout.Space(5f);
        GUILayout.Label("New GUID file path", EditorStyles.miniLabel);
        newGUIDPath = GUILayout.TextArea(newGUIDPath);
        GUILayout.Space(5f);
        if (GUILayout.Button("GetAllGUID"))
        {
            SaveAllGUIDsToFile(oldGUIDpath);
        }
        if(GUILayout.Button("ReplaceGUID"))
        {
            ReplaceGUID(scenePath, oldGUIDpath, newGUIDPath);
        }
    }


    string[] GetAllGUIDInScene()
    {
        List<string> guids = new List<string>();
        List<string> posid = new List<string>();
        foreach (var obj in Selection.objects)
        {
            if (obj == null) continue;
            int id = obj.GetInstanceID();
            string guid;
            string path;
            path = AssetDatabase.GetAssetPath(id);
            guid = AssetDatabase.AssetPathToGUID(path);
            Debug.Log("Object: " + obj.name + ", GUID: " + guid);
            guids.Add(guid);
        }
        return guids.ToArray();

    }
    public void SaveAllGUIDsToFile(string name)
    {
        string[] guids = GetAllGUIDInScene();
        File.WriteAllLines(name, guids);
    }

    public void ReplaceGUID(string path,string oldGUIDPath, string newGUIDPath)
    {
        string[] scene = File.ReadAllLines(path);
        string[] oldGUID = File.ReadAllLines(oldGUIDPath);
        string[] newGUID = File.ReadAllLines(newGUIDPath);
        for(int i = 0; i < scene.Length; i++)
        {
            for(int j = 0; j < oldGUID.Length; j++)
            {
                Regex pattern = new Regex(oldGUID[j]);
                if(pattern.IsMatch(scene[i]))
                {
                    string res = pattern.Replace(scene[i], newGUID[j]);
                    scene[i] = res;
                }
            }

        }
        File.WriteAllLines(path, scene);
    }
}

#endif