using UnityEngine;

namespace Nezumimeshi.Core {
    public interface IPoolable<out T> where T : MonoBehaviour {
        T Entity { get; }

        void OnReleased();
        void OnCatched();
    }
}