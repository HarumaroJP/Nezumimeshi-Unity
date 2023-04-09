using System;
using Nezumimeshi.Omusubi;
using TMPro;
using UnityEngine;

namespace Nezumimeshi.Core
{
    public class ResultView : MonoBehaviour
    {
        [SerializeField] GameObject omusubiItem;
        [SerializeField] Transform omusubisParent;
        [SerializeField] TextMeshProUGUI totalText;

        public void AddResult(OmusubiProfile omusubi, int count, int point)
        {
            Transform item = Instantiate(omusubiItem, omusubisParent).transform;
            ResultItemView itemView = item.GetComponent<ResultItemView>();

            itemView.Icon.sprite = omusubi.Icon;
            itemView.Counter.text = count.ToString();
            itemView.Point.text = point.ToString();

            item.SetSiblingIndex(omusubisParent.childCount - 2);
        }

        public void SetTotalPoint(int totalPoint)
        {
            totalText.text = totalPoint.ToString();
        }

        public void ResetData()
        {
            totalText.text = String.Empty;

            for (int i = 0; i < omusubisParent.childCount - 1; i++)
            {
                Destroy(omusubisParent.GetChild(i).gameObject);
            }
        }
    }
}