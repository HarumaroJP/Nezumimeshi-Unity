using System.Collections.Generic;
using Nezumimeshi.Omusubi;
using Nezumimeshi.Profile;
using Nezumimeshi.UI;
using UnityEngine;

namespace Nezumimeshi.Core
{
    public class ResultController
    {
        ResultView resultView;
        NoticeView noticeView;
        OmusubiSettings _settings;
        Dictionary<OmusubiProfile, int> getOmusubis = new Dictionary<OmusubiProfile, int>();

        public ResultController()
        {
            Transform ui = GameObject.FindWithTag("UI").transform;
            resultView = ui.Find("Result").GetComponent<ResultView>();
            noticeView = ui.Find("Notice").GetComponent<NoticeView>();

            _settings = Game.Instance.omusubiSettings;
            Game.Instance.eHandler.OnOmusubiGet.AddListener(AddOmusubi);
            Game.Instance.eHandler.OnTimeUp.AddListener(SequenceResult);
            Game.Instance.eHandler.OnBack.AddListener(Reset);
        }

        async void SequenceResult()
        {
            await noticeView.ShowResultText();
            SetResult();
            resultView.gameObject.SetActive(true);
        }

        void AddOmusubi(Omusubi.Omusubi omusubi)
        {
            OmusubiProfile profile = _settings.FindProfile(omusubi.Id);

            if (getOmusubis.ContainsKey(profile))
            {
                getOmusubis[profile] += 1;
            }
            else
            {
                getOmusubis.Add(profile, 1);
            }
        }

        void SetResult()
        {
            int totalPoints = 0;

            //個数とポイントの計算
            foreach (KeyValuePair<OmusubiProfile, int> pair in getOmusubis)
            {
                int count = pair.Value;
                int point = pair.Key.Point * pair.Value;

                resultView.AddResult(pair.Key, count, point);

                totalPoints += point;
            }

            resultView.SetTotalPoint(Mathf.Clamp(totalPoints, 0, totalPoints));
        }

        void Reset()
        {
            Game.Instance.eHandler.OnGameReset.Send();
            getOmusubis.Clear();
            resultView.ResetData();
            resultView.gameObject.SetActive(false);
        }
    }
}