using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
	public Game m_game;
	public GameObject titleView;
	public GameObject progressView;

	public void startGame(){
		titleView.SetActive (false);
		progressView.SetActive (true);
		m_game.transform.parent.gameObject.SetActive (true);
		m_game.startGame ();
	}

	void Update(){
		if (titleView.activeSelf) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				startGame ();
			}
		}
	}
}

