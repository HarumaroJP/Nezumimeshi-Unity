using System.Threading;
using Nezumimeshi.Basis;
using Nezumimeshi.Core.Point;
using Nezumimeshi.Profile;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nezumimeshi.Core
{
    public class Game : SingletonMonoBehaviour<Game>
    {
        [SerializeField] InputActionAsset inputActionAsset;
        [SerializeField] TimeSettings timeSettings;
        [SerializeField] public OmusubiSettings omusubiSettings;
        [SerializeField] AudioClip menuBGM;
        [SerializeField] AudioClip gameBGM;

        public CancellationTokenSource gameCanceler;

        public EventHandler eHandler = new EventHandler();

        PointController pointCtrl;
        ResultController resultCtrl;
        TimeController timeCtrl;

        protected override void Awake()
        {
            base.Awake();
            //入力デバイスの初期化
            DeviceInput.Init(inputActionAsset);

            //管理クラスの生成
            resultCtrl = new ResultController();
            pointCtrl = new PointController();
            timeCtrl = new TimeController(timeSettings);
            gameCanceler = new CancellationTokenSource();

            //コールバックの登録
            eHandler.OnGameStart.AddListener(() =>
            {
                AudioSystem.Instance.PlayBGM(gameBGM);
            });
            eHandler.OnGameReset.AddListener(timeCtrl.Reset);
            eHandler.OnTimeUp.AddListener(() =>
            {
                gameCanceler.Cancel();
                AudioSystem.Instance.PlayBGM(menuBGM);
            });
            eHandler.OnBack.AddListener(() =>
            {
                gameCanceler?.Dispose();
                gameCanceler = new CancellationTokenSource();
            });

            AudioSystem.Instance.OnLoaded();
            AudioSystem.Instance.PlayBGM(menuBGM);
        }

        public async void ReadyGame()
        {
            await eHandler.OnGameReady.Send();
            StartGame();
        }

        public void StartGame()
        {
            timeCtrl.Start();
            eHandler.OnGameStart.Send();
        }
    }
}