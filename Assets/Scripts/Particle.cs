using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	public Color color;
	public float timer = 1.0f;
	public float angle;
	public float speed;

	private bool _bEndLife = false;

	// Use this for initialization
	void Start () {
		transform.rigidbody2D.velocity = new Vector2( speed * Mathf.Cos(Mathf.Deg2Rad*angle) , speed * Mathf.Sin(Mathf.Deg2Rad*angle));
		SpriteRenderer renderer = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");
		renderer.color = new Color(color.r,color.g ,color.b, 1f );

	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

	if (timer <= 0)
		{
			if (_bEndLife)
			{
				EndLife();
			}
			else
			{
				transform.rigidbody2D.velocity = Vector2.zero;
				transform.rigidbody2D.gravityScale = 0.5f;
				transform.rigidbody2D.drag = 2.0f;
				timer = 2.0f;
				_bEndLife = true;
			}
		}
	}

	private void EndLife()
	{
		Destroy(gameObject);
	}
}
