using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class BulletController : MonoBehaviour {

	public float speed;
	public GameObject bullet;
	public Transform enemyDirection;


	private Rigidbody2D bulletBody;



	public AudioClip vampireHitSound;


	AudioSource audio;



	// Use this for initialization
	void Start () {

		//if (enemyDirection.rotation.y != 180) {

			//bullet.transform.rotation = Quaternion.Euler(0,180,0);

		//}

		bulletBody = gameObject.GetComponent<Rigidbody2D> ();

		audio = GetComponent<AudioSource> ();

	}void Update () {

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed, 0);﻿

	}
	void OnTriggerEnter2D (Collider2D other){

		if (other.tag == "Enemy") { 
			audio.PlayOneShot (vampireHitSound, 1.0f);			

		}
		if (other.gameObject.tag == "BulletController") {
			Destroy (gameObject);


		}

	}
}
