using UnityEngine;
using System.Collections;

public class RoundWaveGenerate : MonoBehaviour
{
	public Game game;
	public GameObject wave;
	public int waveNum;
	public float waveInterval;
	int currWaveNum=0;
	float currInterval=10;
	bool play = false;
	float lifeTime = 0;

	public void Init(Game mgame){
		game = mgame;
		play = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (play) {
			currInterval += Time.deltaTime;
			if (currInterval >= waveInterval && currWaveNum<waveNum) {
				currInterval = 0;
				GenerateOne ();
				currWaveNum++;
			}
			lifeTime += Time.deltaTime;
			if (lifeTime > 8f) {
				transform.parent.GetComponent<ExposionGenerate> ().roundwaveList.Remove (this);
				Destroy (gameObject);
			}
		}
	}

	void GenerateOne(){
		GameObject w = Instantiate (wave) as GameObject;
		w.transform.SetParent (transform);
		w.transform.localPosition = Vector3.zero;
		w.SetActive (true);
		w.GetComponent<RoundWave> ().Init ();
	}

	public bool checkCharactorInRange(){
		Player player = game.getGamePlayer ();
		float distance = Vector3.Distance (player.transform.position, transform.position);
		if (distance < 4f) {
			return true;
		} else {
			return false;
		}
	}
}

