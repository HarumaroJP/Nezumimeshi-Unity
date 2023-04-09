using UnityEngine;

namespace Nezumimeshi.Profile
{
    [CreateAssetMenu(menuName = "Profiles/GameProfiles")]
    public class GameProfiles : ScriptableObject
    {
        public OmusubiSettings omusubiSettings;
    }
}