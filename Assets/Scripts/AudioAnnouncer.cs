using UnityEngine;
using System.Collections;

public class AudioAnnouncer : MonoBehaviour {
	/*
	 * 
ooooh
aaaah
Guess the Color!
Ready, go!
Red
Orange
Yellow
Green
Blue
Purple
Count the Fireworks!
One
Two
Three
Four
Five
Six
Seven
Eight
Nine
Fireworks
That's right!
Not quite!
Guess Again!

*/



	public AudioClip a_comment1, a_comment2, a_guessColor, a_guessCount, a_readyGo, a_red, 
				a_orange, a_yellow, a_green, a_blue, a_purple, a_one, a_two, a_three,
				a_four, a_five, a_six, a_seven, a_eight, a_nine, a_fireworks, a_correct,
				a_incorrect, a_retry;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Correct()
	{
		audio.clip = a_correct;
		audio.Play ();
	}
}
