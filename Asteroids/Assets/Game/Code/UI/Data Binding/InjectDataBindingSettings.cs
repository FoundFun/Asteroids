namespace DataBinding
{
    [System.Serializable]
    public struct InjectDataBindingSettings<T>
    {
        public InjectType InjectType;
        public T Instance;
        public string Id;
    }
}
