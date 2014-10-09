using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {



	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Gameoptions.Start();
		Debug.Log("Startup Complete");
		GUIScale.SetAspectRatio();
		GUIScale.SetGuiMatrix();

					}			

		void OnGUI () {
		GUIScale.SetGuiMatrix();
		// Make a background box
		/*
		switch (GUIScale.aspect_ratio)
		{
		case ASPECT_RATIO.SIXTEENTONINE:

*/
		GUI.skin.label.fontSize = 36;
		GUI.skin.button.fontSize = 48;
		GUI.skin.box.fontSize = 64;
		GUI.Box(new Rect(GUIScale.virtualWidth*.10f,GUIScale.virtualHeight*.10f,GUIScale.virtualWidth*.80f,GUIScale.virtualHeight*.62f), "Welcome to Toy Chest: Fireworks");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(GUIScale.virtualWidth*.20f,GUIScale.virtualHeight*.30f,GUIScale.virtualWidth*.60f,GUIScale.virtualHeight*.10f), "Start")) {
			Application.LoadLevel(3);
		}
		
		// Make the second button.
		if(GUI.Button(new Rect(GUIScale.virtualWidth*.20f,GUIScale.virtualHeight*.45f,GUIScale.virtualWidth*.60f,GUIScale.virtualHeight*.10f), "Options")) {
			Application.LoadLevel(2);
		}

		if(GUI.Button(new Rect(GUIScale.virtualWidth*.20f,GUIScale.virtualHeight*.60f,GUIScale.virtualWidth*.60f,GUIScale.virtualHeight*.10f), "Exit")) {

			Application.Quit();
		}
		/*
			break;
		}
		*/
	}
}
