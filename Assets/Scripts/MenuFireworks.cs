using UnityEngine;
using System.Collections;

public class MenuFireworks : MonoBehaviour {

	public float MaxTimer;
	public Transform fireWork;
	public float _timer;
	public int _burstSize;
	public int _burstCounter;
	public bool _bInBurst;
	public float _burstTimer;
	public float _burstDelay;

	// Use this for initialization
	void Start () {
		//_timer = (Random.Range(10, (MaxTimer * 10)) / 10.0f) - 1;
	}
	
	// Update is called once per frame
	void Update () {
	
		_timer += Time.deltaTime;

		if(_timer >= MaxTimer)
		{
			_bInBurst = true;
			_burstSize = Random.Range (1, 4);
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

	public Color GetColor()
	{
		int i = Random.Range (0, Gameoptions.NumColors);
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
}
