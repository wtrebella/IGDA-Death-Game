using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DGPageType
{
	None = -1,
	DGGamePage = 0,
	DGPlayerSelectPage,
	Max
};

public class DGMain : MonoBehaviour 
{
	public static DGMain instance = null;
	
	private DGPageType _currentPageType = DGPageType.None;
	private AbstractPage _currentPage = null;

	// Use this for initialization
	void Start () 
	{
		instance = this;

		Go.defaultEaseType = EaseType.Linear;
		Go.duplicatePropertyRule = DuplicatePropertyRuleType.RemoveRunningProperty;
		
		bool landscape = true;
		bool portrait = false;
		
		bool isIPad = SystemInfo.deviceModel.Contains("iPad");
		bool shouldSupportPortraitUpsideDown = isIPad && portrait; //only support portrait upside-down on iPad
		
		FutileParams fparams = new FutileParams(landscape, landscape, portrait, shouldSupportPortraitUpsideDown);
		
		fparams.backgroundColor = Color.black;
		
		fparams.AddResolutionLevel(480.0f,	1.0f,	1.0f,	"_Scale1"); //iPhone
		fparams.AddResolutionLevel(960.0f,	2.0f,	2.0f,	"_Scale2"); //iPhone retina
		fparams.AddResolutionLevel(1024.0f,	2.0f,	2.0f,	"_Scale2"); //iPad
		fparams.AddResolutionLevel(1280.0f,	2.0f,	2.0f,	"_Scale2"); //Nexus 7
		fparams.AddResolutionLevel(2048.0f,	4.0f,	4.0f,	"_Scale4"); //iPad Retina
		
		fparams.origin = Vector2.zero;
		
		Futile.instance.Init (fparams);
		
		Futile.atlasManager.LoadAtlas("Atlases/UIFonts");
		Futile.atlasManager.LoadAtlas("Atlases/GameAtlas");

		FTextParams textParams;

		GamepadManager.Init();
		GamepadManager.instance.SignalGamepadsChanged += HandleGamepadsChanged;
		FPWorld.Create(64.0f);

		DGConfig.colors = new Color[] {
			RXColor.GetColorFromHex(0x4873ff), // blue
			RXColor.GetColorFromHex(0x65f327), // green
			RXColor.GetColorFromHex(0xfdff50), // yellow
			RXColor.GetColorFromHex(0xee45e0)  // magenta
		};

		textParams = new FTextParams();
		textParams.lineHeightOffset = -8.0f;
		Futile.atlasManager.LoadFont("Franchise","FranchiseFont"+Futile.resourceSuffix, "Atlases/FranchiseFont"+Futile.resourceSuffix, -2.0f,-5.0f,textParams);
		
		textParams = new FTextParams();
		textParams.kerningOffset = -0.5f;
		textParams.lineHeightOffset = -8.0f;
		Futile.atlasManager.LoadFont("CubanoInnerShadow","Cubano_InnerShadow"+Futile.resourceSuffix, "Atlases/CubanoInnerShadow"+Futile.resourceSuffix, 0.0f,2.0f,textParams);

		GoToPage(DGPageType.DGPlayerSelectPage);
	}

	public void GoToPage (DGPageType pageType)
	{
		if(_currentPageType == pageType) return; //we're already on the same page, so don't bother doing anything
		
		AbstractPage pageToCreate = null;
		
		switch (pageType)
		{
		case DGPageType.DGGamePage:
			pageToCreate = new DGGamePage();
			break;
		case DGPageType.DGPlayerSelectPage:
			pageToCreate = new DGPlayerSelectPage();
			break;
		}
		
		if(pageToCreate != null) //destroy the old page and create a new one
		{
			_currentPageType = pageType;	
			
			if(_currentPage != null)
			{
				_currentPage.Destroy();
				Futile.stage.RemoveChild(_currentPage);
			}
			
			_currentPage = pageToCreate;
			Futile.stage.AddChild(_currentPage);
			if (pageType == DGPageType.DGGamePage) _currentPage.ZoomThenStart();
			else _currentPage.Start();
		}
		
	}

	public static bool IsWindows() {
		return Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer;
	}

	void Update() {
		GamepadManager.instance.Update();
	}

	public void HandleGamepadsChanged() {
		foreach (DGPlayer p in DGPlayer.players) {
			bool foundGamepad = false;

			foreach (Gamepad g in GamepadManager.instance.gamepads) {
				if (g == p.gamepad) {
					foundGamepad = true;
					break;
				}
			}

			if (!foundGamepad) p.gamepad = null;
		}
	}
}
