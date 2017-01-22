using UnityEngine;
using System.Collections;

public class RandomWaveGenerate : MonoBehaviour
{
	public Game game;
	public GameObject[] wave;
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
				transform.parent.GetComponent<ExposionGenerate> ().randomwaveList.Remove (this);
				Destroy (gameObject);
			}
		}
	}

	void GenerateOne(){
		GameObject w = Instantiate (wave[Random.Range(0,3)]) as GameObject;
		w.transform.SetParent (transform);
		w.transform.localPosition = Vector3.zero;
		w.SetActive (true);
		w.GetComponent<RandomWave> ().Init ();
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

