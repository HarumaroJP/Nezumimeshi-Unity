using Nezumimeshi.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nezumimeshi.UI
{
    public class PictureItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        RectTransform rectTransform;
        Vector2 defaultScale;
        [SerializeField] Vector3 scaleOnHover;
        [SerializeField] Vector3 scaleOnClick;
        [SerializeField] AudioClip pointerEnterClip;
        [SerializeField] AudioClip pointerClickClip;

        bool _isSelecting;

        public bool isSelecting
        {
            get => _isSelecting;
            set
            {
                _isSelecting = value;

                if (value)
                {
                    rectTransform.localScale = scaleOnClick;
                }
                else
                {
                    rectTransform.localScale = defaultScale;
                }
            }
        }

        public void OnStart()
        {
            rectTransform = GetComponent<RectTransform>();
            defaultScale = rectTransform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isSelecting) return;
            rectTransform.localScale = scaleOnHover;

            AudioSystem.Instance.PlaySe(pointerEnterClip);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isSelecting) return;
            rectTransform.localScale = defaultScale;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isSelecting) return;
            rectTransform.localScale = scaleOnClick;

            AudioSystem.Instance.PlaySe(pointerClickClip);
        }
    }
}