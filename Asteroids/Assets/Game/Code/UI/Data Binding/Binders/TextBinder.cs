using System;
using TMPro;
using UniRx;

namespace DataBinding
{
    public sealed class TextBinder : IBinder, IObserver<string>
    {
        private ReactiveProperty<string> _reactiveProperty = new ReactiveProperty<string>();
        private TextMeshProUGUI _tmp;
        private IDisposable _handle;

        public TextBinder(ReactiveProperty<string> reactiveProperty, TextMeshProUGUI tmp)
        {
            _reactiveProperty = reactiveProperty;
            _tmp = tmp;
            Bind();
        }

        public void Dispose()
        {
            Unbind();
        }

        public void Bind()
        {
            OnNext(_reactiveProperty.Value);
            _handle = _reactiveProperty.Subscribe(this);
        }

        public void Unbind()
        {
            _handle?.Dispose();
        }

        public void OnNext(string value)
        {
            _tmp.text = value;
        }

        public void OnError(Exception error)
        {

        }

        public void OnCompleted() 
        { 

        }
    }
}
