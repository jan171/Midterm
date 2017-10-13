using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSytem : MonoBehaviour {

	public Sprite fullHealthBar;
	public Sprite fourfifthsHealthBar;
	public Sprite threefifthsHealthBar;
	public Sprite twofifthsHealthBar;
	public Sprite onefifthHealthBar;
	public Sprite emptyHealthBar;

	public Image healthUI;

	private PlayerController player;

	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();

	}

	// Update is called once per frame
	void Update () {

		if (player.curHealth == 5) {

			healthUI.sprite = fullHealthBar;

		}

		if (player.curHealth == 4) {

			healthUI.sprite = fourfifthsHealthBar;

		}

		if (player.curHealth == 3) {

			healthUI.sprite = threefifthsHealthBar;

		}

		if (player.curHealth == 2) {

			healthUI.sprite = twofifthsHealthBar;

		}

		if (player.curHealth == 1) {

			healthUI.sprite = onefifthHealthBar;

		}

		if (player.curHealth == 0) {

			healthUI.sprite = emptyHealthBar;

		}
	}

}