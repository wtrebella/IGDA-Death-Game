using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DGGamePage : AbstractPage {
	List<DGCell> cells = new List<DGCell>();

	public DGGamePage() {
		FSprite deathBG = new FSprite("death_bg");
		deathBG.SetPosition(Futile.screen.halfWidth, Futile.screen.halfHeight);
		float ratio = deathBG.height / deathBG.width;
		deathBG.width = Futile.screen.width;
		deathBG.height = deathBG.width * ratio;
		deathBG.scale *= 2f;
		deathBG.x -= 30;
		AddChild(deathBG);

		FSprite heart = new FSprite("heart");
		heart.SetPosition(Futile.screen.halfWidth, Futile.screen.halfHeight);
		heart.rotation = 90;
		AddChild(heart);

		for (int i = 0; i < DGPlayer.players.Count; i++) {
			DGCell cell = new DGCell("player " + (i + 1), DGPlayer.players[i]);
			cell.x = Futile.screen.halfWidth;
			cell.y = 100;
			AddChild(cell);
			cells.Add(cell);
		}
	}

	// Use this for initialization
	override public void Start () {
		foreach (DGCell p in cells) p.physicsComponent.StartPhysics();
	}
	
	// Update is called once per frame
	override public void Destroy () {
	
	}
}
