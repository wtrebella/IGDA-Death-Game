using UnityEngine;
using System.Collections;

public class DGCell : WTPhysicsNode {
	FSprite sprite;
	DGPlayer player;

	public DGCell(string name, DGPlayer player) : base(name) {
		this.player = player;

		FSprite sprite = new FSprite("player_cell");
		sprite.color = new Color(0.2f, 0.4f, 1.0f);
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

		Vector2 vel = physicsComponent.rigidbody.velocity;

		if (player.gamepad == null) return;

		vel += player.gamepad.direction * DGConfig.baseVelocityMagnitude;

		if (vel.x > 0) vel.x = Mathf.Min(vel.x, DGConfig.maxVelocity.x);
		if (vel.x < 0) vel.x = Mathf.Max(vel.x, -DGConfig.maxVelocity.x);
		if (vel.y > 0) vel.y = Mathf.Min(vel.y, DGConfig.maxVelocity.y);
		if (vel.y < 0) vel.y = Mathf.Max(vel.y, -DGConfig.maxVelocity.y);

		physicsComponent.rigidbody.velocity = vel;
	}
}
