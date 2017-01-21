using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExposionGenerate : MonoBehaviour
{
	public List<RoundWaveGenerate> roundwaveList = new List<RoundWaveGenerate>();
	public List<LineWaveGenerate> linewaveList = new List<LineWaveGenerate> ();
	public List<SpreadWaveGenerate> spreadwaveList = new List<SpreadWaveGenerate> ();
	public GameObject roundWave;
	public GameObject lineWave;
	public GameObject spreadWave;
	public Game game;
	// Use this for initialization
	void Start ()
	{
		roundwaveList.Clear ();
	}

	public void generateOneExplostion(){
		GameObject go = Instantiate (roundWave) as GameObject;
		go.transform.SetParent (transform);
		go.transform.position = new Vector3 (game.getGamePlayer().transform.position.x, game.getGamePlayer().transform.position.y, -0.1f);
		go.GetComponent<RoundWaveGenerate> ().Init (game);
		roundwaveList.Add (go.GetComponent<RoundWaveGenerate> ());
	}

	public void generateTwoExplostion(){
		GameObject go = Instantiate (lineWave) as GameObject;
		go.transform.SetParent (transform);
		go.transform.position = new Vector3 (game.getGamePlayer().transform.position.x, game.getGamePlayer().transform.position.y, -0.1f);
		go.transform.eulerAngles = game.getGamePlayer ().transform.eulerAngles;
		go.GetComponent<LineWaveGenerate> ().Init (game);
		linewaveList.Add (go.GetComponent<LineWaveGenerate> ());
	}

	public void generateThreeExplostion(){
		GameObject go = Instantiate (spreadWave) as GameObject;
		go.transform.SetParent (transform);
		go.transform.position = new Vector3 (game.getGamePlayer().transform.position.x, game.getGamePlayer().transform.position.y, -0.1f);
		go.transform.eulerAngles = game.getGamePlayer ().transform.eulerAngles;
		go.GetComponent<SpreadWaveGenerate> ().Init (game);
		spreadwaveList.Add (go.GetComponent<SpreadWaveGenerate> ());
	}

	public bool checkCanMove(){
		bool inarea = false;
		Collider2D[] cols = Physics2D.OverlapPointAll (new Vector2 (game.getGamePlayer ().transform.position.x, game.getGamePlayer ().transform.position.y));
		if (cols.Length > 0) {
			return true;
		} else {
			return false;
		}
	}
}

