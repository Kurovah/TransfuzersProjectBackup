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

public class HelperFunctions
{
    public static IEnumerator punchValue(float value, float newValue, int smoothing)
    {
        float t = 0;
        while (t > 1)
        {
            float y = 4 * (-(Mathf.Pow(t, 2)) + t);
            value = Mathf.Lerp(value, newValue, y);
            t += smoothing;
            yield return null;
        }
    }

    public static bool IsBetween(float a, float b, float t)
    {
        return t > a && t < b;
    }
}
