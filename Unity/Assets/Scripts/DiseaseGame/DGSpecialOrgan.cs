using UnityEngine;
using System.Collections;
using System;

public class DGSpecialOrgan : WTPhysicsNode {
	FSprite sprite;
	WTPhysicsNode boxPart;
	WTPhysicsNode circlePart;
	public float[] infectionAmounts;
	public event Action<DGSpecialOrgan> SignalOrganIsFullyInfected;
	bool fullyInfected = false;

	public DGSpecialOrgan(string name, OrganType organType, int infectionCount) : base(name) {
		infectionAmounts = new float[infectionCount];
		for (int i = 0; i < infectionCount; i++) infectionAmounts[i] = 0;

		if (organType == OrganType.Heart) {
			sprite = new FSprite("heart");
			AddChild(sprite);

			boxPart = new WTPhysicsNode("heartBox");
			//boxPart.physicsComponent.AddRigidBody(0f, 0f, 1000f);
			boxPart.physicsComponent.AddBoxCollider(new Vector2(5, 20));
			boxPart.physicsComponent.SetupPhysicMaterial(1f, 0.0f, 0.0f);
			boxPart.physicsComponent.SetIsTrigger(true);
			boxPart.physicsComponent.SignalOnTriggerStay += HandleOnTriggerStay;
			boxPart.x -= 2;
			boxPart.y += 65;
			AddChild(boxPart);

			circlePart = new WTPhysicsNode("heartCircle");
			//circlePart.physicsComponent.AddRigidBody(0f, 0f, 1000f);
			circlePart.physicsComponent.AddSphereCollider(6);
			circlePart.physicsComponent.SetupPhysicMaterial(1f, 0.0f, 0.0f);
			circlePart.physicsComponent.SetIsTrigger(true);
			circlePart.physicsComponent.SignalOnTriggerStay += HandleOnTriggerStay;
			circlePart.x += 3;
			circlePart.y += 50;
			AddChild(circlePart);
		}

		ListenForFixedUpdate(HandleFixedUpdate);
	}

	override public void HandleOnTriggerStay(Collider coll) {
		DGCell cell = (DGCell)(coll.gameObject.GetComponent<WTPhysicsComponent>().container);

		int infectionIndex = -1;

		for (int i = 0; i < DGPlayer.players.Count; i++) {
			if (cell.player == DGPlayer.players[i]) {
				infectionIndex = i;
				break;
			}
		}

		if (!fullyInfected) {
			infectionAmounts[infectionIndex] += Time.fixedDeltaTime;

			if (GetTotalInfectionAmount() > DGConfig.totalInfectionAmountGoal) {
				infectionAmounts[infectionIndex] = DGConfig.totalInfectionAmountGoal - (GetTotalInfectionAmount() - infectionAmounts[infectionIndex]);
				fullyInfected = true;
				if (SignalOrganIsFullyInfected != null) SignalOrganIsFullyInfected(this);
			}
		}
	}

	public float GetTotalInfectionAmount() {
		float infectionAmt = 0;

		foreach (float f in infectionAmounts) infectionAmt += f;

		return infectionAmt;
	}

	public float GetInfectionAmountForPlayerNum(int playerNum) {
		return infectionAmounts[playerNum];
	}

	override public void HandleFixedUpdate() {

	}
}
