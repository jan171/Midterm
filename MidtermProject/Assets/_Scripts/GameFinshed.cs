using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinshed : MonoBehaviour {

	public GameObject finishGameScreen;
	public AudioClip gameWonSfx;
	private PlayerController player;
	AudioSource winner;

	void Start(){

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		finishGameScreen.SetActive (false);


	}
	void OnCollisionEnter2D (Collider2D col)
	{ 
		if (col.CompareTag ("EndGame")) {

			StartCoroutine ("FinishedGame");


		}

		}
	void Finish (){

		finishGameScreen.SetActive (true);
	
		if (Input.GetKeyDown (KeyCode.T)) {

			SceneManager.LoadScene ("StartMenu");
		}
	}
	IEnumerator FinishedGame(){
		winner.PlayOneShot(gameWonSfx,1.0f);
		yield return new WaitForSeconds (2);	
		Finish ();
	}



		}
	



	