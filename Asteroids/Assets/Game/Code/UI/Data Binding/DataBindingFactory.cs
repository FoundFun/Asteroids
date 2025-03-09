using TMPro;
using UniRx;

namespace DataBinding
{
    public sealed class DataBindingFactory
    {
        public TextBinder CreateTextBinder(ReactiveProperty<string> reactiveProperty, TextMeshProUGUI tmp)
        {
            TextBinder binder = new TextBinder(reactiveProperty, tmp);
            return binder;
        }
    }
}
