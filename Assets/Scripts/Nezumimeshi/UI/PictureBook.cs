using System.Collections.Generic;
using Nezumimeshi.Core;
using Nezumimeshi.Omusubi;
using Nezumimeshi.Profile;
using UnityEngine;
using UnityEngine.UI;

namespace Nezumimeshi.UI
{
    public class PictureBook : MonoBehaviour
    {
        [SerializeField] GameObject rootObj;
        [SerializeField] GameObject pictureItem;
        [SerializeField] GameObject itemButton;

        [SerializeField] Transform pictureItemParent;
        [SerializeField] Transform itemButtonParent;

        [SerializeField] Vector2 firstAnchoredPos;
        [SerializeField] Vector2 lastAnchoredPos;

        OmusubiSettings _settings;

        List<GameObject> pictures = new List<GameObject>();
        Dictionary<int, PictureItemButton> pictureButtons = new Dictionary<int, PictureItemButton>();

        int beforeIdx = 0;

        void Start()
        {
            _settings = Game.Instance.omusubiSettings;
            CreateBook();
        }

        public void SetOpen(bool isOpen)
        {
            rootObj.SetActive(isOpen);

            if (isOpen)
            {
                SetOpenItem(beforeIdx, true);
            }
        }

        void CreateBook()
        {
            for (int i = 0; i < _settings.profiles.Count; i++)
            {
                OmusubiProfile profile = _settings.profiles[i];
                //奇数位置か偶数位置かを判定

                GameObject item = CreatePicture(i);
                pictures.Add(item);

                item.GetComponent<PictureItemView>().SetOmusubi(profile.Name, profile.Icon, profile.Description, profile.Point);
                item.SetActive(false);

                CreateButton(i);
            }

            SetOpenItem(0, true);
        }

        GameObject CreatePicture(int indexPos)
        {
            bool isEven = (indexPos + 1) % 2 == 0;
            Vector2 pos = !isEven ? firstAnchoredPos : lastAnchoredPos;
            GameObject picture = Instantiate(pictureItem, pos, Quaternion.identity, pictureItemParent);
            picture.GetComponent<RectTransform>().anchoredPosition = pos;

            return picture;
        }

        void CreateButton(int indexPos)
        {
            bool isEven = (indexPos + 1) % 2 == 0;

            if (isEven)
            {
                return;
            }

            Button button = Instantiate(itemButton, itemButtonParent).GetComponent<Button>();
            PictureItemButton footerButton = button.GetComponent<PictureItemButton>();
            footerButton.OnStart();

            pictureButtons.Add(indexPos, footerButton);

            button.onClick.AddListener(() =>
            {
                SetOpenItem(beforeIdx, false);
                SetOpenItem(indexPos, true);

                beforeIdx = indexPos;
            });
        }

        void SetOpenItem(int idx, bool isOpen)
        {
            pictures[idx].SetActive(isOpen);
            pictureButtons[idx].isSelecting = isOpen;

            if (pictures.Count >= idx + 2)
            {
                pictures[idx + 1].SetActive(isOpen);
            }
        }
    }
}