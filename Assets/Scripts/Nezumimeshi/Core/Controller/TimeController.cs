using System;
using Cysharp.Threading.Tasks;
using Nezumimeshi.Profile;
using Nezumimeshi.UI;
using UnityEngine;

namespace Nezumimeshi.Core
{
    public class TimeController
    {
        readonly TimerView timerView;
        readonly TimeSettings settings;

        public TimeController(TimeSettings settings)
        {
            this.settings = settings;
            Transform ui = GameObject.FindWithTag("UI").transform;
            this.timerView = ui.Find("Timer").GetComponent<TimerView>();

            Reset();
        }

        public void Reset()
        {
            timerView.SetTime(settings.gameTime);
        }

        public async void Start()
        {
            int remaining = settings.gameTime;
            TimeSpan secSpan = TimeSpan.FromSeconds(1);

            //1秒間隔でViewを更新する
            for (int i = remaining - 1; i >= 0; i--)
            {
                await UniTask.Delay(secSpan);
                timerView.SetTime(i);
            }

            Game.Instance.eHandler.OnTimeUp.Send();
        }
    }
}