using System;
using Nezumimeshi.Basis;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Nezumimeshi.Omusubi
{
    [Serializable]
    public class OmusubiProfile : IEquatable<OmusubiProfile>
    {
        [ReadOnly] [NonSerialized] public int Id = IdCreator.Get();

        [PropertySpace]
        [HideLabel]
        [PreviewField(50, ObjectFieldAlignment.Right)]
        [HorizontalGroup("row1", 50), VerticalGroup("row1/right")]
        public Sprite Icon;


        [PropertySpace]
        [Multiline(2)]
        [VerticalGroup("row1/left"), LabelWidth(-54)]
        public string Name;


        [VerticalGroup("row1/left"), LabelWidth(-54)]
        public int Point;

        [VerticalGroup("row1/left"), LabelWidth(-54)]
        public Rarity Rarity;

        [VerticalGroup("row1/left"), LabelWidth(-54)]
        public bool isExplode;


        [VerticalGroup("row1/left"), LabelWidth(-54)]
        [Multiline(4)]
        public string Description;


        public bool Equals(OmusubiProfile other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return 397 ^ Id;
            }
        }
    }
}