using System.Collections.Generic;
using UnityEngine;
using Wanna.DebugEx;

namespace Nezumimeshi.Core
{
    /// <summary>
    /// 全体のゲームループをまとめるクラス
    /// </summary>
    public class GameLoop : MonoBehaviour
    {
        static readonly List<IGameLoop> _gameLoops = new List<IGameLoop>();

        public static void Add(IGameLoop gameLoop)
        {
            if (_gameLoops.Contains(gameLoop))
            {
                DebugEx.Log("既に追加されたゲームループです");
                return;
            }

            _gameLoops.Add(gameLoop);
        }

        public static void Remove(IGameLoop gameLoop)
        {
            if (!_gameLoops.Contains(gameLoop))
            {
                DebugEx.Log("存在しないゲームループです");
                return;
            }

            _gameLoops.Remove(gameLoop);
        }

        void Update()
        {
            foreach (IGameLoop gameLoop in _gameLoops)
            {
                gameLoop.OnUpdate();
            }
        }

        void FixedUpdate()
        {
            foreach (IGameLoop gameLoop in _gameLoops)
            {
                gameLoop.OnFixedUpdate();
            }
        }
    }
}