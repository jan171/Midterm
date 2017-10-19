using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;


public class EnemyAI : MonoBehaviour {

	public float speed;


	public Transform bulletPoint;
	public GameObject bullet;


	Rigidbody2D enemyBody;
	Transform enemyPosition;
	float enemyWidth;
	float enemyHeight;

	public LayerMask enemyMask;



	void Start () {

		SpriteRenderer enemySprite = this.GetComponent<SpriteRenderer> ();
		enemyBody = this.GetComponent<Rigidbody2D> ();

		enemyWidth = enemySprite.bounds.extents.x; //gets enemy width on x axis
		enemyHeight = enemySprite.bounds.extents.y; // gets enemy height on y axis

		enemyPosition = this.transform;
	}
	 

	void OnTriggerEnter2D (Collider2D col){

		if (col.CompareTag ("Player")){

			Instantiate (bullet, bulletPoint.position, bulletPoint.rotation);

		}

	}

	void FixedUpdate () {

		Vector2 lineCastPos = enemyPosition.position.toVector2 () - enemyPosition.right.toVector2 () * enemyWidth + Vector2.up * enemyHeight;

		bool isGrounded = Physics2D.Linecast (lineCastPos, lineCastPos + Vector2.down, enemyMask);

		bool isBlocked = Physics2D.Linecast (lineCastPos, lineCastPos - enemyPosition.right.toVector2 () * .05f);

		Debug.DrawLine (lineCastPos, lineCastPos - enemyPosition.right.toVector2 () * .05f);

		Debug.DrawLine (lineCastPos, lineCastPos + Vector2.down);

		Debug.Log (isGrounded);

		Vector2 myVelocity = enemyBody.velocity;
		myVelocity.x = enemyPosition.right.x * -speed;
		enemyBody.velocity = myVelocity;


		if (!isGrounded || isBlocked) {

			Vector3 currRot = enemyPosition.eulerAngles;
			currRot.y += 180;
			enemyPosition.eulerAngles = currRot;

		}
	}

}