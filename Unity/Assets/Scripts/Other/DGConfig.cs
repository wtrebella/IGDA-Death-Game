using UnityEngine;
using System.Collections;

public static class DGConfig {
	public static Vector2 maxVelocity = new Vector2(10f, 10f);
	
	public static Color [] colors = new Color[] {
		Color.blue,
		Color.red,
		Color.green,
		Color.yellow
	};

	public const float baseVelocityMagnitude = 0.25f;
	public const float drag = 2f;
	public const float gravity = -10f;
	public const float frictionConstant = 0f;
	public const float bounceConstant = 0.5f;
}
