using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverHealth : MonoBehaviour {

	private PlayerController player;


	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();

	}


	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Player") {
			player.Recover (1);
		}
	}

}
