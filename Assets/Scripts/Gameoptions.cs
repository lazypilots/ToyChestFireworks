using UnityEngine;
using System.Collections;

/*	PlayerPrefs values are Float, Int, and String
 * 
 *  Gametype INTS are defined as:
 *  0 = Free Play, fireworks as desired
 *  1 = Color Ident, 
 *  2 = Cinema
 *  3 = Counting
 */ 

public class Gameoptions{
	public static int Repeat;
	public static int TypeGame;
	public static int NumColors;
	public static int ParentalLock;
	public static int FireworkDetail;



	public static void Start()
	{
		if (!LoadOptions())
		{
			DefaultOptions();
		}

	}

	public static void IncrementRepeat()
	{
		if (Repeat == 1)
			Repeat = 0;
		else
			Repeat = 1;
	}

	public static void IncrementTypeGame()
	{
		TypeGame++;
		if (TypeGame > 3)
			TypeGame = 0;
	}

	public static void IncrementNumColors()
	{
		NumColors++;
		if (NumColors > 6)
			NumColors = 2;
	}

	public static void IncrementParentalLock()
	{
		if (ParentalLock == 1)
		{
			ParentalLock = 0;
			//ChildLock.Disable();
		}
		else
		{
			ParentalLock = 1;
			//ChildLock.Enable();
		}
	}

	public static void IncrementFireworkDetail()
	{
		FireworkDetail++;
		if (FireworkDetail > 5)
		{
			FireworkDetail = 1;
		}

		Debug.Log("Firework Detail: " + FireworkDetail);
	}

	public static bool DefaultOptions()
	{
		Repeat = 1;
		TypeGame = 0;
		NumColors = 6;
		ParentalLock = 0;
		FireworkDetail = 3;
		Debug.Log("Options set to default");
		return true;
	}

	public static bool LoadOptions()
	{
		Repeat = PlayerPrefs.GetInt("Repeat", -1);
		TypeGame = PlayerPrefs.GetInt("TypeGame", -1);
		NumColors = PlayerPrefs.GetInt("NumColors", -1);
		ParentalLock = PlayerPrefs.GetInt("ParentalLock", -1);
		FireworkDetail = PlayerPrefs.GetInt("FireworkDetail", -1);

		if (Repeat == -1 || TypeGame == -1 || NumColors == -1 || ParentalLock == -1 || FireworkDetail == -1)
		{
			Debug.Log("Option load failed");
			return false;
		}

		return true;
	}

	public static bool SaveOptions()
	{
		PlayerPrefs.SetInt("Repeat", Repeat);
		PlayerPrefs.SetInt("TypeGame", TypeGame);
		PlayerPrefs.SetInt("NumColors", NumColors);
		PlayerPrefs.SetInt("ParentalLock", ParentalLock);
		PlayerPrefs.SetInt("FireworkDetail", FireworkDetail);
		Debug.Log("Options saved");
		return true;
	}
}
