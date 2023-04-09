using UnityEngine;
using UnityEngine.InputSystem;

namespace Nezumimeshi.Core
{
    public static class DeviceInput
    {
        static InputActionAsset s_actionAsset;

        static InputAction moveAction;

        public static Vector2 Move => moveAction.ReadValue<Vector2>();

        public static void Init(InputActionAsset actionAsset)
        {
            s_actionAsset = actionAsset;
            AssignActions();
            s_actionAsset.Enable();
        }

        static void AssignActions()
        {
            moveAction = s_actionAsset.FindAction("Move");
        }
    }
}