using UnityEngine;
using System.Collections;

public class DGPlayer : WTPhysicsNode {
	FSprite sprite;

	public DGPlayer(string name) : base(name) {
		FSprite sprite = new FSprite("player_cell");
		sprite.scale = 0.9f;
		AddChild(sprite);

		physicsComponent.AddRigidBody(0f, 0f, 10f);
		physicsComponent.rigidbody.drag = DGConfig.drag;
		physicsComponent.AddSphereCollider(sprite.width * sprite.scale);
		physicsComponent.SetupPhysicMaterial(0.0f, 0.0f, 0.0f, PhysicMaterialCombine.Minimum);

		ListenForFixedUpdate(HandleFixedUpdate);
	}

	override public void HandleFixedUpdate() {
		if (!physicsComponent.IsControlledByPhysicsEngine()) return;

		Gamepad g = GamepadManager.instance.gamepads[2];
		Debug.Log(GamepadManager.instance.gamepads.Count);

		Vector2 vel = physicsComponent.rigidbody.velocity;

		vel += g.direction * DGConfig.baseVelocityMagnitude;

		if (vel.x > 0) vel.x = Mathf.Min(vel.x, DGConfig.maxVelocity.x);
		if (vel.x < 0) vel.x = Mathf.Max(vel.x, -DGConfig.maxVelocity.x);
		if (vel.y > 0) vel.y = Mathf.Min(vel.y, DGConfig.maxVelocity.y);
		if (vel.y < 0) vel.y = Mathf.Max(vel.y, -DGConfig.maxVelocity.y);

		physicsComponent.rigidbody.velocity = vel;
	}
}
