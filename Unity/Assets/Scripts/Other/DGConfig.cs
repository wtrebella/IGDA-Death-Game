using UnityEngine;
using System.Collections;

public static class DGConfig {
	public static Vector2 maxVelocity = new Vector2(2f, 2f);

	public const float baseVelocityMagnitude = 0.25f;
	public const float drag = 2f;
	public const float gravity = -10f;
	public const float frictionConstant = 0f;
	public const float bounceConstant = 0;
	public const float minBounceDist = 0;
}
