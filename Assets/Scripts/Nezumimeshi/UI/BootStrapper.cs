using UnityEngine;
using UnityEngine.EventSystems;

namespace Nezumimeshi.UI
{
    public class BootStrapper : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            gameObject.SetActive(false);
        }
    }
}