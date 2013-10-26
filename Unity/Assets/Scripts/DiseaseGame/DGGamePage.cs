using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DGGamePage : AbstractPage {
	List<DGCell> cells = new List<DGCell>();
	List<FLabel> labels = new List<FLabel>();
	FContainer sceneContainer = new FContainer();
	FContainer playableContainer = new FContainer();
	DGSpecialOrgan heart;

	public DGGamePage() {
		FSprite deathBG = new FSprite("death_bg");
		//deathBG.SetPosition(Futile.screen.halfWidth, Futile.screen.halfHeight);
		//float ratio = deathBG.height / deathBG.width;
		//deathBG.width = Futile.screen.width;
		//deathBG.height = deathBG.width * ratio;
		deathBG.y += 75;
		deathBG.x -= 15;
		sceneContainer.AddChild(deathBG);

		DGPlayer.players.Add(new DGPlayer());
		DGPlayer.players.Add(new DGPlayer());
		DGPlayer.players.Add(new DGPlayer());

		CreateOrgans();
		CreateBorderColliders();

		int playerCount = DGPlayer.players.Count;

		sceneContainer.AddChild(playableContainer);

		float labelMargin = 30;

		for (int i = 0; i < playerCount; i++) {
			DGCell cell;

			cell = new DGCell("player " + (i + 1), DGPlayer.players[i]);

			cell.sprite.color = DGConfig.colors[i];
			playableContainer.AddChild(cell);
			cells.Add(cell);

			FLabel l = new FLabel("Franchise", "0.00");
			sceneContainer.AddChild(l);
			l.color = DGConfig.colors[i];
			l.x = labelMargin + ((Futile.screen.width - labelMargin * 2) / (playerCount + 1)) * (i + 1) - Futile.screen.halfWidth;
			l.y = -145;
			l.scale = 0.5f;
			labels.Add(l);
		}

		float yVal = -100;

		if (cells.Count == 1) {
			cells[0].x = 0;
			cells[0].y = yVal;
		}
		else if (cells.Count == 2) {
			cells[0].x = -50;
			cells[1].x = 50;

			foreach (DGCell cell in cells) {
				cell.y = yVal;
			}
		}
		else if (cells.Count == 3) {
			cells[0].x = -50;
			cells[2].x = 50;

			foreach (DGCell cell in cells) {
				cell.y = yVal;
			}

			cells[0].y += 20;
			cells[2].y += 20;
		}
		else if (cells.Count == 4) {
			cells[0].x = -60;
			cells[1].x = -21;
			cells[2].x = 21;
			cells[3].x = 60;

			foreach (DGCell cell in cells) {
				cell.y = yVal;
			}

			cells[0].y += 15;
			cells[3].y += 15;
		}

		sceneContainer.scale = 1.1f;
		AddChild(sceneContainer);

		ListenForUpdate(HandleUpdate);
	}

	public void HandleUpdate() {
		for (int i = 0; i < DGPlayer.players.Count; i++) {
			labels[i].text = heart.infectionAmounts[i].ToString("0.00");
		}
	}

	public void CreateBorderColliders() {
		float xMax = 95;
		float yMax = 135;

		WTPhysicsNode wall1 = new WTPhysicsNode("wall1");
//		FSprite wall1sprite = new FSprite("WhiteBox");
//		wall1sprite.width = 10;
//		wall1sprite.height = 280;
//		wall1.AddChild(wall1sprite);
		wall1.physicsComponent.AddRigidBody(0f, 0f, 1000f);
		wall1.physicsComponent.AddBoxCollider(new Vector2(10, 280));
		wall1.physicsComponent.SetupPhysicMaterial(1f, 0.0f, 0.0f);
		wall1.x -= xMax;
		playableContainer.AddChild(wall1);

		WTPhysicsNode wall2 = new WTPhysicsNode("wall2");
//		FSprite wall2sprite = new FSprite("WhiteBox");
//		wall2sprite.width = 10;
//		wall2sprite.height = 280;
//		wall2.AddChild(wall2sprite);
		wall2.physicsComponent.AddRigidBody(0f, 0f, 1000f);
		wall2.physicsComponent.AddBoxCollider(new Vector2(10, 280));
		wall2.physicsComponent.SetupPhysicMaterial(1f, 0.0f, 0.0f);
		wall2.x += xMax;
		playableContainer.AddChild(wall2);

		WTPhysicsNode wall3 = new WTPhysicsNode("wall3");
//		FSprite wall3sprite = new FSprite("WhiteBox");
//		wall3sprite.width = 200;
//		wall3sprite.height = 10;
//		wall3.AddChild(wall3sprite);
		wall3.physicsComponent.AddRigidBody(0f, 0f, 1000f);
		wall3.physicsComponent.AddBoxCollider(new Vector2(200, 10));
		wall3.physicsComponent.SetupPhysicMaterial(1f, 0.0f, 0.0f);
		wall3.y -= yMax;
		playableContainer.AddChild(wall3);

		WTPhysicsNode wall4 = new WTPhysicsNode("wall4");
//		FSprite wall4sprite = new FSprite("WhiteBox");
//		wall4sprite.width = 200;
//		wall4sprite.height = 10;
//		wall4.AddChild(wall4sprite);
		wall4.physicsComponent.AddRigidBody(0f, 0f, 1000f);
		wall4.physicsComponent.AddBoxCollider(new Vector2(200, 10));
		wall4.physicsComponent.SetupPhysicMaterial(1f, 0.0f, 0.0f);
		wall4.y += yMax;
		playableContainer.AddChild(wall4);
	}

	public void CreateOrgans() {
		FSprite playableBG = new FSprite("playable");
		playableBG.alpha = 0.9f;
		playableContainer.AddChild(playableBG);

		FSprite stomach = new FSprite("stomach");
		stomach.x = 15;
		stomach.y = 10;
		playableContainer.AddChild(stomach);

		FSprite intestines = new FSprite("intestines");
		intestines.y = -75;
		playableContainer.AddChild(intestines);

		FSprite liver = new FSprite("liver");
		liver.x = -20;
		liver.y = 10;
		playableContainer.AddChild(liver);

		FSprite lungR = new FSprite("lungR");
		lungR.x = -35;
		lungR.y = 75;
		playableContainer.AddChild(lungR);

		FSprite lungL = new FSprite("lungL");
		lungL.x = 35;
		lungL.y = 75;
		playableContainer.AddChild(lungL);

		heart = new DGSpecialOrgan("heart", OrganType.Heart, DGPlayer.players.Count);
		heart.SignalOrganIsFullyInfected += HandleOrganIsFullyInfected;
		heart.x = 5;
		heart.y = 65;
		playableContainer.AddChild(heart);

		liver.color = lungL.color = lungR.color = stomach.color = intestines.color = new Color(0.5f, 0.5f, 0.5f);
	}

	// Use this for initialization
	override public void Start () {
		foreach (DGCell p in cells) p.physicsComponent.StartPhysics();
	}
	
	// Update is called once per frame
	override public void Destroy () {
	
	}

	public void HandleOrganIsFullyInfected(DGSpecialOrgan organ) {
		int winningIndex = 0;

		for (int i = 1; i < organ.infectionAmounts.Length; i++) {
			if (organ.infectionAmounts[i] > organ.infectionAmounts[winningIndex]) winningIndex = i;
		}

		DGCell cell = cells[winningIndex];
		playableContainer.AddChild(cell);
		Go.killAllTweensWithTarget(cell);
		Go.to(cell, 0.5f, new TweenConfig().floatProp("scale", 4f));
	}
}
