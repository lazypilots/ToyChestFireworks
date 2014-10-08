using UnityEngine;
using System.Collections;

enum E_Colors
{
	RED,
	ORANGE,
	YELLOW,
	GREEN,
	BLUE,
	PURPLE
}

enum EDU_GAMESTATES
{
	STARTUP,
	PROMPT,
	DISPLAY,
	WAIT,
	CORRECT,
	INCORRECT,
	CLEANUP
}


public class Controller : MonoBehaviour {
	public float Timer;
	public Transform fireWork;
	public float _timer;
	public GameObject projectile;
	public GameObject button;
	public Button[] _buttons = new Button[6];
	public int _color;
	private float _lockTimer;
	private EDU_GAMESTATES _gameState;
	private float _gameStateTimer;
	public float _correctBurstTimer;
	private bool _stateStart;          //Set to true for the first frame of a given state//
	private float _burstRandomTimer;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		_timer = 2.0f;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		Gameoptions.Start();

		_lockTimer = 0;
		_gameStateTimer = 0;
		_gameState = EDU_GAMESTATES.STARTUP;
		_stateStart = true;
		_correctBurstTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		_timer -= Time.deltaTime;

		int i = 0;
		if (Gameoptions.TypeGame == 0)
		{
		while (i < Input.touchCount) 
		{
				if (Input.GetTouch(i).phase == TouchPhase.Began)
				{
				GameObject clone;
				Vector3 Pos = new Vector3( Input.GetTouch(i).position.x / (Screen.width / 14) - 7, Input.GetTouch(i).position.y / (Screen.height / 10 ) - 5, 0 ); 
				clone = Instantiate(projectile, Pos  , transform.rotation) as GameObject;
				Firework script = (Firework)clone.GetComponent("Firework");
				script.color = GetColor();
				}	
				++i;
		}
		}

		if ( Gameoptions.TypeGame == 1)
		{
			UpdateEduGameLogic();
		}

		/*
		if (Gameoptions.TypeGame == 1)
		{
			foreach (Button ob in _buttons)
			{
				if (ob.b_Active)
				{
					if (ob._color == _color)
					{
						_timer = 0;
					}
					ob.b_Active = false;
				}
			}

		}
*/

