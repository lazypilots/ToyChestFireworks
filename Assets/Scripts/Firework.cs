using UnityEngine;
using System.Collections;

public enum E_FireworkType
{
	PLAIN,
	CRACKLE,
	RINGED,
	CIRCLE

}

public class Firework : MonoBehaviour {
	public Color color;
	public Transform particle;
	public Transform left_ring;
	public float counter = 0;
	public float timer = 5.0f;
	public int TopSpeed;
	//public Color[]Mixedcolors;
	public E_FireworkType type;
	//public UnityEngine.Particle [] p;
	private bool _bSparks = true;


	// Use this for initialization
	void Start () {
		SetFireworkType();


		ParticleAnimator particleAnimator = GetComponent<ParticleAnimator>();
		Color[] modifiedColors = particleAnimator.colorAnimation;
		modifiedColors[0] =  Color.Lerp(color, Color.white, .4f);
		modifiedColors[0].a = 0.1f;
		modifiedColors[1] = Color.Lerp(color, Color.white, .1f);
		modifiedColors[1].a = 0.8f;
		modifiedColors[2] = color;
		modifiedColors[3] = Color.Lerp(color, Color.black, .1f);
		modifiedColors[3].a = 0.8f;
		modifiedColors[4] = Color.Lerp(color, Color.black, .4f);
		modifiedColors[4].a = 0.1f;
		particleAnimator.colorAnimation = modifiedColors;



		//Attempted ring emitter script//

		//Debug.Log("Set Color to" + color);
		/*
		p = gameObject.particleEmitter.particles;


		foreach (UnityEngine.Particle q in p )
		{
			q.velocity.Normalize();
			q.velocity.Scale( new Vector3( 5, 5, 0));
			Debug.Log("Change Particle #");
		}
		*/

		//anim.colorAnimation[0] = color;
		//anim.colorAnimation[1] = color;
		//anim.colorAnimation[2] = color;
		//anim.colorAnimation[3] = color;
		/*
		for (int i = 0; i < (Gameoptions.FireworkDetail * 35); i++)
		{
			Instantiate(particle, transform.position, Quaternion.identity);
			audio.Play();
			Particle script = (Particle)particle.GetComponent("Particle");
			script.angle =    Random.Range(0,360);
			script.speed = Random.Range(1,TopSpeed * 20);
			script.speed = script.speed / 20;
			script.color = color;
			Instantiate(particle, transform.position, Quaternion.identity);
			counter++;
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		timer -=Time.deltaTime;

		if (timer <4.3 && _bSparks && type == E_FireworkType.CRACKLE)
		{
			Instantiate(particle, transform.position, Quaternion.identity);
			_bSparks = false;
		}
		if (timer < 4.5 && type != E_FireworkType.CRACKLE)
		{
			audio.Stop();
		}
		if (type == E_FireworkType.RINGED && _bSparks)
		{
			Instantiate(left_ring, transform.position, Quaternion.Euler(new Vector3(0,0, Random.Range(0, 60) - 30) ));
			_bSparks = false;
		}

	if (timer < 0)
		{
			Destroy(gameObject);
		}
	}


	public void SetFireworkType()
	{
		int i = Random.Range(0,20);
		if (i < 8)
		{
			type = E_FireworkType.PLAIN;
		}
		else if (i < 14)
		{
			type = E_FireworkType.CRACKLE;
		}
		else
		{
			type = E_FireworkType.RINGED;
		}


	}
}
