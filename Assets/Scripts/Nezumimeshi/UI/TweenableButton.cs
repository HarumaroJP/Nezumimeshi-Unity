using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Nezumimeshi.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Nezumimeshi.Basis
{
    [RequireComponent(typeof(EventTrigger))]
    public class TweenableButton : MonoBehaviour
    {
        [SerializeField] public float tweenDuration;

        [Space] [SerializeField] public Vector3 pointerMoveOffset;
        [SerializeField] public Vector3 pointerDownScaleOffset;
        [SerializeField] public Vector3 clickedMoveOffset;

        [SerializeField] bool ignoreTimeScale;

        [Space] public AudioClip onClickSe;

        [Space] public UnityEvent onClickByTween;

        RectTransform m_rectTransform;
        Button button;
        Vector3 m_positionCache;
        Vector3 m_scaleCache;

        bool m_isSceneUnload;
        bool isClicked;


        void Awake()
        {
            SceneManager.sceneUnloaded += _ => m_isSceneUnload = false;

            //Cache initial coordinates.
            m_rectTransform = GetComponent<RectTransform>();

            m_positionCache = m_rectTransform.anchoredPosition;
            m_scaleCache = m_rectTransform.localScale;

            //Register Events.
            button = GetComponent<Button>();
            button.onClick.AddListener(() => AudioSystem.Instance.PlaySe(onClickSe));

            EventTrigger trigger = GetComponent<EventTrigger>();


            EventTrigger.Entry entryPointerEnter;

            if (trigger.triggers.Any(t => t.eventID == EventTriggerType.PointerEnter))
            {
                //If event trigger already has pointer down event.
                entryPointerEnter = trigger.triggers.Find(e => e.eventID == EventTriggerType.PointerEnter);
            }
            else
            {
                entryPointerEnter = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerEnter,
                };
                trigger.triggers.Add(entryPointerEnter);
            }

            entryPointerEnter.callback.AddListener(_ =>
            {
                if (!button.interactable) return;
                OnPointerEnter();
            });

            EventTrigger.Entry entryPointerUp;

            if (trigger.triggers.Any(t => t.eventID == EventTriggerType.PointerExit))
            {
                //If event trigger already has pointer up event.
                entryPointerUp = trigger.triggers.Find(e => e.eventID == EventTriggerType.PointerExit);
            }
            else
            {
                entryPointerUp = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerExit,
                };
                trigger.triggers.Add(entryPointerUp);
            }


            entryPointerUp.callback.AddListener(_ =>
            {
                if (!button.interactable) return;
                OnPointerExit();
            });


            EventTrigger.Entry entryPointerDown;

            if (trigger.triggers.Any(t => t.eventID == EventTriggerType.PointerDown))
            {
                //If event trigger already has pointer up event.
                entryPointerDown = trigger.triggers.Find(e => e.eventID == EventTriggerType.PointerDown);
            }
            else
            {
                entryPointerDown = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerDown,
                };
                trigger.triggers.Add(entryPointerDown);
            }

            entryPointerDown.callback.AddListener(_ =>
            {
                if (!button.interactable) return;
                OnClickAsync().Forget();
            });
        }


        void OnPointerEnter()
        {
            if (isClicked) return;

            m_tweenPos = m_rectTransform.DOAnchorPos(m_positionCache + pointerMoveOffset, tweenDuration)
                .SetLink(gameObject)
                .SetEase(Ease.OutBack);
            m_tweenScale = m_rectTransform.DOScale(m_scaleCache + pointerDownScaleOffset, tweenDuration)
                .SetLink(gameObject)
                .SetEase(Ease.OutBack);

            if (ignoreTimeScale)
            {
                m_tweenPos.SetUpdate(true);
                m_tweenScale.SetUpdate(true);
            }

            m_tweenPos.Play();
            m_tweenScale.Play();
        }


        Tween m_tweenPos;
        Tween m_tweenScale;


        void OnPointerExit()
        {
            if (isClicked) return;
            if (m_isSceneUnload) return;

            m_tweenPos = m_rectTransform.DOAnchorPos(m_positionCache, tweenDuration).SetLink(gameObject).SetEase(Ease.OutBack);
            m_tweenScale = m_rectTransform.DOScale(m_scaleCache, tweenDuration).SetLink(gameObject).SetEase(Ease.OutBack);

            if (ignoreTimeScale)
            {
                m_tweenPos.SetUpdate(true);
                m_tweenScale.SetUpdate(true);
            }

            m_tweenPos.Play();
            m_tweenScale.Play();
        }

        public void ForceReset()
        {
            m_rectTransform.anchoredPosition = m_positionCache;
            m_rectTransform.localScale = m_scaleCache;
        }


        async UniTaskVoid OnClickAsync()
        {
            if (isClicked) return;

            isClicked = true;
            await m_rectTransform.DOAnchorPos(m_positionCache + clickedMoveOffset, 0.4f).SetLink(gameObject).AsyncWaitForCompletion();
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

            m_tweenPos?.Kill();
            m_tweenScale?.Kill();

            onClickByTween?.Invoke();
            isClicked = false;
        }
    }
}