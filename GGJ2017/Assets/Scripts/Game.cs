using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public Player player;
	public ExposionGenerate waveGenerater;
	public float waveGenerateInterval = 1.5f;
	public GameObject titleView;
	public GameObject progressView;

	public TargetGenerater targetGenerater;
	public AudioSource source;
	public AudioClip[] clips;

	public TextMesh resultText;
	public Animation resultAnim;
	public TextMesh jokeText;

	float currTime=99f;
	public bool isRun = false;

	public void startGame(){
		currTime = 99f;
		player.transform.localPosition = new Vector3 (10f, 10f, -1f);
		player.life = 250f;
		for (int i = 0; i < waveGenerater.transform.childCount; i++) {
			Destroy (waveGenerater.transform.GetChild (i).gameObject);
		}
		waveGenerater.transform.DetachChildren ();

		for (int i = 0; i < targetGenerater.transform.childCount; i++) {
			Destroy (targetGenerater.transform.GetChild (i).gameObject);
		}
		targetGenerater.transform.DetachChildren ();
		isRun = true;
	}

	// Update is called once per frame
	void Update ()
	{
		if (isRun) {
			currTime += Time.deltaTime;
			if (currTime >= waveGenerateInterval) {
				currTime = 0;
				int type = Random.Range (0, 3);
				switch (type) {
				case 0:
					waveGenerater.generateOneExplostion ();
					break;
				case 1:
					waveGenerater.generateTwoExplostion ();
					break;
				case 2:
					waveGenerater.generateThreeExplostion ();
					break;
				}
			}

			player.life -= 10f * Time.deltaTime;
			if (player.life <= 0) {
				player.life = 0;
				Debug.Log ("Die");
				resultText.text = "Filled 30% of the canvas.";
				resultText.gameObject.SetActive (true);
				jokeText.gameObject.SetActive (true);
				resultAnim.Stop ();
				resultAnim.Play ();
				FindObjectOfType<TextureDrawing> ().saveDieTexture ();
				isRun = false;
			}
			UpdateProgress ();
		} else {
			if (Input.GetKeyDown (KeyCode.Space)) {
				resultText.gameObject.SetActive (false);
				jokeText.gameObject.SetActive (false);
				FindObjectOfType<TextureDrawing> ().initTexture ();
				transform.parent.gameObject.SetActive (false);
				titleView.SetActive (true);
				progressView.SetActive (false);
			}
		}
	}

	public Player getGamePlayer(){
		return player;
	}

	void UpdateProgress(){
		float scaleX = player.life / 250f;
		progressView.transform.FindChild ("pro").localScale = new Vector3 (scaleX, 1f, 1f);
	}

	public void playSound(int index){
		source.clip = clips[index];
		source.Stop ();
		source.loop = false;
		source.Play ();
	}
}

