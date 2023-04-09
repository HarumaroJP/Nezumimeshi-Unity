using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nezumimeshi.UI
{
    public class PictureItemView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] Image iconImage;
        [SerializeField] TextMeshProUGUI descriptionText;
        [SerializeField] TextMeshProUGUI pointText;

        public void SetOmusubi(string name, Sprite icon, string description, int point)
        {
            nameText.text = name;
            iconImage.sprite = icon;
            descriptionText.text = description;
            pointText.text = point.ToString();
        }
    }
}