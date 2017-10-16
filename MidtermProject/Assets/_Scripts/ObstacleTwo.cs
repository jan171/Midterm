using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTwo : MonoBehaviour {

	private PlayerController player;


	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();

	}


	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player") {
			player.hurt = true;
			player.Damage (2);

		} else {
			player.hurt = false;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			player.hurt = false;
		}
	}
}