		if ( Input.GetKey(KeyCode.Escape) == true)
		{
			Debug.Log("Escape Down");

			if (Gameoptions.ParentalLock == 0)
			{
			Application.LoadLevel(0);
			}
			else
			{
				_lockTimer += Time.deltaTime;
				Debug.Log ("Lock Timer Incremented to: " + _lockTimer);
				if (_lockTimer > 4.0)
				{
					//NATETODO  Parental lock code
					Application.LoadLevel(0);
				}
			}
		}
		else
		{
			_lockTimer = 0;
		}


	}

	void UpdateEduGameLogic()
	{
		_gameStateTimer += Time.deltaTime;

		switch (_gameState)
		{
		case EDU_GAMESTATES.STARTUP:
			_gameStateTimer = 0;
			_gameState = EDU_GAMESTATES.PROMPT;
			_stateStart = true;
			break;
		case EDU_GAMESTATES.PROMPT:
			//Audio for Color guess CUE//

			if(_gameStateTimer > 3.0)
			{
				_gameStateTimer = 0;
				_gameState = EDU_GAMESTATES.DISPLAY;
				_stateStart = true;
			}
			break;
		case EDU_GAMESTATES.DISPLAY:
			if (_stateStart)
			{
				Firework script1 = (Firework)fireWork.GetComponent("Firework");
				script1.color = GetColor();
				_gameStateTimer = 0;
				_stateStart = false;
				Vector3 pos1 = new Vector3(Random.Range(0, 45)/5 - 4 , Random.Range(0, 30)/5 - 1 , 0);
				Instantiate(fireWork, pos1, Quaternion.identity);
			}	
			else if (_gameStateTimer >= _burstRandomTimer)
			{
				_gameStateTimer = 0;
				Firework script1 = (Firework)fireWork.GetComponent("Firework");
				script1.color = GetColor(_color);
				Vector3 pos1 = new Vector3(Random.Range(0, 45)/5 - 4 , Random.Range(0, 30)/5 - 1 , 0);
				Instantiate(fireWork, pos1, Quaternion.identity);

				SetRandomTimer(0.5f);
				if (Random.Range(1,3) == 1)
				{
					_gameState = EDU_GAMESTATES.WAIT;
					_stateStart = true;
				}
			}

			break;
		case EDU_GAMESTATES.WAIT:

			if (_gameStateTimer > _burstRandomTimer)
			{
				Firework script = (Firework)fireWork.GetComponent("Firework");
				script.color = GetColor(_color);
				Vector3 pos = new Vector3(Random.Range(0, 45)/5 - 4 , Random.Range(0, 30)/5 - 1 , 0);
				Instantiate(fireWork, pos, Quaternion.identity);
				_gameStateTimer = 0;

				SetRandomTimer(2.5f);
			}

			break;
		case EDU_GAMESTATES.CORRECT:
			//audio correct prompt play//
			if (_stateStart)
			{
				_correctBurstTimer = 0;
				SetRandomTimer(0.70f);
				_stateStart = false;
			}
			_correctBurstTimer += Time.deltaTime;

			if (_correctBurstTimer >= _burstRandomTimer)
			{
				Firework script2 = (Firework)fireWork.GetComponent("Firework");
				script2.color = GetColor();
				Vector3 pos2 = new Vector3(Random.Range(0, 45)/5 - 4 , Random.Range(0, 30)/5 - 1 , 0);
				Instantiate(fireWork, pos2, Quaternion.identity);

				_correctBurstTimer = 0;
				SetRandomTimer(0.70f);
			}

			if (_gameStateTimer > 3.0)
				_gameState = EDU_GAMESTATES.CLEANUP;
			break;
		case EDU_GAMESTATES.INCORRECT:
			//audio incorrect prompt play//

			if (_gameStateTimer > 3.0)
				_gameState = EDU_GAMESTATES.WAIT;
			break;
		case EDU_GAMESTATES.CLEANUP:


			_gameState = EDU_GAMESTATES.STARTUP;
			break;

		}


	}

	void OnGUI () {
		GUIScale.SetGuiMatrix();
		GUI.skin.label.fontSize = 72;
		GUI.skin.button.fontSize = 48;
		GUI.skin.box.fontSize = 36;


		if (Gameoptions.TypeGame == 1)
		{
			for (int i = 0; i < Gameoptions.NumColors; i++)
			{
				if(_gameState != EDU_GAMESTATES.WAIT)
					GUI.enabled = false;
				if(GUI.Button(new Rect((GUIScale.virtualWidth / Gameoptions.NumColors) * i ,
				                       GUIScale.virtualHeight *0.85f, 
				                       (GUIScale.virtualWidth / Gameoptions.NumColors),
				                       GUIScale.virtualHeight *0.15f ), 
				              GetColorString(i)) &&_gameState == EDU_GAMESTATES.WAIT  ) {
					if (_color == i)
					{
						_gameState = EDU_GAMESTATES.CORRECT;
						_gameStateTimer = 0;
						_stateStart = true;
					}
					else
					{
						_gameState = EDU_GAMESTATES.INCORRECT;
						_gameStateTimer = 0;
						_stateStart = true;
					}
				}
			}

			GUI.enabled = true;

			if (_gameState == EDU_GAMESTATES.PROMPT)
			{
				GUI.skin.box.fontSize = 72;
				GUI.Box(new Rect(GUIScale.virtualWidth * 0.2f,GUIScale.virtualHeight * 0.3f ,GUIScale.virtualWidth * 0.6f,86), "Guess the Color!");
			}
			else if (_gameState == EDU_GAMESTATES.CORRECT)
			{
				GUI.skin.box.fontSize = 72;
				GUI.Box(new Rect(GUIScale.virtualWidth * 0.2f,GUIScale.virtualHeight * 0.3f ,GUIScale.virtualWidth * 0.6f,86), "Thats Right!");
			}
			else if (_gameState == EDU_GAMESTATES.INCORRECT)
			{
				GUI.skin.box.fontSize = 72;
				GUI.Box(new Rect(GUIScale.virtualWidth * 0.2f,GUIScale.virtualHeight * 0.3f ,GUIScale.virtualWidth * 0.6f,86), "Not Quite! Guess again!");
			}
		}


	}

	public void SetRandomTimer(float i)
	{
		_burstRandomTimer = (((float)Random.Range(0, 40)) / 40.0f) * i;
	}

	public Color GetColor()
	{
		int i = Random.Range (0, Gameoptions.NumColors);
		_color = i;
		switch(i)
		{
		case (0):
			return Color.red;
		case(5):
			return new Color(1.0f, 0.45f, 0.0f, 1f);	//Orange

		case(3):
			return Color.yellow;
		case(2):
			return Color.green;
		case(1):
			return Color.blue;
		case(4):
			return new Color(1f, 0, 1f, 1f); //Purple
		default:
			return Color.white;
		};
	}

	public Color GetColor(int i)
	{
		switch(i)
		{
		case (0):
			return Color.red;
		case(5):
			return new Color(1.0f, 0.45f, 0.0f, 1f);	//Orange		
		case(3):
			return Color.yellow;
		case(2):
			return Color.green;
		case(1):
			return Color.blue;
		case(4):
			return new Color(1f, 0, 1f, 1f); //Purple
		default:
			return Color.white;
		};
	}

	public string GetColorString(int i)
	{
		switch(i)
		{
		case (0):
			return "Red";
		case(5):
			return "Orange";	//Orange
		case(3):
			return "Yellow";
		case(2):
			return "Green";
		case(1):
			return "Blue";
		case(4):
			return "Purple"; //Purple
		default:
			return "Error";
		};
	}


}
