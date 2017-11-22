using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Part
{
    public string name;
    public string displayName;
    public GameObject prefab;
    public Sprite icon;
    public float mass = 1;
}

public class PartManager : MonoBehaviour
{
    public List<Part> parts;

    static Dictionary<string, Part> partMap;

    void Awake()
    {
        partMap = new Dictionary<string, Part>();

        foreach (Part part in this.parts)
        {
            if (partMap.ContainsKey(part.name))
            {
                Debug.LogError("| PartManager | Part names must be unique. There are two or more parts with the name: " + part.name + " this is not allowed.");
            }

            partMap.Add(part.name, part);
        }
    }

    public static bool HasPart(string name)
    {
        return partMap.ContainsKey(name);
    }

    public static Part GetPart(string name)
    {
        if (partMap.ContainsKey(name))
        {
            return partMap[name];
        }
        
        throw new Exception("| PartManager::GetPart() | There is no part with name: " + name);
    }

    public static IEnumerable<Part> GetEnumerable()
    {
        return (IEnumerable<Part>)partMap.Values;
    }
}
