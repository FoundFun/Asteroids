using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace JsonService
{
    public static class JsonConverter<T>
    {
        public static void Export(TextAsset textAsset, T obj)
        {
#if UNITY_EDITOR
            string assetPath = AssetDatabase.GetAssetPath(textAsset);

            if (string.IsNullOrEmpty(assetPath))
            {
                Debug.LogError("Path = empty");
                return;
            }

            string json = ToJson(obj);
            File.WriteAllText(assetPath, json);
            AssetDatabase.Refresh();
#endif
        }

        public static T Import(TextAsset textAsset)
        {
            T obj = ToObject(textAsset.text);
            return obj;
        }

        private static string ToJson(T obj)
        {
            string json = JsonUtility.ToJson(obj);
            return json;
        }

        private static T ToObject(string json)
        {
            T obj = JsonUtility.FromJson<T>(json);
            return obj;
        }
    }
}
