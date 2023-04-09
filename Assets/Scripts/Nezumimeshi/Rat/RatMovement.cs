using System;
using Nezumimeshi.Core;
using UnityEngine;

namespace Nezumimeshi.Rat
{
    public class RatMovement : IGameLoop
    {
        readonly Transform _transform;
        readonly MovementSettings _settings;
        readonly Vector2 origin;

        public RatMovement(Transform transform, MovementSettings settings)
        {
            _transform = transform;
            _settings = settings;
            origin = transform.localPosition;

            Game.Instance.eHandler.OnGameStart.AddListener(() =>
            {
                GameLoop.Add(this);
            });
            Game.Instance.eHandler.OnTimeUp.AddListener(() =>
            {
                GameLoop.Remove(this);
            });
            Game.Instance.eHandler.OnBack.AddListener(() =>
            {
                _transform.localPosition = origin;
            });
        }

        public void OnUpdate()
        {
            Move();
        }

        public void OnFixedUpdate() { }

        void Move()
        {
            Vector2 input = DeviceInput.Move * (_settings.correctionFactor * Time.deltaTime);
            Vector2 current = _transform.localPosition;

            float range = _settings.movableRange;
            float x = Mathf.Clamp(current.x + input.x, origin.x - range, origin.x + range);

            _transform.localPosition = new Vector2(x, current.y);
        }
    }

    [Serializable]
    public class MovementSettings
    {
        public float correctionFactor;
        public float movableRange;
    }
}