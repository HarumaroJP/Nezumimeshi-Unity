using UnityEngine;

namespace Nezumimeshi.Core.Point
{
    public class GamePoint
    {
        public int Point
        {
            get => _point;
            set => _point = Mathf.Clamp(value, 0, 9999);
        }

        int _point;
    }
}