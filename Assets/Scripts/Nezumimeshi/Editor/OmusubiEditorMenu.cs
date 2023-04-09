using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Nezumimeshi.Editor
{
    public class OmusubiEditorMenu : OdinMenuEditorWindow
    {
        const string PROFILE_PATH = "Assets/Profile/";

        [MenuItem("Omusubi/Settings", false, 1000)]
        static void OpenWindow()
        {
            GetWindow<OmusubiEditorMenu>().Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree();
            tree.Selection.SupportsMultiSelect = false;

            tree.AddAssetAtPath("Main",
                PROFILE_PATH + "OmusubiSettings.asset",
                typeof(ScriptableObject));

            return tree;
        }
    }
}