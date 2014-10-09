using UnityEngine;
using System.Collections;

public class GameSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update () {

	}


	void OnGUI()
	{


		GUIScale.SetGuiMatrix();
		GUI.skin.label.fontSize = 36;
		GUI.skin.button.fontSize = 48;
		GUI.skin.box.fontSize = 64;

		GUI.Box(new Rect(GUIScale.virtualWidth*.10f,GUIScale.virtualHeight*.10f,GUIScale.virtualWidth*.80f,GUIScale.virtualHeight*.82f), "Select Gametype");

		if(GUI.Button(new Rect(GUIScale.virtualWidth*.20f,GUIScale.virtualHeight*.30f,GUIScale.virtualWidth*.60f,GUIScale.virtualHeight*.10f), "Free Play")) {
			Gameoptions.TypeGame = 0;
			Gameoptions.SaveOptions();
			Application.LoadLevel(1);
		}

		if (GUI.Button (new Rect (GUIScale.virtualWidth * .20f, GUIScale.virtualHeight * .45f, GUIScale.virtualWidth * .60f, GUIScale.virtualHeight * .10f), "Colors")) {
			Gameoptions.TypeGame = 1;
			Gameoptions.SaveOptions();	
			Gameoptions.LoadOptions();
			Application.LoadLevel (1);
				}

		if (GUI.Button (new Rect (GUIScale.virtualWidth * .20f, GUIScale.virtualHeight * .60f, GUIScale.virtualWidth * .60f, GUIScale.virtualHeight * .10f), "Cinema")) {
			Gameoptions.TypeGame = 2;
			Gameoptions.SaveOptions();	
			Application.LoadLevel (1);
		}

		if (GUI.Button (new Rect (GUIScale.virtualWidth * .20f, GUIScale.virtualHeight * .75f, GUIScale.virtualWidth * .60f, GUIScale.virtualHeight * .10f), "Counting")) {
			Gameoptions.TypeGame = 3;
			Gameoptions.SaveOptions();	
			Application.LoadLevel (1);
		}
	}
}
