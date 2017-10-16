using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimAI : MonoBehaviour {

	public float speed;
	Rigidbody2D victimBody;
	Transform victimPosition;
	float victimWidth;
	float victimHeight;

	public LayerMask victimMask;


		void Start () {
		SpriteRenderer victimSprite = this.GetComponent<SpriteRenderer> ();
		victimBody = this.GetComponent<Rigidbody2D> ();

		victimWidth = victimSprite.bounds.extents.x;
		victimHeight = victimSprite.bounds.extents.y;

		victimPosition = this.transform;
	}
	void FixedUpdate () {
		Vector2 linecastPos = victimPosition.position.toVector2 () - victimPosition.right.toVector2 () * victimWidth + Vector2.up * victimHeight;
		bool isGrounded = Physics2D.Linecast (linecastPos, linecastPos + Vector2.down, victimMask);
		bool isBlocked = Physics2D.Linecast (linecastPos, linecastPos - victimPosition.right.toVector2 () * .05f);

		Debug.DrawLine (linecastPos, linecastPos - victimPosition.right.toVector2 () * .05f);
		Debug.DrawLine (linecastPos, linecastPos + Vector2.down);

		Vector2 myVelocity = victimBody.velocity;
		myVelocity.x = victimPosition.right.x * -speed;
		victimBody.velocity = myVelocity;

		if (!isGrounded || isBlocked) {

			Vector3 currRot = victimPosition.eulerAngles;
			currRot.y += 180;
			victimPosition.eulerAngles = currRot;

		}
	}
}
