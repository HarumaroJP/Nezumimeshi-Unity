using UnityEngine;

namespace Nezumimeshi.Stage
{
    public class DestroyZone : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Omusubi"))
            {
                Omusubi.Omusubi omusubi = other.collider.GetComponent<Omusubi.Omusubi>();
                omusubi.FadeDestroy();
            }
        }
    }
}