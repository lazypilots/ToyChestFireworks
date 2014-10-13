using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

	private string _typeText, _repeatText, _colorsText, _lockText, _detailText;

	public AudioClip testAudioClip;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {

		GUIScale.SetGuiMatrix();
		GUI.skin.label.fontSize = 36;
		GUI.skin.button.fontSize = 48;
		GUI.skin.box.fontSize = 64;

		if (Gameoptions.TypeGame == 0)
			_typeText = "Gametype: Free Play";
		else if (Gameoptions.TypeGame == 1)
			_typeText = "Gametype: Colors";
		else if (Gameoptions.TypeGame == 2)
			_typeText = "Gametype: Cinema";
		else if (Gameoptions.TypeGame == 3)
			_typeText = "Gametype: Count";

		if (Gameoptions.Repeat == 0)
			_repeatText = "Repeat: Off";
		else
			_repeatText = "Repeat: On";

		_colorsText = "Number Colors: " + Gameoptions.NumColors.ToString();

		if (Gameoptions.ParentalLock == 0)
			_lockText = "Parent Lock: Off";
		else
			_lockText = "Parent Lock: On";

		_detailText = "Detail: " + Gameoptions.FireworkDetail.ToString();


		GUI.Box(new Rect(GUIScale.virtualWidth*.03f,GUIScale.virtualHeight*.10f,GUIScale.virtualWidth*.94f,GUIScale.virtualHeight*.85f), "Game Options");


	
		if(GUI.Button(new Rect(GUIScale.virtualWidth*.08f,GUIScale.virtualHeight*.20f,GUIScale.virtualWidth*.37f,GUIScale.virtualHeight*.10f), "Test")) {
			testAudioClip = Microphone.Start ( null, false, 4, 44100 );
		}

		if(GUI.Button(new Rect(GUIScale.virtualWidth*.55f,GUIScale.virtualHeight*.20f,GUIScale.virtualWidth*.37f,GUIScale.virtualHeight*.10f), "Save")) {


			SavWav.Save("myfile", testAudioClip);
		}


		if(GUI.Button(new Rect(GUIScale.virtualWidth*.08f,GUIScale.virtualHeight*.40f,GUIScale.virtualWidth*.37f,GUIScale.virtualHeight*.10f), "Play")) {
			this.audio.clip = testAudioClip;
			audio.Play();
		}

		if(GUI.Button(new Rect(GUIScale.virtualWidth*.55f,GUIScale.virtualHeight*.40f,GUIScale.virtualWidth*.37f,GUIScale.virtualHeight*.10f), _lockText)) {
			Gameoptions.IncrementParentalLock();
		}

		if(GUI.Button(new Rect(GUIScale.virtualWidth*.55f,GUIScale.virtualHeight*.60f,GUIScale.virtualWidth*.37f,GUIScale.virtualHeight*.10f), _detailText)) {
			Gameoptions.IncrementFireworkDetail();
		}

		
		if(GUI.Button(new Rect(GUIScale.virtualWidth*.20f,GUIScale.virtualHeight*.80f,GUIScale.virtualWidth*.60f,GUIScale.virtualHeight*.10f), "Back")) {
			Gameoptions.SaveOptions();
			Application.LoadLevel(0);
		}

		//GUI.Label(new Rect(5, 5, 100, 100), ChildLock.isEnabled().ToString());
	}
}
