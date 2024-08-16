using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsUtility : MonoBehaviour
{
    public static void SaveDictionary(Dictionary<string, int> dictionary, string key)
    {
        // Серіалізація словника у формат JSON
        string json = JsonUtility.ToJson(new SerializableDictionary(dictionary));
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public static Dictionary<string, int> LoadDictionary(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            SerializableDictionary serializableDictionary = JsonUtility.FromJson<SerializableDictionary>(json);
            return serializableDictionary.ToDictionary();
        }
        return new Dictionary<string, int>();
    }
}
[System.Serializable]
public class SerializableDictionary
{
    public List<string> keys;
    public List<int> values;

    public SerializableDictionary(Dictionary<string, int> dictionary)
    {
        keys = new List<string>(dictionary.Keys);
        values = new List<int>(dictionary.Values);
    }

    public Dictionary<string, int> ToDictionary()
    {
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        for (int i = 0; i < keys.Count; i++)
        {
            dictionary.Add(keys[i], values[i]);
        }
        return dictionary;
    }
}
