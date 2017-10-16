using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour {

	public float speed;
	Rigidbody2D zombieBody;
	Transform zombiePosition;
	float zombieWidth;
	float zombieHeight;

	public LayerMask zombieMask;


	void Start () {
		SpriteRenderer victimSprite = this.GetComponent<SpriteRenderer> ();
		zombieBody = this.GetComponent<Rigidbody2D> ();

		zombieWidth = victimSprite.bounds.extents.x;
		zombieHeight = victimSprite.bounds.extents.y;

		zombiePosition = this.transform;
	}
	void FixedUpdate () {
		Vector2 linecastPos = zombiePosition.position.toVector2 () - zombiePosition.right.toVector2 () * zombieWidth + Vector2.up * zombieHeight;
		bool isGrounded = Physics2D.Linecast (linecastPos, linecastPos + Vector2.down, zombieMask);
		bool isBlocked = Physics2D.Linecast (linecastPos, linecastPos - zombiePosition.right.toVector2 () * .05f);

		Debug.DrawLine (linecastPos, linecastPos - zombiePosition.right.toVector2 () * .05f);
		Debug.DrawLine (linecastPos, linecastPos + Vector2.down);

		Vector2 myVelocity = zombieBody.velocity;
		myVelocity.x = zombiePosition.right.x * -speed;
		zombieBody.velocity = myVelocity;

		if (!isGrounded || isBlocked) {

			Vector3 currRot = zombiePosition.eulerAngles;
			currRot.y += 180;
			zombiePosition.eulerAngles = currRot;

		}
	}
}
