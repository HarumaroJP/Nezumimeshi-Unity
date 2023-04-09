using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Nezumimeshi.Basis
{
    public class Message
    {
        Action action;


        public void AddListener(Action action)
        {
            this.action += action;
        }


        public void RemoveListener(Action action)
        {
            this.action -= action;
        }


        public void Send()
        {
            action?.Invoke();
        }
    }

    public class Message<T>
    {
        Action<T> action;


        public void AddListener(Action<T> action)
        {
            this.action += action;
        }


        public void RemoveListener(Action<T> action)
        {
            this.action -= action;
        }


        public void Send(T arg)
        {
            action?.Invoke(arg);
        }
    }

    public class AsyncMessage
    {
        List<Func<UniTask>> actions = new List<Func<UniTask>>();

        public void AddListener(Func<UniTask> action)
        {
            actions.Add(action);
        }


        public void RemoveListener(Func<UniTask> action)
        {
            actions.Remove(action);
        }

        public async UniTask Send()
        {
            foreach (Func<UniTask> task in actions)
            {
                await UniTask.Create(task);
            }
        }
    }
}