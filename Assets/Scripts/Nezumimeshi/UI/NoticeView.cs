using System;
using Cysharp.Threading.Tasks;
using Nezumimeshi.Core;
using UnityEngine;

namespace Nezumimeshi.UI
{
    public class NoticeView : MonoBehaviour
    {
        [SerializeField] GameObject finishText;
        [SerializeField] GameObject startText;
        [SerializeField] float textTime;

        [SerializeField] AudioClip beginSE;
        [SerializeField] AudioClip finishSE;

        public async UniTask ShowResultText()
        {
            finishText.SetActive(true);
            AudioSystem.Instance.PlaySe(finishSE);

            await UniTask.Delay(TimeSpan.FromSeconds(textTime));

            finishText.SetActive(false);
        }

        public async UniTask ShowStartText()
        {
            startText.SetActive(true);
            AudioSystem.Instance.PlaySe(beginSE);

            await UniTask.Delay(TimeSpan.FromSeconds(textTime));

            startText.SetActive(false);
        }
    }
}