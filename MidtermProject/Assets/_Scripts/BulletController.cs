﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class BulletController : MonoBehaviour {

	public float speed;
	public GameObject bullet;
	private Rigidbody2D bulletBody;
	private EnemyAI enemy;


	public AudioClip vampireHitSound;


	AudioSource audio;



	// Use this for initialization
	void Start () {
		enemy = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<EnemyAI> ();

		if (enemy.transform.localScale.x < 0) {

			speed = -speed;

		}
		bulletBody = gameObject.GetComponent<Rigidbody2D> ();

		audio = GetComponent<AudioSource> ();

	}void Update () {

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed, 0);﻿

	}
	void OnTriggerEnter2D (Collider2D other){

		if (other.tag == "Player") { 
			audio.PlayOneShot (vampireHitSound, 1.0f);			

		}
		if (other.gameObject.tag == "BulletController") {
			Destroy (gameObject);


		}

	}
}
