using Nezumimeshi.Basis;

namespace Nezumimeshi.Core
{
    public class EventHandler
    {
        public readonly AsyncMessage OnGameReady = new AsyncMessage();
        public readonly Message OnGameStart = new Message();
        public readonly Message<Omusubi.Omusubi> OnOmusubiGet = new Message<Omusubi.Omusubi>();
        public readonly Message OnTimeUp = new Message();
        public readonly Message OnBack = new Message();
        public readonly Message OnGameReset = new Message();
    }
}