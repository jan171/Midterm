using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class PlayerController : MonoBehaviour {

	public float speed;
	public float maxSpeed;
	public float jumpForce;

	public bool isGrounded;

	private Rigidbody2D rigidBody;
	private Animator anim;

	public AudioClip jumpsfx;
	public AudioClip batCollect;
	public AudioClip bottleCollect;
	public AudioClip damageSfx;

	public AudioClip levelTrigger;

	AudioSource audio;


	public GameObject respawn;

	private GameMaster gm;

	public GameObject vampireDeathEffect;
	public AudioClip vampireDeathSound;

	public int curHealth;
	public int maxHealth = 5;
	public bool deathCheck; 
	public bool hurt;

	public GameObject gameOverScreen;
	public GameObject player;

	void Start () {

		rigidBody = gameObject.GetComponent<Rigidbody2D> (); //gives access to RigidBody2D component
		anim = gameObject.GetComponent<Animator> (); //gives access to Animator component

		audio = GetComponent<AudioSource> ();

		gm = GameObject.FindGameObjectWithTag ("Game Master").GetComponent<GameMaster> (); //get access to GameMaster Script

		gameOverScreen.SetActive (false);

		player.SetActive (true);

	}

	void Update () {

		anim.SetBool ("IsGrounded", isGrounded);
		anim.SetFloat ("Speed", Mathf.Abs(rigidBody.velocity.x));
		anim.SetBool ("IsAlive", deathCheck);
		anim.SetBool ("IsDamaged", hurt);


		float h = Input.GetAxis ("Horizontal");

		if (Input.GetAxis ("Horizontal") < -.001f) {
			
			transform.localScale = new Vector3 (-1.0f, 1.0f, 1.0f); // turns the sprite on the x axis

		}

		if (Input.GetAxis ("Horizontal") > .001f) {

			transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f); // turns the sprite on the x axis

		}

		if (isGrounded) {

			rigidBody.AddForce ((Vector2.right * speed) * h);
		
		}

		else if (!isGrounded) {

			rigidBody.AddForce (((Vector2.right * speed) * h)/4);

		}

		if (Input.GetKeyDown (KeyCode.Space) && isGrounded == true) {

			rigidBody.AddForce ((Vector2.up * jumpForce));
			audio.PlayOneShot (jumpsfx, 1.0f);

		}

		if (!isGrounded) {
			
			speed = 300f;

		} else {

			speed = 100f;

		}
		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}

		if (curHealth <= 0) {
			StartCoroutine ("DelayedRestart");
		}

		if (Input.GetKeyDown (KeyCode.R)) {

			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			Time.timeScale = 1;

		}


	}

	void OnTriggerEnter2D(Collider2D col){
		
		if (col.CompareTag ("KillZone")) {
			transform.position = respawn.transform.position;
		}

		if (col.CompareTag("Attack")){
			gm.points += 1;

		}

		if (col.CompareTag ("Bat")) {
			Destroy(col.gameObject);
			gm.points += 1;
			audio.PlayOneShot (batCollect, 1.0f);
		}

		if (col.CompareTag ("BloodBottle")) {
			Destroy(col.gameObject);
			gm.points += 2;
			audio.PlayOneShot (bottleCollect, 1.0f);
		}


		if (col.CompareTag ("trigger box")) {

			audio.PlayOneShot (levelTrigger, 1.0f);

			SceneManager.LoadScene ("Level2");

		}

}

	void FixedUpdate(){

		Vector3 easeVelocity = rigidBody.velocity;
		easeVelocity.y = rigidBody.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;

		if(isGrounded) {

			rigidBody.velocity = easeVelocity;

		}

		if(rigidBody.velocity.x>maxSpeed) {
			
			rigidBody.velocity = new Vector2 (maxSpeed, rigidBody.velocity.y);

		}

		if(rigidBody.velocity.x < -maxSpeed){

			rigidBody.velocity = new Vector2 (-maxSpeed, rigidBody.velocity.y);

		}

	}




	void Death (){
		deathCheck = true;

	
		gameOverScreen.SetActive (true);
		Destroy(audio);
		if (deathCheck){
			Debug.Log ("Player Dead");
		
			Time.timeScale = 0;
	
	
		}

	}
	IEnumerator DelayedRestart() {
		player.SetActive (false);
		audio.PlayOneShot (vampireDeathSound, 1.0f);
		Instantiate(vampireDeathEffect, rigidBody.transform.position, rigidBody.transform.rotation);
		yield return new WaitForSeconds (2);
	
		Death ();

	}

	public void Damage (int dmg) {
		audio.PlayOneShot (damageSfx, 1.0f);
		curHealth -= dmg;
	}
	public void Recover (int dmg){
		curHealth += dmg;

	}
		
}