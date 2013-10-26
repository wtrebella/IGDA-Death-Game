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

	float baseScale = 0.5f;
	float minScaleRatioX = 0.9f;
	float maxScaleRatioY = 1.1f;
	List<FAtlasElement> animationMovingElements = new List<FAtlasElement>();
	List<FAtlasElement> animationIdleElements = new List<FAtlasElement>();

	DGPlayer player;
	float animationNextTime = 0;
	float animationStep = 0.075f;
	int animationMovingIndex = 0;
	int animationIdleIndex = 0;

	public DGCell(string name, DGPlayer player) : base(name) {
		this.player = player;

		sprite = new FSprite("player_idle/player_idle1");
		sprite.scale = baseScale;
		AddChild(sprite);

		for (int i = 1; i <= 5; i++) {
			animationMovingElements.Add(Futile.atlasManager.GetElementWithName("player_move/player_move" + i));
		}
		for (int i = 4; i > 1; i--) {
			animationMovingElements.Add(Futile.atlasManager.GetElementWithName("player_move/player_move" + i));
		}

		for (int i = 1; i <= 9; i++) {
			animationIdleElements.Add(Futile.atlasManager.GetElementWithName("player_idle/player_idle" + i));
		}
		for (int i = 8; i > 1; i--) {
			animationIdleElements.Add(Futile.atlasManager.GetElementWithName("player_idle/player_idle" + i));
		}

		physicsComponent.AddRigidBody(0f, 0f, 100f);
		physicsComponent.rigidbody.freezeRotation = true;
		physicsComponent.rigidbody.drag = DGConfig.drag;
		physicsComponent.AddSphereCollider(13);
		physicsComponent.SetupPhysicMaterial(1f, 0.1f, 0.1f);

		ListenForUpdate(HandleUpdate);
		ListenForFixedUpdate(HandleFixedUpdate);
	}

	override public void HandleFixedUpdate() {
		if (!physicsComponent.IsControlledByPhysicsEngine()) return;

		Vector2 vel = physicsComponent.rigidbody.velocity;

		if (player == null || player.gamepad == null) return;

		vel += player.gamepad.direction * DGConfig.baseVelocityMagnitude;

		if (vel.x > 0) vel.x = Mathf.Min(vel.x, DGConfig.maxVelocity.x);
		if (vel.x < 0) vel.x = Mathf.Max(vel.x, -DGConfig.maxVelocity.x);
		if (vel.y > 0) vel.y = Mathf.Min(vel.y, DGConfig.maxVelocity.y);
		if (vel.y < 0) vel.y = Mathf.Max(vel.y, -DGConfig.maxVelocity.y);

		physicsComponent.rigidbody.velocity = vel;
	}

	public bool IsIdle() {
		if (player == null) return false;

		return player.gamepad.direction.magnitude < 0.1f;
	}

	override public void HandleUpdate() {
		sprite.scaleX = Mathf.Clamp(baseScale / physicsComponent.rigidbody.velocity.magnitude, minScaleRatioX * baseScale, 1 * baseScale);
		sprite.scaleY = Mathf.Clamp(baseScale * physicsComponent.rigidbody.velocity.magnitude, 1 * baseScale, maxScaleRatioY * baseScale);

		if (IsIdle()) animationType = CellAnimationType.Idle;
		else animationType = CellAnimationType.Swimming;

		float rotation = 90 - Mathf.Atan2(physicsComponent.rigidbody.velocity.y, physicsComponent.rigidbody.velocity.x) * Mathf.Rad2Deg;

		if ((int)rotation != 90) sprite.rotation = rotation;

		if (animationType == CellAnimationType.Swimming) {
			if (Time.time > animationNextTime) {
				animationMovingIndex++;
				animationNextTime = Time.time + animationStep;
				sprite.element = animationMovingElements[animationMovingIndex%animationMovingElements.Count];
			}
		}

		else if (animationType == CellAnimationType.Idle) {
			if (Time.time > animationNextTime) {
				animationIdleIndex++;
				animationNextTime = Time.time + animationStep;
				sprite.element = animationIdleElements[animationIdleIndex%animationIdleElements.Count];
			}
		}
	}
}
