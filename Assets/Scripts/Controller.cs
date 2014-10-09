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
	public int countTarget, countCurrent;
	public int [] _countList;
	private float _lockTimer;
	private EDU_GAMESTATES _gameState;
	private float _gameStateTimer;
	public float _correctBurstTimer;
	private bool _stateStart;          //Set to true for the first frame of a given state//
	private float _burstRandomTimer;
	public float MaxTimer;
	public int _burstSize;
	public int _burstCounter;
	public bool _bInBurst;
	public float _burstTimer;
	public float _burstDelay;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		_timer = 0.0f;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		Gameoptions.Start();
		_countList = new int [6];
		_lockTimer = 0;
		_gameStateTimer = 0;
		_gameState = EDU_GAMESTATES.STARTUP;
		_stateStart = true;
		_correctBurstTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		_timer += Time.deltaTime;

	

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

		if (Gameoptions.TypeGame == 2) {
			if(_timer >= MaxTimer)
			{
				_bInBurst = true;
				_burstSize = Random.Range (1, 6);
				_timer = (Random.Range(10, (MaxTimer * 10)) / 10.0f) - 1;
				
			}
			
			if (_bInBurst)
			{
				_burstTimer += Time.deltaTime;
				
				
				
				if (_burstTimer >= _burstDelay)
				{
					//new firework//
					Firework script1 = (Firework)fireWork.GetComponent("Firework");
					script1.color = GetColor();
					Vector3 pos1 = new Vector3(Random.Range(0, 45)/5 - 4 , Random.Range(0, 30)/5 - 1 , 0);
					Instantiate(fireWork, pos1, Quaternion.identity);
					
					_burstCounter ++;
					_burstTimer = 0;
					_burstDelay = ((float)Random.Range(0,20.0f)) / 20.0f;
				}
				
				if (_burstCounter == _burstSize)
				{
					_burstCounter = 0;
					_bInBurst = false;
				}
			}


				}

		if ( Gameoptions.TypeGame == 1)
		{
			UpdateEduGameLogic();
		}

		if ( Gameoptions.TypeGame == 3)
		{
			UpdateCountGameLogic();
		}
		

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
	//Having two game type updates is totally unnecesary, but I wrote the original code so long ago
	//I forget how it works to modify it for counting game.
	void UpdateCountGameLogic()
	{
		_gameStateTimer += Time.deltaTime;

		switch (_gameState) {
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
				//Firework script1 = (Firework)fireWork.GetComponent("Firework");
				//script1.color = GetColor();
				_gameStateTimer = 3.0f;
				countCurrent = 0;
				_stateStart = false;
				countTarget = Random.Range(1,9);

			}	
			if (_gameStateTimer > 2.5){
				Firework script1 = (Firework)fireWork.GetComponent("Firework");
				script1.color = GetColor();
				Vector3 pos1 = new Vector3(Random.Range(0, 45)/5 - 4 , Random.Range(0, 30)/5 - 1 , 0);
				Instantiate(fireWork, pos1, Quaternion.identity);
				countCurrent++;
				_gameStateTimer = Random.Range(0, 1.0f);
			}

			if (countCurrent == countTarget)
			{
				_gameState = EDU_GAMESTATES.WAIT;
				_stateStart = true;
				GenerateCountList();
			}

			break;

		case EDU_GAMESTATES.WAIT:

			break;
			//If no correct state set to startup//

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
				_gameState = EDU_GAMESTATES.CLEANUP;
			break;
		case EDU_GAMESTATES.CLEANUP:
			
			_gameState = EDU_GAMESTATES.STARTUP;
			break;


		default:
			_gameState = EDU_GAMESTATES.STARTUP;
			break;
		};


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

		if (Gameoptions.TypeGame == 3)
		{
			for (int i = 0; i < _countList.Length; i++)
			{
				if(_gameState != EDU_GAMESTATES.WAIT)
				{
					GUI.enabled = false;
				}
				if(GUI.Button(new Rect((GUIScale.virtualWidth / _countList.Length) * i ,
				                       GUIScale.virtualHeight *0.85f, 
				                       (GUIScale.virtualWidth / _countList.Length),
				                       GUIScale.virtualHeight *0.15f ), 
				              GetArrayString(i)) &&_gameState == EDU_GAMESTATES.WAIT  ) {
					if (_countList[i] == countTarget)
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
				GUI.Box(new Rect(GUIScale.virtualWidth * 0.2f,GUIScale.virtualHeight * 0.3f ,GUIScale.virtualWidth * 0.6f,86), "Count the Fireworks!");
			}
			else if (_gameState == EDU_GAMESTATES.CORRECT)
			{
				GUI.skin.box.fontSize = 72;
				GUI.Box(new Rect(GUIScale.virtualWidth * 0.2f,GUIScale.virtualHeight * 0.3f ,GUIScale.virtualWidth * 0.6f,86), "Thats Right!");
			}
			else if (_gameState == EDU_GAMESTATES.INCORRECT)
			{
				GUI.skin.box.fontSize = 72;
				GUI.Box(new Rect(GUIScale.virtualWidth * 0.2f,GUIScale.virtualHeight * 0.3f ,GUIScale.virtualWidth * 0.6f,86), "Not Quite!");
			}
		}

	}

	private string GetArrayString(int i)
	{
		string s;
		if (_gameState == EDU_GAMESTATES.WAIT) 
		{
			switch (_countList[i])
			{
			case 1:
				return ("One\n(1)");
				break;
			case 2:
				return ("Two\n(2)");
				break;
			case 3:
				return ("Three\n(3)");
				break;
			case 4:
				return ("Four\n(4)");
				break;
			case 5:
				return ("Five\n(5)");
				break;
			case 6:
				return ("Six\n(6)");
			break;			
			case 7:
				return ("Seven\n(7)");
				break;
			case 8:
				return ("Eight\n(8)");
				break;
			case 9:
				return ("Nine\n(9)");
				break;
			default:
				return (" ");
				break;
			};
		} 

		else 
		{
						return (" ");
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

	public void GenerateCountList()
	{
		_countList [0] = countTarget;
		for (int i = 1; i < 6; i++) 
		{
			int k = 0;
			bool b_uniq = true;
			while(b_uniq)
			{
				k = Random.Range(1, 9);
				b_uniq = false;
				for (int j = 0; j < i; j++)
				{

				if (k == _countList[j])
					{
						b_uniq = true;
					}
					Debug.Log( k + " equals " + _countList[j] + " is " + b_uniq);
				}
			}
			_countList[i] = k;
		}

		for (int i = _countList.Length; i>0; i--) 
		{
			int r = Random.Range(0, i);
			int l = _countList[r];
			_countList[r] = _countList[i - 1];
			_countList[i - 1] = l;
		}
	}
}
