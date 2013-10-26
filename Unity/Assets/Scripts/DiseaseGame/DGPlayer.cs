using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DGPlayer {
	public static List<DGPlayer> players = new List<DGPlayer>();

	public ControlType controlType = ControlType.NONE;
	public string diseaseName;
	public Gamepad gamepad = null;

	public DGPlayer() {

	}
}
