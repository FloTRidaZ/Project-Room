using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScript : MonoBehaviour {
	Vector3 originPosition;
	Vector3 targetPosition;
	bool animateContinue;
	bool open;

	void Start () {
		originPosition = transform.position;
		float targetZ = originPosition.z - 0.3f;
		targetPosition = new Vector3 (originPosition.x,originPosition.y,targetZ);
		animateContinue = false;
		open = false;
	}


	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.E)) {
			open = !open;
			animateContinue = true;
		}
		if (animateContinue) {
			ContinueAnim ();
		}
	}
		

	void ContinueAnim () {
		if (open && transform.position != targetPosition) {
			OpenDoor ();
		} else if (!open && transform.position != originPosition) {
			CloseDoor ();
		}

	}

	void OpenDoor () {
		transform.position = Vector3.Slerp (transform.position, targetPosition, 0.1f);
	}

	void CloseDoor () {
		transform.position = Vector3.Slerp (transform.position, originPosition, 0.1f);
	}
}
