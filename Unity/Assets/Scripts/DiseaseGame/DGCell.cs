using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CellAnimationType {
	Idle,
	Swimming
}

public class DGCell : WTPhysicsNode {
	public FSprite sprite;
	public CellAnimationType animationType = CellAnimationType.Swimming;

	List<FAtlasElement> animationElements = new List<FAtlasElement>();

	DGPlayer player;
	float animationNextTime = 0;
	float animationStep = 0.1f;
	int animationIndex = 0;

	public DGCell(string name, DGPlayer player) : base(name) {
		this.player = player;

		sprite = new FSprite("player_move/player_move1");
		AddChild(sprite);

		for (int i = 1; i <= 5; i++) {
			animationElements.Add(Futile.atlasManager.GetElementWithName("player_move/player_move" + i));
		}
		for (int i = 4; i > 1; i--) {
			animationElements.Add(Futile.atlasManager.GetElementWithName("player_move/player_move" + i));
		}

		physicsComponent.AddRigidBody(0f, 0f, 100f);
		physicsComponent.rigidbody.drag = DGConfig.drag;
		physicsComponent.AddSphereCollider(25);
		physicsComponent.SetupPhysicMaterial(1f, 0.1f, 0.1f);

		ListenForUpdate(HandleUpdate);
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

	override public void HandleUpdate() {
		if (animationType == CellAnimationType.Swimming) {
			if (Time.time > animationNextTime) {
				animationIndex++;
				animationNextTime = Time.time + animationStep;
				sprite.element = animationElements[animationIndex%animationElements.Count];
			}
		}
	}
}
