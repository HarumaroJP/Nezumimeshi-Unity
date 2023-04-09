using System.Text;
using TMPro;
using UnityEngine;

namespace Nezumimeshi.Core
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TMPUnicodeChecker : MonoBehaviour
    {
#if UNITY_EDITOR
        TextMeshProUGUI textMesh;

        void Awake()
        {
            textMesh = GetComponent<TextMeshProUGUI>();
            TMPUnicodeUtil.AddChecker(this);
        }

        public string NullCheck()
        {
            StringBuilder builder = new StringBuilder();
            string text = textMesh.text;

            for (int i = 0; i < text.Length; i++)
            {
                int utf32 = char.ConvertToUtf32(text, i);

                TMP_Character character =
                    TMP_FontAssetUtilities.GetCharacterFromFontAsset((uint)utf32, textMesh.font, false, textMesh.fontStyle,
                        textMesh.fontWeight, out bool _);

                if (character == null)
                {
                    builder.Append(char.ConvertFromUtf32(utf32));
                }
            }

            return builder.ToString();
        }
#endif
    }
}