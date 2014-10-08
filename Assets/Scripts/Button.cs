using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public const int BUTTON_WIDTH = 108;
	public SpriteRenderer spriteRenderer;
	public int _color;

	public bool b_Active;
	// Use this for initialization
	void Start () {
		b_Active = false;


	}

	/*Int i is the index for the collor of the sprite, taken from Controller.cs
	 * 
	 * 
	 */
	public void LoadSprite(int i)
	{
		spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

		Debug.Log("Color = " + i);
		_color = i;
		switch (i)
		{
		case (0):
			//return Color.red;
			spriteRenderer.sprite = UnityEngine.Sprite.Create(
				Resources.Load<Texture2D>("red"),
				new Rect(0,0,108,108), new Vector2(0.5f, 0.5f));
			spriteRenderer.color = Color.red;
			break;
		case(5):
			//return new Color(1f, 0.64f, 0, 1f);	//Orange
			spriteRenderer.sprite = UnityEngine.Sprite.Create(
				Resources.Load<Texture2D>("orange"),
				new Rect(0,0,108,108), new Vector2(0.5f, 0.5f));
			spriteRenderer.color = new Color(1f, 0.64f, 0, 1f);
			break;
		case(3):
			//return Color.yellow;
			spriteRenderer.sprite = UnityEngine.Sprite.Create(
				Resources.Load<Texture2D>("yellow"),
				new Rect(0,0,108,108), new Vector2(0.5f, 0.5f));
			spriteRenderer.color = Color.yellow;
			break;
		case(2):
			//return Color.green;
			spriteRenderer.sprite = UnityEngine.Sprite.Create(
				Resources.Load<Texture2D>("Green"),
				new Rect(0,0,108,108), new Vector2(0.5f, 0.5f));
			spriteRenderer.color = Color.green;
			break;
		case(1):
			//return Color.blue;
			spriteRenderer.sprite = UnityEngine.Sprite.Create(
				Resources.Load<Texture2D>("Blue"),
				new Rect(0,0,108,108), new Vector2(0.5f, 0.5f));
			spriteRenderer.color = Color.blue;
			break;
		case(4):
			//return new Color(1f, 0, 1f, 1f); //Purple
			spriteRenderer.sprite = UnityEngine.Sprite.Create(
				Resources.Load<Texture2D>("purple"),
				new Rect(0,0,108,108), new Vector2(0.5f, 0.5f));
			spriteRenderer.color = new Color(1f, 0, 1f, 1f);
			break;
		default:
			//return Color.white;
			break;
		};

	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform ==  RuntimePlatform.WindowsEditor)
		{
			if (Input.GetMouseButtonDown(0))
			{
				CheckTouch(Input.mousePosition);
			}
		}
		else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			int i = 0;
		while (i < Input.touchCount) 
			{
			if(Input.GetTouch(i).phase == TouchPhase.Ended)
				{
				CheckTouch(Input.GetTouch(i).position);
				}
				i++;
			}
		}

	}


	void CheckTouch(Vector2 pos)
	{
		RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);


		if(hitInfo.transform == transform)
		{
			b_Active = true;
			Debug.Log("A touch has been activated on color: " + _color);
		}
		/*
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);

		if (collider2D.OverlapPoint(touchPos));
		{
			b_Active = true;
			Debug.Log("A touch has been activated");
			Debug.Log(touchPos);
			Debug.Log(transform.position);
			Debug.Log(Input.mousePosition);
		}

*/



	}
}
