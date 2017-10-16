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
	public AudioClip damageSfx;
	public AudioClip recoverHealth;
	public AudioClip levelTrigger;

	AudioSource audio;


	public GameObject respawn;

	private GameMaster gm;



	public int curHealth;
	public int maxHealth = 5;
	public bool deathCheck; 
	public bool hurt;

	public GameObject gameOverScreen;


	void Start () {

		rigidBody = gameObject.GetComponent<Rigidbody2D> (); //gives access to RigidBody2D component
		anim = gameObject.GetComponent<Animator> (); //gives access to Animator component

		audio = GetComponent<AudioSource> ();

		gm = GameObject.FindGameObjectWithTag ("Game Master").GetComponent<GameMaster> (); //get access to GameMaster Script

		gameOverScreen.SetActive (false);

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

			rigidBody.AddForce (((Vector2.right * speed) * h)/8);

		}

		if (Input.GetKeyDown (KeyCode.Space) && isGrounded == true) {

			rigidBody.AddForce ((Vector2.up * jumpForce));
			audio.PlayOneShot (jumpsfx, 1.0f);

		}

		if (!isGrounded) {
			
			speed = 300f;

		} else {

			speed = 400f;

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


	public GameObject vampireDeathEffect;
	public AudioClip vampireDeathSound;


	void Death (){

	
		deathCheck = true;


		gameOverScreen.SetActive(true);

		if (deathCheck){
			Debug.Log("PLayer Dead");

			Time.timeScale = 0;

		audio.PlayOneShot (vampireDeathSound, 1.0f);
		Instantiate (vampireDeathEffect, transform.position, transform.rotation);
		Destroy (gameObject);


		Debug.Log ("Player dead");

		SceneManager.LoadScene ("Forest");

		}
	}
	IEnumerator DelayedRestart() {
		yield return new WaitForSeconds (1);
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