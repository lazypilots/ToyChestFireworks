using UnityEngine;
using System.Collections;

public enum ASPECT_RATIO{
	SIXTEENTONINE,
	SIXTEENTOTEN,
	THREETOTWO,
	FOURTOTHREE
}

/*  Baseline Resolutions for each aspect ratio
 *  16:9 = 1280 x 720
 *  16:10 = 1280 x 800
 *  3:2 = 1440 x 960
 *  4:3 = 1280 x 960
 * 
 */

public static class GUIScale {
	public static ASPECT_RATIO aspect_ratio;

	public static float virtualWidth = 1280.0f;
	public static float virtualHeight = 800.0f;
	public static Matrix4x4 GUI_Matrix;

	// Use this for initialization
	public static void SetAspectRatio()
	{
		if (Camera.main.aspect >= 1.7)
		{
			aspect_ratio = ASPECT_RATIO.SIXTEENTONINE;
		}
		else if (Camera.main.aspect >= 1.58)
		{
			aspect_ratio = ASPECT_RATIO.SIXTEENTOTEN;
		}
		else if (Camera.main.aspect >= 1.5)
		{
			aspect_ratio = ASPECT_RATIO.THREETOTWO;
		}
		else
		{
			aspect_ratio = ASPECT_RATIO.FOURTOTHREE;
		}
	}
	
	public static void SetGuiMatrix()
	{
		switch (aspect_ratio)
		{
		case (ASPECT_RATIO.SIXTEENTONINE):
			virtualWidth = 1280.0f;
			virtualHeight = 720.0f;
			break;
		case ASPECT_RATIO.SIXTEENTOTEN:
			virtualWidth = 1280.0f;
			virtualHeight = 800.0f;
			break;
		case ASPECT_RATIO.THREETOTWO:
			virtualWidth = 1440.0f;
			virtualHeight = 960.0f;
			break;
		case ASPECT_RATIO.FOURTOTHREE:
			virtualWidth = 1280.0f;
			virtualHeight = 960.0f;
			break;
		}
		GUI_Matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width/virtualWidth, Screen.height/virtualHeight, 1.0f));
		GUI.matrix = GUI_Matrix;
	}
}
