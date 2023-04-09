using System;
using UnityEngine;

namespace Nezumimeshi.Basis
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T s_instance;

        public static T Instance
        {
            get
            {
                if (s_instance == null)
                {
                    Type t = typeof(T);

                    s_instance = (T)FindObjectOfType(t);
                    if (s_instance == null)
                    {
                        Debug.LogError(t + " をアタッチしているGameObjectはありません");
                    }
                }

                return s_instance;
            }
        }

        protected virtual void Awake()
        {
            CheckInstance();
        }

        protected bool CheckInstance()
        {
            if (s_instance == null)
            {
                s_instance = this as T;
                return true;
            }
            else if (Instance == this)
            {
                return true;
            }

            Destroy(this);
            return false;
        }
    }
}