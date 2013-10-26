using UnityEngine;
using System.Collections;

public class WTPivvotInput {
	public static bool GetRightDown() {
#if !UNITY_EDITOR
		if (WTConfig.controlType == ControlType.NONE) return false;
#endif
		bool buttonIsDown = false;
		
		foreach (Gamepad gamepad in GamepadManager.instance.gamepads) {
			if (gamepad.controlType == ControlType.Xbox) {
				buttonIsDown =
					buttonIsDown ||
					Input.GetKeyDown(GetXboxButtonString(gamepad, XboxButtonType.RB)) ||
					Input.GetKeyDown(GetXboxButtonString(gamepad, XboxButtonType.Right)) ||
					gamepad.directionTypeDown == DirectionType.Right;
			}
			else if (gamepad.controlType == ControlType.PS3) {
				buttonIsDown = 
					buttonIsDown ||
					Input.GetKeyDown(GetPS3ButtonString(gamepad, PS3ButtonType.R1)) ||
					Input.GetKeyDown(GetPS3ButtonString(gamepad, PS3ButtonType.Right)) ||
					gamepad.directionTypeDown == DirectionType.Right;
			}
		}

		return Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || buttonIsDown;
	}
	
	public static bool GetLeftDown() {
#if !UNITY_EDITOR
		if (WTConfig.controlType == ControlType.NONE) return false;
#endif
		bool buttonIsDown = false;

		foreach (Gamepad gamepad in GamepadManager.instance.gamepads) {
			if (gamepad.controlType == ControlType.Xbox) {
				buttonIsDown =
					buttonIsDown ||
					Input.GetKeyDown(GetXboxButtonString(gamepad, XboxButtonType.LB)) ||
					Input.GetKeyDown(GetXboxButtonString(gamepad, XboxButtonType.Left)) ||
					gamepad.directionTypeDown == DirectionType.Left;
			}
			else if (gamepad.controlType == ControlType.PS3) {
				buttonIsDown = 
					buttonIsDown ||
					Input.GetKeyDown(GetPS3ButtonString(gamepad, PS3ButtonType.L1)) ||
					Input.GetKeyDown(GetPS3ButtonString(gamepad, PS3ButtonType.Left)) ||
					gamepad.directionTypeDown == DirectionType.Left;
			}
		}

		return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || buttonIsDown;
	}
	
	public static bool GetRight() {
#if !UNITY_EDITOR
		if (WTConfig.controlType == ControlType.NONE) return false;
#endif
		bool button = false;

		foreach (Gamepad gamepad in GamepadManager.instance.gamepads) {
			if (gamepad.controlType == ControlType.Xbox) {
				button =
					button ||
					Input.GetKey(GetXboxButtonString(gamepad, XboxButtonType.RB)) ||
					Input.GetKey(GetXboxButtonString(gamepad, XboxButtonType.Right)) ||
					gamepad.directionType == DirectionType.Right;
			}
			else if (gamepad.controlType == ControlType.PS3) {
				button = 
					button ||
					Input.GetKey(GetPS3ButtonString(gamepad, PS3ButtonType.R1)) ||
					Input.GetKey(GetPS3ButtonString(gamepad, PS3ButtonType.Right)) ||
					gamepad.directionType == DirectionType.Right;
			}
		}

		return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || button;
	}
	
	public static bool GetLeft() {
#if !UNITY_EDITOR
		if (WTConfig.controlType == ControlType.NONE) return false;
#endif
		bool button = false;

		foreach (Gamepad gamepad in GamepadManager.instance.gamepads) {
			if (gamepad.controlType == ControlType.Xbox) {
				button =
					button ||
					Input.GetKey(GetXboxButtonString(gamepad, XboxButtonType.LB)) ||
					Input.GetKey(GetXboxButtonString(gamepad, XboxButtonType.Left)) ||
					gamepad.directionType == DirectionType.Left;
			}
			else if (gamepad.controlType == ControlType.PS3) {
				button = 
					button ||
					Input.GetKey(GetPS3ButtonString(gamepad, PS3ButtonType.L1)) ||
					Input.GetKey(GetPS3ButtonString(gamepad, PS3ButtonType.Left)) ||
					gamepad.directionType == DirectionType.Left;
			}
		}

		return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || button;
	}
	
