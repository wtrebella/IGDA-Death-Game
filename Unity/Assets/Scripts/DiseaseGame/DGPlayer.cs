using UnityEngine;
using System.Collections;

public class DGPlayer : WTPhysicsNode {
	FSprite sprite;

	public DGPlayer(string name) : base(name) {
		FSprite sprite = new FSprite("heart");
		sprite.rotation = 90;
		AddChild(sprite);

		physicsComponent.AddRigidBody(0f, 0f, 10f);
		physicsComponent.AddSphereCollider(sprite.width);
		physicsComponent.SetupPhysicMaterial(0.0f, 0.0f, 0.0f, PhysicMaterialCombine.Minimum);

		ListenForFixedUpdate(HandleFixedUpdate);
	}

	override public void HandleFixedUpdate() {
		Gamepad g = GamepadManager.instance.gamepads[0];
		physicsComponent.StartPhysics();
		physicsComponent.rigidbody.velocity = g.direction * 5;
	}
}
