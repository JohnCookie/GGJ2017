using UnityEngine;
using System.Collections;

public class TargetGenerater : MonoBehaviour
{
	public float generateInterval;
	private float currTime = 99;

	public GameObject targetObj;
	public Game m_game;
	public TextureDrawing drawing;

	public float extraGenerateInterval;
	private float currExtraTime = 99;

	// Update is called once per frame
	void Update ()
	{
		if (m_game.isRun) {
			currTime += Time.deltaTime;
			if (currTime >= generateInterval) {
				currTime = 0;
				generateTarget ();
			}

			currExtraTime += Time.deltaTime;
			if (currExtraTime >= extraGenerateInterval) {
				currExtraTime = 0;
				generateExtraTarget ();
			}
		}
	}

	void generateTarget(){
		float x = Random.Range (1f, 19f);
		float y = Random.Range (1f, 19f);

		GameObject t = Instantiate (targetObj) as GameObject;
		t.transform.SetParent (transform);
		t.transform.localPosition = new Vector3 (x, y, -1);
		m_game.playSound (0);
	}

	void generateTargetAtXY(float x, float y){
		GameObject t = Instantiate (targetObj) as GameObject;
		t.transform.SetParent (transform);
		t.transform.localPosition = new Vector3 (x, y, -1);
		m_game.playSound (0);
	}

	void generateExtraTarget(){
		int x = Random.Range (10, 991);
		int y = Random.Range (10, 991);

		if (drawing.generateTarget (x, y)) {
			float fx = (float)((float)x / 50f);
			float fy = (float)((float)y / 50f);
			generateTargetAtXY (fx, fy);
			Debug.Log ("generateOneExtraTarget");
		} else {
			Debug.Log ("generateOneExtraTarget Failed");
		}
	}
}

