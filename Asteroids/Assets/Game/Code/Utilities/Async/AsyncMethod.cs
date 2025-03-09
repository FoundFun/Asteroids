using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Utilities
{
    public sealed class AsyncMethod : IDisposable
    {
        private bool _isStop;
        private bool _isStopDelay;
        private Func<bool> _waitUntil;
        private float _delay;
        private float _time;
        private AsyncMethodType _asyncMethodType;
        private CancellationTokenSource _cancellationTokenSource;

        private event Action onStart;
        private event Action onUpdate;
        private event Action onEnd;

        /// <summary>
        /// Delay
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="type"></param>
        /// <param name="OnEnter"></param>
        /// <param name="OnUpdate"></param>
        /// <param name="OnExit"></param>
        public AsyncMethod(float delay, bool isStopDelay, Action OnEnter, Action OnUpdate, Action OnExit)
        {
            _isStop = false;
            _delay = delay;
            _isStopDelay = isStopDelay;
            onStart = OnEnter;
            onUpdate = OnUpdate;
            onEnd = OnExit;
            _asyncMethodType = AsyncMethodType.Delay;
        }

        /// <summary>
        /// Delay with time
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="time"></param>
        /// <param name="OnStart"></param>
        /// <param name="OnUpdate"></param>
        /// <param name="OnEnd"></param>
        public AsyncMethod(float delay, float time, Action OnStart, Action OnUpdate, Action OnEnd)
        {
            _isStop = false;
            _delay = delay;
            _time = time;
            onStart = OnStart;
            onUpdate = OnUpdate;
            onEnd = OnEnd;
            _asyncMethodType = AsyncMethodType.DelayWitchTime;
        }

        /// <summary>
        /// Yield
        /// </summary>
        /// <param name="OnEnter"></param>
        /// <param name="OnUpdate"></param>
        /// <param name="OnExit"></param>
        public AsyncMethod(Action OnEnter, Action OnUpdate, Action OnExit)
        {
            _isStop = false;
            onStart = OnEnter;
            onUpdate = OnUpdate;
            onEnd = OnExit;
            _asyncMethodType = AsyncMethodType.Yield;
        }

        /// <summary>
        /// Wait until
        /// </summary>
        /// <param name="waitUntil"></param>
        /// <param name="OnEnter"></param>
        /// <param name="OnUpdate"></param>
        /// <param name="OnExit"></param>
        public AsyncMethod(Func<bool> waitUntil, Action OnEnter, Action OnUpdate, Action OnExit)
        {
            _isStop = false;
            _waitUntil = waitUntil;
            onStart = OnEnter;
            onUpdate = OnUpdate;
            onEnd = OnExit;
            _asyncMethodType = AsyncMethodType.WaitUntil;
        }

        /// <summary>
        /// Wait until witch time
        /// </summary>
        /// <param name="time"></param>
        /// <param name="waitUntil"></param>
        /// <param name="OnEnter"></param>
        /// <param name="OnUpdate"></param>
        /// <param name="OnExit"></param>
        public AsyncMethod(float time, Func<bool> waitUntil, Action OnEnter, Action OnUpdate, Action OnExit)
        {
            _isStop = false;
            _time = time;
            _waitUntil = waitUntil;
            onStart = OnEnter;
            onUpdate = OnUpdate;
            onEnd = OnExit;
            _asyncMethodType = AsyncMethodType.WaitUntilWitchTime;
        }

        ~AsyncMethod()
        {
            Dispose();
        }

        public void Dispose()
        {
            Stop();
        }

        public async void Run(float delay = 0f)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                if (delay > 0f)
                {
                    await WaitTime(delay, _cancellationTokenSource.Token);
                }

                switch (_asyncMethodType)
                {
                    case AsyncMethodType.Delay:
                        await Delay(_cancellationTokenSource.Token);
                        break;
                    case AsyncMethodType.DelayWitchTime:
                        await DelayWitchTime(_cancellationTokenSource.Token);
                        break;
                    case AsyncMethodType.Yield:
                        await Yield(_cancellationTokenSource.Token);
                        break;
                    case AsyncMethodType.WaitUntil:
                        await WaitUntil(_cancellationTokenSource.Token);
                        break;
                    case AsyncMethodType.WaitUntilWitchTime:
                        await WaitUntilWitchTime(_cancellationTokenSource.Token);
                        break;
                    default:
                        break;
                }
            }
            catch (OperationCanceledException)
            {
                
            }
            finally
            {
                
            }
        }

        public void Stop()
        {
            if (_isStop) return;

            if (_cancellationTokenSource != null)
            {
                _isStop = true;
                onUpdate = null;
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
        }

        private async UniTask Delay(CancellationToken cancellationToken)
        {
            onStart?.Invoke();

            while (cancellationToken.IsCancellationRequested == false)
            {
                cancellationToken.ThrowIfCancellationRequested();
                onUpdate?.Invoke();

                await UniTask.Delay(Convert.ToInt32(_delay * 1000), false, PlayerLoopTiming.Update, cancellationToken);

                onEnd?.Invoke();

                if (_isStopDelay)
                    Stop();
            }
        }

        private async UniTask DelayWitchTime(CancellationToken cancellationToken)
        {
            onStart?.Invoke();
            bool isTimeEnd = false;
            UniTask uniTask = WaitTime(cancellationToken).ContinueWith(() => isTimeEnd = true);

            while (cancellationToken.IsCancellationRequested == false && isTimeEnd == false)
            {
                cancellationToken.ThrowIfCancellationRequested();
                onUpdate?.Invoke();

                await UniTask.Delay(Convert.ToInt32(_delay * 1000), false, PlayerLoopTiming.Update, cancellationToken);

                onEnd?.Invoke();
            }

            Stop();
        }

        private async UniTask WaitUntil(CancellationToken cancellationToken)
        {
            onStart?.Invoke();
            bool isCompleted = false;

            while (cancellationToken.IsCancellationRequested == false && isCompleted == false)
            {
                cancellationToken.ThrowIfCancellationRequested();
                onUpdate?.Invoke();

                if (_waitUntil.Invoke())
                    isCompleted = true;

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }

            onEnd?.Invoke();
            Stop();
        }

        private async UniTask WaitUntilWitchTime(CancellationToken cancellationToken)
        {
            onStart?.Invoke();
            bool isCompleted = false;
            bool isTimeEnd = false;
            UniTask uniTask = WaitTime(cancellationToken).ContinueWith(() => isTimeEnd = true);

            while (cancellationToken.IsCancellationRequested == false && isCompleted == false && isTimeEnd == false)
            {
                cancellationToken.ThrowIfCancellationRequested();
                onUpdate?.Invoke();

                if (_waitUntil.Invoke())
                    isCompleted = true;

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }

            onEnd?.Invoke();
            Stop();
        }

        private async UniTask Yield(CancellationToken cancellationToken)
        {
            onStart?.Invoke();

            while (cancellationToken.IsCancellationRequested == false)
            {
                cancellationToken.ThrowIfCancellationRequested();
                onUpdate?.Invoke();

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }

            onEnd?.Invoke();
            Stop();
        }

        private async UniTask WaitTime(CancellationToken cancellationToken)
        {
            float timer = 0f;

            while (cancellationToken.IsCancellationRequested == false && timer < _time)
            {
                cancellationToken.ThrowIfCancellationRequested();
                timer += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }
        }

        private async UniTask WaitTime(float time, CancellationToken cancellationToken)
        {
            float timer = 0f;

            while (cancellationToken.IsCancellationRequested == false && timer < time)
            {
                cancellationToken.ThrowIfCancellationRequested();
                timer += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }
        }
    }
}
