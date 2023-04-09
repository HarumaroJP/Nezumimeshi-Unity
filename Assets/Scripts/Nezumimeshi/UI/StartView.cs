using Nezumimeshi.Basis;
using Nezumimeshi.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Nezumimeshi.UI
{
    public class StartView : MonoBehaviour
    {
        [SerializeField] GameObject backBlur;
        [SerializeField] GameObject titleElement;
        [SerializeField] PictureBook pictureBook;
        [SerializeField] NoticeView noticeView;
        [SerializeField] Button backButton;
        [SerializeField] Button pictureBackButton;
        [SerializeField] TweenableButton startButton;
        [SerializeField] TweenableButton studyButton;

        void Start()
        {
            Game.Instance.eHandler.OnGameReady.AddListener(noticeView.ShowStartText);
            Game.Instance.eHandler.OnBack.AddListener(ReturnToMenu);

            startButton.onClickByTween.AddListener(GameStart);
            studyButton.onClickByTween.AddListener(OpenPictureBook);
            backButton.onClick.AddListener(Game.Instance.eHandler.OnBack.Send);
            pictureBackButton.onClick.AddListener(() =>
            {
                pictureBook.SetOpen(false);
                ReturnToMenu();
            });
        }

        public void GameStart()
        {
            SetOpenMenu(false, false);
            Game.Instance.ReadyGame();
        }

        public void OpenPictureBook()
        {
            SetOpenMenu(false, true);
            pictureBook.SetOpen(true);
        }

        public void ReturnToMenu()
        {
            startButton.ForceReset();
            studyButton.ForceReset();
            SetOpenMenu(true, true);
        }

        public void SetOpenMenu(bool isOpen, bool isBlur)
        {
            titleElement.SetActive(isOpen);
            backBlur.SetActive(isBlur);
        }
    }
}