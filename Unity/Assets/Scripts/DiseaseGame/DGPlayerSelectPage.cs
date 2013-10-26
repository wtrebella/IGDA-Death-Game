using UnityEngine;
using System.Collections;

public class DGPlayerSelectPage : AbstractPage {
	FLabel[] playerLabels = new FLabel[4];
	float margin = 50;
	float leftMargin = 20;
	float workableArea;
	float distPerLabel;
	int playerCount = 0;

	Color [] colors = new Color[] {
		Color.blue,
		Color.red,
		Color.green,
		Color.yellow
	};

	string[] names = new string[] {
		"Poopy Disease",
		"Skin Eater",
		"Decaying Problem",
		"Gooey Lewis and the Flus"
	};

	public DGPlayerSelectPage() {
		for (int i = 0; i < 4; i++) {
			playerLabels[i] = new FLabel("Franchise", "");
			playerLabels[i].anchorX = 0;
			playerLabels[i].scale = 0.75f;
			playerLabels[i].color = colors[i];
			playerLabels[i].x = leftMargin;
			playerLabels[i].isVisible = false;
			AddChild(playerLabels[i]);
		}

		UpdateLabels();

		ListenForUpdate(HandleUpdate);
	}

	public void UpdateLabels() {
		workableArea = Futile.screen.height - margin * 2;
		distPerLabel = workableArea / (playerCount + 1);

		for (int i = 0; i < DGPlayer.players.Count; i++) {
			playerLabels[i].text = "Player " + (i + 1) + ": " + names[i];
			playerLabels[i].y = margin + (distPerLabel * (DGPlayer.players.Count - i));
			playerLabels[i].isVisible = true;
		}

		for (int i = DGPlayer.players.Count; i < 4; i++) {
			playerLabels[i].isVisible = false;
		}
	}

	public void UpdatePlayers() {
		foreach (Gamepad g in GamepadManager.instance.gamepads) {
			bool gamepadIsPressingSelectButton = DGInput.GetXboxButtonDown(g, XboxButtonType.A) || DGInput.GetPS3ButtonDown(g, PS3ButtonType.X);
			bool gamepadIsPressingRemoveButton = DGInput.GetXboxButtonDown(g, XboxButtonType.B) || DGInput.GetPS3ButtonDown(g, PS3ButtonType.Circle);

			if (gamepadIsPressingRemoveButton) {
				DGPlayer playerToRemove = null;

				foreach (DGPlayer p in DGPlayer.players) {
					if (p.gamepad == g) {
						playerToRemove = p;
						break;
					}			
				}

				if (playerToRemove != null) {
					DGPlayer.players.Remove(playerToRemove);
				}
			}

			if (gamepadIsPressingSelectButton) {
				bool gamepadIsFree = true;

				foreach (DGPlayer p in DGPlayer.players) {
					if (p.gamepad == g) {
						gamepadIsFree = false;
						break;
					}
				}

				if (!gamepadIsFree) continue;

				bool needNewPlayer = true;

				foreach (DGPlayer p in DGPlayer.players) {
					if (p.gamepad == null) {
						needNewPlayer = false;
						p.gamepad = g;
						break;
					}
				}

				if (needNewPlayer) {
					DGPlayer player = new DGPlayer();
					player.gamepad = g;
					DGPlayer.players.Add(player);
					player.diseaseName = names[DGPlayer.players.Count - 1];
				}
			}
		}
	}

	public void HandleUpdate() {
		UpdatePlayers();

		if (DGPlayer.players.Count != playerCount) {
			playerCount = DGPlayer.players.Count;
			UpdateLabels();
		}

		foreach (DGPlayer p in DGPlayer.players) {
			bool startButtonDown = DGInput.GetPS3Button(p.gamepad, PS3ButtonType.Start) || DGInput.GetXboxButton(p.gamepad, XboxButtonType.Start);

			if (startButtonDown) {
				DGMain.instance.GoToPage(DGPageType.DGGamePage);
			}
		}
	}
}
