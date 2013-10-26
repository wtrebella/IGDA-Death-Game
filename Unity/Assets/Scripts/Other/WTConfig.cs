using UnityEngine;
using System.Collections;

public static class WTConfig {
	public static Vector2 objectDrag = new Vector2(10f, 0);
	public static Vector2 maxVelocity = new Vector2(1000f, 1000f);

	public const float gravity = -10f;
	public const float frictionConstant = 0f;
	public const float bounceConstant = 0;
	public const float minBounceDist = 0;
}
