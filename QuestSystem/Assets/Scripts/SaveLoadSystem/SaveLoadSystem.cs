using System;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveLoadSystem
{
    public static T Load<T>() where T : ISaveable
    {
        string name = typeof(T).ToString();
        string savedName = Base64Encode(name);
        string folderPath = Application.streamingAssetsPath;
        string path = Path.Combine(folderPath, savedName);
        string text = File.ReadAllText(path);
        string loadedText = Base64Decode(text);

        return (T)JsonUtility.FromJson(loadedText, typeof(T));
    }

    public static void Save<T>(T instance) where T : ISaveable
    {
        string name = typeof(T).ToString();
        string text = JsonUtility.ToJson(instance);
        string textToSave = Base64Encode(text);
        string nameToSave = Base64Encode(name);
        string folderPath = Application.streamingAssetsPath;
        string path = Path.Combine(folderPath, nameToSave);

        File.WriteAllText(path, textToSave);
    }

    static string Base64Decode(string base64EncodedData)
    {
        return base64EncodedData;

        byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

    static string Base64Encode(string plainText)
    {
        return plainText;

        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }
}