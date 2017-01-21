using UnityEngine;
using System.Collections;

public class RoundWave : MonoBehaviour
{	
	public float scaleSpd = 1f;
	public float alphaSpd = 1f;
	public SpriteRenderer render;
	bool playing=false;
	Color currColor;

	public void Init(){
		playing = true;
		render = GetComponent<SpriteRenderer> ();
		currColor = render.color;
	}

	void Update ()
	{
		if (playing) {
			transform.localScale = new Vector3 (transform.localScale.x + scaleSpd * Time.deltaTime, transform.localScale.y + scaleSpd * Time.deltaTime, transform.localScale.z);
			currColor = new Color (currColor.r, currColor.g, currColor.b, currColor.a - alphaSpd * Time.deltaTime);
			if (currColor.a <= 0.1f) {
				if (GetComponent<CircleCollider2D> () != null) {
					DestroyObject (GetComponent<CircleCollider2D> ());
				}
			}
			render.color = currColor;
		}
	}
}