	public static bool GetRightUp() {
#if !UNITY_EDITOR
		if (WTConfig.controlType == ControlType.NONE) return false;
#endif

		bool buttonIsUp = false;

		foreach (Gamepad gamepad in GamepadManager.instance.gamepads) {
			if (gamepad.controlType == ControlType.Xbox) {
				buttonIsUp =
					buttonIsUp ||
					Input.GetKeyUp(GetXboxButtonString(gamepad, XboxButtonType.RB)) ||
					Input.GetKeyUp(GetXboxButtonString(gamepad, XboxButtonType.Right)) ||
					gamepad.directionTypeUp == DirectionType.Right;
			}
			else if (gamepad.controlType == ControlType.PS3) {
				buttonIsUp = 
					buttonIsUp ||
					Input.GetKeyUp(GetPS3ButtonString(gamepad, PS3ButtonType.R1)) ||
					Input.GetKeyUp(GetPS3ButtonString(gamepad, PS3ButtonType.Right)) ||
					gamepad.directionTypeUp == DirectionType.Right;
			}
		}

		return Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) || buttonIsUp;
	}
	
	public static bool GetLeftUp() {
#if !UNITY_EDITOR
		if (WTConfig.controlType == ControlType.NONE) return false;
#endif
		bool buttonIsUp = false;

		foreach (Gamepad gamepad in GamepadManager.instance.gamepads) {
			if (gamepad.controlType == ControlType.Xbox) {
				buttonIsUp =
					buttonIsUp ||
					Input.GetKeyUp(GetXboxButtonString(gamepad, XboxButtonType.LB)) ||
					Input.GetKeyUp(GetXboxButtonString(gamepad, XboxButtonType.Left)) ||
					gamepad.directionTypeUp == DirectionType.Left;
			}
			else if (gamepad.controlType == ControlType.PS3) {
				buttonIsUp = 
					buttonIsUp ||
					Input.GetKeyUp(GetPS3ButtonString(gamepad, PS3ButtonType.L1)) ||
					Input.GetKeyUp(GetPS3ButtonString(gamepad, PS3ButtonType.Left)) ||
					gamepad.directionTypeUp == DirectionType.Left;
			}
		}

		return Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || buttonIsUp;
	}

	public static bool GetXboxButtonDown(XboxButtonType buttonType) {
#if !UNITY_EDITOR
		if (WTConfig.controlType == ControlType.NONE) return false;
#endif
		bool buttonIsDown = false;

		foreach (Gamepad gamepad in GamepadManager.instance.gamepads) {
			if (gamepad.controlType != ControlType.Xbox) continue;

			buttonIsDown = buttonIsDown || Input.GetKeyDown(GetXboxButtonString(gamepad, buttonType));
		}

		return buttonIsDown;
	}

	public static bool GetXboxButton(XboxButtonType buttonType) {
		if (WTConfig.controlType == ControlType.NONE) return false;

		bool button = false;

		foreach (Gamepad gamepad in GamepadManager.instance.gamepads) {
			if (gamepad.controlType != ControlType.Xbox) continue;

			button = button || Input.GetKey(GetXboxButtonString(gamepad, buttonType));
		}

		return button;
	}

	public static bool GetXboxButtonUp(XboxButtonType buttonType) {
#if !UNITY_EDITOR
		if (WTConfig.controlType == ControlType.NONE) return false;
#endif
		bool buttonIsUp = false;

		foreach (Gamepad gamepad in GamepadManager.instance.gamepads) {
			if (gamepad.controlType != ControlType.Xbox) continue;

			buttonIsUp = buttonIsUp || Input.GetKeyUp(GetXboxButtonString(gamepad, buttonType));
		}

		return buttonIsUp;
	}

	public static bool GetPS3ButtonDown(PS3ButtonType buttonType) {
#if !UNITY_EDITOR
		if (WTConfig.controlType == ControlType.NONE) return false;
#endif
		bool buttonIsDown = false;

		foreach (Gamepad gamepad in GamepadManager.instance.gamepads) {
			if (gamepad.controlType != ControlType.PS3) continue;

			buttonIsDown = buttonIsDown || Input.GetKeyDown(GetPS3ButtonString(gamepad, buttonType));
		}

		return buttonIsDown;
	}

	public static bool GetPS3Button(PS3ButtonType buttonType) {
#if !UNITY_EDITOR
		if (WTConfig.controlType == ControlType.NONE) return false;
#endif
		bool button = false;

		foreach (Gamepad gamepad in GamepadManager.instance.gamepads) {
			if (gamepad.controlType != ControlType.PS3) continue;

			button = button || Input.GetKey(GetPS3ButtonString(gamepad, buttonType));
		}

		return button;
	}

	public static bool GetPS3ButtonUp(PS3ButtonType buttonType) {
#if !UNITY_EDITOR
		if (WTConfig.controlType == ControlType.NONE) return false;
#endif
		bool buttonIsUp = false;

		foreach (Gamepad gamepad in GamepadManager.instance.gamepads) {
			if (gamepad.controlType != ControlType.PS3) continue;

			buttonIsUp = buttonIsUp || Input.GetKeyUp(GetPS3ButtonString(gamepad, buttonType));
		}

		return buttonIsUp;
	}
	
	public static string GetPS3ButtonString(Gamepad gamepad, PS3ButtonType buttonType) {
		return gamepad.buttonJoyName + " button " + buttonType.numStringOSX;
	}
	
	public static string GetXboxButtonString(Gamepad gamepad, XboxButtonType buttonType) {
		string numString = WTUtils.IsWindows()?buttonType.numStringWin:buttonType.numStringOSX;
		return gamepad.buttonJoyName + " button " + numString;
	}
}
