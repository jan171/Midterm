using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class BehindbackAttack : MonoBehaviour {

	public AudioClip victimDeathSound;
	public float delay;

	AudioSource audio;
	private Rigidbody2D playerBody;
	public float bounceOnVictim;

	public GameObject victimDeathEffect;


	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		playerBody = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();



	}
	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Player") {

			StartCoroutine ("DelayedDeath");
			Instantiate(victimDeathEffect, coll.transform.position, coll.transform.rotation);
			playerBody.velocity = new Vector2 (playerBody.velocity.x, bounceOnVictim);



		}

	}

	IEnumerator DelayedDeath(){

		audio.PlayOneShot (victimDeathSound, 1.0f);
		yield return new WaitForSeconds (delay);
		Destroy (transform.parent.gameObject);
	}


}

