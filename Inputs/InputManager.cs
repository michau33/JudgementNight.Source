using UnityEngine;

namespace Assets.Scripts.Inputs
{
    public static class InputManager
    {
        private static ControlType _controlType;

        public static void Initialize(ControlType controlType)
        {
            InputManager._controlType = controlType;
        }

        public static float Horizontal()
        {
            return _controlType == ControlType.Keyboard
                ? Input.GetAxisRaw(KeyboardKeys.KeyboardHorizontal)
                : Input.GetAxis(GamepadButtons.GamepadLeftHorizontal);
        }

        public static float Vertical()
        {
            return _controlType == ControlType.Keyboard
                ? Input.GetAxisRaw(KeyboardKeys.KeyboardVertical)
                : Input.GetAxis(GamepadButtons.GamepadLeftVertical);
        }

        // Following two axes are only available on gamepad for character's rotation purposes
        public static float RightHorizontal()
        {
            return _controlType == ControlType.Gamepad ? Input.GetAxis(GamepadButtons.GamepadRightHorizontal) : 0f;
        }

        public static float RightVertical()
        {
            return _controlType == ControlType.Gamepad ? Input.GetAxis(GamepadButtons.GamepadRightVertical) : 0f;
        }

        // TODO FOLLOWiNG BUTTONS WON'T WORK ON GAMEPAD BECAUSE TRIGGERS ARE NOT BUTTONS BUT AXES !!!
        // TODO FOR NOW I OVERCAME THIS PROBLEM BY CHECKING IF RAW AXIS IS NOUGHT OR NOT

        public static bool FireButtonHeld()
        {
            // 6th axis is right gamepad trigger
            return _controlType == ControlType.Keyboard ? Input.GetMouseButton(0) : Input.GetAxisRaw(GamepadButtons.RightGamepadTrigger) != 0f;
        }

        public static bool FireButtonPressed()
        {
            // 6th axis is right gamepad trigger
            return _controlType == ControlType.Keyboard ? Input.GetMouseButtonDown(0) : Input.GetAxisRaw(GamepadButtons.RightGamepadTrigger) != 0f;
        }

        public static bool FireButtonReleased()
        {
            return _controlType == ControlType.Keyboard ? Input.GetMouseButtonUp(0) : Input.GetAxisRaw(GamepadButtons.RightGamepadTrigger) == 0f;
        }

        public static bool ReloadingButtonHeld()
        {
            // change to reloading button on the keyboard
            return _controlType == ControlType.Keyboard ? Input.GetKey(KeyCode.R) : Input.GetButton(GamepadButtons.GamepadXButton);
        }

        public static bool AimingPressed()
        {      
            return _controlType == ControlType.Keyboard ? Input.GetMouseButtonDown(1) : Input.GetAxisRaw(GamepadButtons.LeftGamepadTrigger) != 0f;           
        }

        public static bool AimingReleased()
        {
            return _controlType == ControlType.Keyboard ? Input.GetMouseButtonUp(1) : Input.GetAxisRaw(GamepadButtons.LeftGamepadTrigger) == 0f;
        }
    }
}


