using TMPro;
using UnityEngine;

namespace Nezumimeshi.UI
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI timerText;

        public void SetTime(int sec)
        {
            timerText.text = $"残り{sec:00}秒";
        }
    }
}