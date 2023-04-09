using UnityEditor;
using UnityEngine;
using Wanna.DebugEx;

namespace Nezumimeshi.Editor
{
    public class BuiltinResourceChecker
    {
        [MenuItem("Tools/CheckBuiltinResources")]
        static void CheckBuiltinResources()
        {
            // EditorUtility.DisplayPopupMenu(Rect.zero, "Assets", (MenuCommand)null);
            const string BuiltinResources = "Library/unity editor resources";
            const string BuiltinExtraResources = "Resources/unity_builtin_extra";

            Object[] allAssets = AssetDatabase.LoadAllAssetsAtPath(BuiltinResources);
            Object[] exAssets = AssetDatabase.LoadAllAssetsAtPath(BuiltinExtraResources);

            foreach (Object allAsset in allAssets)
            {
                DebugEx.Log(allAsset.name);
            }
        }
    }
}