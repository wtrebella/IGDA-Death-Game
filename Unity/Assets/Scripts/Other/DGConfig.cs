using UnityEngine;
using System.Collections;

public static class DGConfig {
	public static Vector2 maxVelocity = new Vector2(7f, 7f);
	
	public static Color [] colors;

	public const float totalInfectionAmountGoal = 10;
	public const float baseVelocityMagnitude = 0.13f;
	public const float drag = 2f;
	public const float gravity = -10f;
	public const float frictionConstant = 0f;
	public const float bounceConstant = 0.5f;
}
