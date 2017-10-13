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
	public AudioClip coinCollect;
	public AudioClip damageSfx;

	AudioSource audio;

	public GameObject respawn;

	private GameMaster gm;

	//health stats
	public int curHealth;
	public int maxHealth = 5;
	public bool deathCheck; 
	public bool hurt;

	public GameObject bullet;
	public Transform bulletPoint;



	void Start () {

		rigidBody = gameObject.GetComponent<Rigidbody2D> (); //gives access to RigidBody2D component
		anim = gameObject.GetComponent<Animator> (); //gives access to Animator component

		audio = GetComponent<AudioSource> ();

		gm = GameObject.FindGameObjectWithTag ("Game Master").GetComponent<GameMaster> (); //get access to GameMaster Script



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
			StartCoroutine ("Delayed Start");
		}


		if (Input.GetKeyDown (KeyCode.Z)) {

			Instantiate (bullet, bulletPoint.position, bulletPoint.rotation);

		}


	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.CompareTag ("KillZone")) {
			transform.position = respawn.transform.position;
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
		Debug.Log ("Player dead");
		SceneManager.LoadScene ("level1");

		}
	IEnumerator DelayedRestart (){
		yield return new WaitForSeconds (1);
		Death ();

	}

	public void Damage (int dmg) {
		audio.PlayOneShot (damageSfx, 1.0f);
		curHealth -= dmg;
	}
}