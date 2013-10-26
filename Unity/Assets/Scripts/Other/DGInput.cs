using UnityEngine;
using System.Collections;

public class DGInput {
	public static bool GetXboxButtonDown(Gamepad gamepad, XboxButtonType buttonType) {
		bool buttonIsDown = false;

		foreach (Gamepad g in GamepadManager.instance.gamepads) {
			if (g != gamepad) continue;
			if (g.controlType != ControlType.Xbox) continue;

			buttonIsDown = buttonIsDown || Input.GetKeyDown(GetXboxButtonString(gamepad, buttonType));
		}

		return buttonIsDown;
	}

	public static bool GetXboxButton(Gamepad gamepad, XboxButtonType buttonType) {
		bool button = false;

		foreach (Gamepad g in GamepadManager.instance.gamepads) {
			if (g != gamepad) continue;
			if (g.controlType != ControlType.Xbox) continue;

			button = button || Input.GetKey(GetXboxButtonString(gamepad, buttonType));
		}

		return button;
	}

	public static bool GetXboxButtonUp(Gamepad gamepad, XboxButtonType buttonType) {
		bool buttonIsUp = false;

		foreach (Gamepad g in GamepadManager.instance.gamepads) {
			if (g != gamepad) continue;
			if (g.controlType != ControlType.Xbox) continue;

			buttonIsUp = buttonIsUp || Input.GetKeyUp(GetXboxButtonString(gamepad, buttonType));
		}

		return buttonIsUp;
	}

	public static bool GetPS3ButtonDown(Gamepad gamepad, PS3ButtonType buttonType) {
		bool buttonIsDown = false;

		foreach (Gamepad g in GamepadManager.instance.gamepads) {
			if (g != gamepad) continue;
			if (g.controlType != ControlType.PS3) continue;

			buttonIsDown = buttonIsDown || Input.GetKeyDown(GetPS3ButtonString(gamepad, buttonType));
		}

		return buttonIsDown;
	}

	public static bool GetPS3Button(Gamepad gamepad, PS3ButtonType buttonType) {
		bool button = false;

		foreach (Gamepad g in GamepadManager.instance.gamepads) {
			if (g != gamepad) continue;
			if (g.controlType != ControlType.PS3) continue;

			button = button || Input.GetKey(GetPS3ButtonString(gamepad, buttonType));
		}

		return button;
	}

	public static bool GetPS3ButtonUp(Gamepad gamepad, PS3ButtonType buttonType) {
		bool buttonIsUp = false;

		foreach (Gamepad g in GamepadManager.instance.gamepads) {
			if (g != gamepad) continue;
			if (g.controlType != ControlType.PS3) continue;

			buttonIsUp = buttonIsUp || Input.GetKeyUp(GetPS3ButtonString(gamepad, buttonType));
		}

		return buttonIsUp;
	}
	
	public static string GetPS3ButtonString(Gamepad gamepad, PS3ButtonType buttonType) {
		return gamepad.buttonJoyName + " button " + buttonType.numStringOSX;
	}
	
	public static string GetXboxButtonString(Gamepad gamepad, XboxButtonType buttonType) {
		string numString = DGMain.IsWindows()?buttonType.numStringWin:buttonType.numStringOSX;
		return gamepad.buttonJoyName + " button " + numString;
	}
}