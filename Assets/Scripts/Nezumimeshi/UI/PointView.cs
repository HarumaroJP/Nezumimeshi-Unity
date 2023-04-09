using TMPro;
using UnityEngine;

namespace Nezumimeshi.UI
{
    public class PointView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI pointText;

        public void SetPoint(int point)
        {
            pointText.text = point.ToString("0000");
        }
    }
}