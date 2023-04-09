using Nezumimeshi.UI;
using UnityEngine;

namespace Nezumimeshi.Core.Point
{
    public class PointController
    {
        readonly GamePoint gamePoint;
        readonly PointView pointView;

        public PointController()
        {
            gamePoint = new GamePoint();

            Transform ui = GameObject.FindWithTag("UI").transform;
            pointView = ui.Find("Point").GetComponent<PointView>();

            Game.Instance.eHandler.OnOmusubiGet.AddListener(AddPoint);
            Game.Instance.eHandler.OnBack.AddListener(Reset);
        }

        void AddPoint(Omusubi.Omusubi omusubi)
        {
            gamePoint.Point += omusubi.Point;
            pointView.SetPoint(gamePoint.Point);
        }

        void Reset()
        {
            gamePoint.Point = 0;
            pointView.SetPoint(0);
        }
    }
}