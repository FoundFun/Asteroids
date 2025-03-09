using UnityEngine;

namespace JsonService
{
    public class JsonObject<T> : ScriptableObject
    {
        [SerializeField] private TextAsset _json;
        [SerializeField] private T _data;

        public T Data => _data;

        [ContextMenu("Import")]
        public virtual void Import()
        {
            try
            {
                _data = JsonConverter<T>.Import(_json);
            }
            catch (System.Exception exception)
            {
                Debug.LogError("Import error: " + exception);
                throw;
            }
        }

        [ContextMenu("Export")]
        public virtual void Export()
        {
            try
            {
                JsonConverter<T>.Export(_json, _data);
            }
            catch (System.Exception exception)
            {
                Debug.LogError("Export error: " + exception);
                throw;
            }
        }
    }
}
