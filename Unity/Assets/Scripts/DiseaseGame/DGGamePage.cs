using UnityEngine;
using System.Collections;

public class GamePage : AbstractPage {
	public GamePage() {
		FSprite deathBG = new FSprite("death_bg");
		deathBG.SetPosition(Futile.screen.halfWidth, Futile.screen.halfHeight);
		float ratio = deathBG.height / deathBG.width;
		deathBG.height = Futile.screen.width;
		deathBG.width = deathBG.height / ratio;
		deathBG.rotation = 90;
		deathBG.scale *= 1.3f;
		deathBG.x += 15;
		AddChild(deathBG);

		FSprite heart = new FSprite("heart");
		heart.SetPosition(Futile.screen.halfWidth, Futile.screen.halfHeight);
		heart.rotation = 90;
		AddChild(heart);


	}

	// Use this for initialization
	override public void Start () {
	
	}
	
	// Update is called once per frame
	override public void Destroy () {
	
	}
}
