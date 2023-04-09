#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using Wanna.DebugEx;

namespace Nezumimeshi.Core
{
    public static class TMPUnicodeUtil
    {
        static List<TMPUnicodeChecker> checkers = new List<TMPUnicodeChecker>();

        public static void AddChecker(TMPUnicodeChecker checker)
        {
            checkers.Add(checker);
        }

        [MenuItem("Tools/CheckTMPUnicode")]
        public static void CheckAll()
        {
            StringBuilder builder = new StringBuilder();

            foreach (TMPUnicodeChecker checker in checkers)
            {
                builder.Append(checker.NullCheck());
            }

            string nullStr = new string(builder.ToString().ToCharArray().Distinct().ToArray());

            DebugEx.Log(nullStr);
        }
    }
}
#endif