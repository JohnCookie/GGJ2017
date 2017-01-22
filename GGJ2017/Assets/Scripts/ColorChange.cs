using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{
	public Text title;

	Color currColor=Color.white;
	float currHSL = 0f;
	float currTime = 0;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		currTime += Time.deltaTime;
		if (currTime >= 0.2f) {
			currHSL += 1.5f;
			if (currHSL >= 360f) {
				currHSL = 0f;
			}
			currColor = Color.HSVToRGB (currHSL / 360f, 1f, 1f);
			title.color = currColor;
		}
	}
}

