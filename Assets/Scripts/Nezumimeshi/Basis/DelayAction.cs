using System;
using Cysharp.Threading.Tasks;

namespace Nezumimeshi.Basis
{
    public struct DelayAction
    {
        Action a;

        public void Invoke() => a();

        public void Invoke(float delay) => InvokeDelay_Internal(delay).Forget();

        async UniTaskVoid InvokeDelay_Internal(float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            a();
        }

        public void AddListener(Action action) => a += action;

        public void RemoveListener(Action action) => a -= action;
    }

    public struct DelayAction<T>
    {
        Action<T> a;

        public void Invoke(T p) => a(p);

        public void Invoke(T p, float delay) => InvokeDelay_Internal(p, delay).Forget();

        async UniTaskVoid InvokeDelay_Internal(T p, float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            a(p);
        }

        public void AddListener(Action<T> action) => a += action;

        public void RemoveListener(Action<T> action) => a -= action;
    }
}