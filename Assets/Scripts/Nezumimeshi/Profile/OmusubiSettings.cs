using System.Collections.Generic;
using Nezumimeshi.Omusubi;
using UnityEngine;

namespace Nezumimeshi.Profile
{
    [CreateAssetMenu(menuName = "Profiles/OmusubiSettings")]
    public class OmusubiSettings : ScriptableObject {
        public List<OmusubiProfile> profiles;

        public OmusubiProfile FindProfile(int id) { return profiles.Find(om => om.Id == id); }
    }
}