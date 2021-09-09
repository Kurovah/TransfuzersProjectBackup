using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
public static class GameSaveSystem
{

    public static void SaveData(GameSaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameSaveData GetSaveData()
    {
        string path = Application.persistentDataPath + "/save.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameSaveData data = formatter.Deserialize(stream) as GameSaveData;
            stream.Close();
            return data;

        }
        else
        {
            return null;
        }
    }

}


[System.Serializable]
public class GameSaveData
{

}

[System.Serializable]
public class Item
{
    public Sprite itemSprite;
    public string name;
}

[System.Serializable]
public class DataLog
{
    public string name;
    public enum logtype
    {
        enemy,
        area,
        item
    }

    public bool unlocked;
    public Sprite Image;
    public string logDescription;
}

[System.Serializable]
public class TurretData
{
    public List<RecipeComponent> components = new List<RecipeComponent>();
    public GameObject prefab;
    public Sprite turretIcon;
    public string turretName;
    public bool unlocked;
    public bool displayed;
}

[System.Serializable]
public class RecipeComponent
{
    public int itemIndex;
    public int itemAmount;
}

[System.Serializable]
public class ItemInventory
{
    public Dictionary<int, int> dictionary;
    public ItemInventory()
    {
        dictionary = new Dictionary<int, int>();
    }

    public ItemInventory(Dictionary<int,int> data)
    {
        dictionary = data;
    }
}

[System.Serializable]
public class TurretInventory
{
    public Dictionary<int, int> dictionary;
    public TurretInventory()
    {
        dictionary = new Dictionary<int, int>();
    }

    public TurretInventory(Dictionary<int, int> data)
    {
        dictionary = data;
    }
}

public class ColorPallette
{
    //this is here so that I don't have to keep memorising these
    public static Color[] colors = {
        new Color32(237, 255, 253,255),
        new Color32(138,139,141,255),
        new Color32(84,92,98,255),
        new Color32(71,78,85,255),
        new Color32(231, 121, 84,255),
        new Color32(43,197,152,255),
        };
}

public class HelperFunctions
{
    public static bool IsBetween(float a, float b, float t)
    {
        return t > a && t < b;
    }
}
