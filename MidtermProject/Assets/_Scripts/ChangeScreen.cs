using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class ChangeScreen : MonoBehaviour {


	public AudioClip uiSound;
	AudioSource audio;

	void Start(){

		audio = GetComponent<AudioSource> ();

	}

	public void ChangeToScene(int SceneToChangeTo) {

		SceneManager.LoadScene (SceneToChangeTo);

		StartCoroutine ("Delay");


		if (SceneToChangeTo == 10) {

			Application.Quit ();

		}

	}
	IEnumerator Delay() {

		audio.PlayOneShot (uiSound, 1.0f);
		yield return new WaitForSeconds (1);


	}

}
