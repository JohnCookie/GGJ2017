using UnityEngine;
using System.Collections;

public class LineWave : MonoBehaviour
{	
	public float moveSpd = 1f;
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
			transform.localPosition += new Vector3 (0, moveSpd * Time.deltaTime, 0);
			transform.localScale = new Vector3 (transform.localScale.x + scaleSpd * Time.deltaTime, transform.localScale.y, transform.localScale.z);
			currColor = new Color (currColor.r, currColor.g, currColor.b, currColor.a - alphaSpd * Time.deltaTime);
			if (currColor.a <= 0.1f) {
				if (GetComponent<BoxCollider2D> () != null) {
					DestroyObject (GetComponent<BoxCollider2D> ());
				}
			}
			render.color = currColor;
		}
	}
}

