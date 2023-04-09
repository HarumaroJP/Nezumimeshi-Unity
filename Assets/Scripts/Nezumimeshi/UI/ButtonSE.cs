using Nezumimeshi.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Nezumimeshi.UI
{
    public class ButtonSE : MonoBehaviour
    {
        [SerializeField] AudioClip onClickSE;

        void Start()
        {
            Button button = GetComponent<Button>();

            button.onClick.AddListener(() =>
            {
                AudioSystem.Instance.PlaySe(onClickSE);
            });
        }
    }
}