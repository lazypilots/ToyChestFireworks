using UnityEngine;
using System.Collections;

public class Gametype : MonoBehaviour {

	int score;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void AddScore(int i)
	{
		score += i;
	}
}
