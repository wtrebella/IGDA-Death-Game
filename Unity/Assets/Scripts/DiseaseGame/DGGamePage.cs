using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DGGamePage : AbstractPage {
	List<DGPlayer> players = new List<DGPlayer>();

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

		DGPlayer player = new DGPlayer("player");
		player.x = Futile.screen.halfWidth;
		player.y = 100;
		AddChild(player);
		players.Add(player);
	}

	// Use this for initialization
	override public void Start () {
		foreach (DGPlayer p in players) p.physicsComponent.StartPhysics();
	}
	
	// Update is called once per frame
	override public void Destroy () {
	
	}
}
