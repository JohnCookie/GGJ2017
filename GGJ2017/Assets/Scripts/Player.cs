using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float moveSpd;
	public float rotateSpd;
	public float transScale = 20f;
	public GameObject startPoint;
	public GameObject endPoint;
	public GameObject controlA;
	public GameObject controlB;
	public ExposionGenerate waveGenerater;

	float lengthRandomRange = 1f;
	float controlRandomRage = 3f;

	public float life { get ; set ;}
	public Game m_game;

	void Start(){
		life = 250f;
	}
	// Update is called once per frame
	void Update ()
	{
		if (m_game.isRun) {
			Vector3 oldPosition = transform.localPosition;
			Vector3 oldEuler = transform.localEulerAngles;

			if (Input.GetKey (KeyCode.UpArrow)) {
				transform.localPosition += new Vector3 (moveSpd * Time.deltaTime * Mathf.Cos ((transform.localEulerAngles.z + 90) * Mathf.Deg2Rad), moveSpd * Time.deltaTime * Mathf.Sin ((transform.localEulerAngles.z + 90) * Mathf.Deg2Rad), 0);
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				transform.localPosition += new Vector3 (-moveSpd * Time.deltaTime * Mathf.Cos ((transform.localEulerAngles.z + 90) * Mathf.Deg2Rad), -moveSpd * Time.deltaTime * Mathf.Sin ((transform.localEulerAngles.z + 90) * Mathf.Deg2Rad), 0);
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				float z = transform.localEulerAngles.z + rotateSpd * Time.deltaTime;
				if (z > 180f) {
					z -= 360f;
				}
				transform.localEulerAngles = new Vector3 (0, 0, z);
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				float z = transform.localEulerAngles.z + rotateSpd * Time.deltaTime;
				if (z < -180f) {
					z += 360f;
				}
				transform.localEulerAngles -= new Vector3 (0, 0, rotateSpd * Time.deltaTime);
			}

			if (transform.localPosition.x < 0.2f) {
				transform.localPosition = new Vector3 (0.2f, transform.localPosition.y, transform.localPosition.z);
			}
			if (transform.localPosition.x > 19.8f) {
				transform.localPosition = new Vector3 (19.8f, transform.localPosition.y, transform.localPosition.z);
			}
			if (transform.localPosition.y < 0.2f) {
				transform.localPosition = new Vector3 (transform.localPosition.x, 0.2f, transform.localPosition.z);
			}
			if (transform.localPosition.y > 19.8f) {
				transform.localPosition = new Vector3 (transform.localPosition.x, 19.8f, transform.localPosition.z);
			}
			
			if (!waveGenerater.checkCanMove ()) {
				transform.localPosition = oldPosition;
				transform.localEulerAngles = oldEuler;
			}

			RandomBezierPoints (Time.deltaTime);
		}
	}

	public JPoint[] getPositions(){
		float x0 = startPoint.transform.position.x;
		float y0 = startPoint.transform.position.y;
		JPoint p0 = new JPoint ((int)(x0*50f), (int)(y0*50f));

		float x1 = controlA.transform.position.x;
		float y1 = controlA.transform.position.y;
		JPoint p1 = new JPoint ((int)(x1*50f), (int)(y1*50f));

		float x2 = controlB.transform.position.x;
		float y2 = controlB.transform.position.y;
		JPoint p2 = new JPoint ((int)(x2*50f), (int)(y2*50f));

		float x3 = endPoint.transform.position.x;
		float y3 = endPoint.transform.position.y;
		JPoint p3 = new JPoint ((int)(x3*50f), (int)(y3*50f));

		JPoint[] ret = new JPoint[4];
		if (x0 < x3) {
			ret [0] = p0;
			ret [1] = p1;
			ret [2] = p2;
			ret [3] = p3;
		} else {
			ret [0] = p3;
			ret [1] = p2;
			ret [2] = p1;
			ret [3] = p0;
		}


		return ret;
	}

	void RandomBezierPoints(float deltaTime){
		startPoint.transform.localPosition += new Vector3 (Random.Range (-lengthRandomRange, lengthRandomRange) * deltaTime, 0, 0);
		endPoint.transform.localPosition += new Vector3 (Random.Range (-lengthRandomRange, lengthRandomRange) * deltaTime, 0, 0);
		controlA.transform.localPosition += new Vector3 (Random.Range (-controlRandomRage, controlRandomRage) * deltaTime, Random.Range (-controlRandomRage, controlRandomRage) * deltaTime, 0);
		controlB.transform.localPosition += new Vector3 (Random.Range (-controlRandomRage, controlRandomRage) * deltaTime, Random.Range (-controlRandomRage, controlRandomRage) * deltaTime, 0);
	}

	void OnTriggerEnter(Collider col){
		Debug.Log ("Trigger enter");
		if (col.tag == "Target") {
			m_game.playSound (1);
			Destroy (col.transform.parent.gameObject);
			if (life + 40 >= 250) {
				life = 250;
			} else {
				life = life + 40;
			}
		}
	}

}

