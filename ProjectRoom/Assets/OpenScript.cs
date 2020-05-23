using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScript : MonoBehaviour {
	Vector3 originPosition;
	float targetZ;
	bool opening;
	// Use this for initialization
	void Start () {
		originPosition = transform.position;
		opening = false;
		targetZ = originPosition.z - 0.3f;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E) || opening) {
			float z = transform.position.z - 0.01f;
			if (z <= targetZ) {
				opening = false;
			} else {
				opening = true;
			}
			z = Mathf.Clamp (z, targetZ, originPosition.z);
			transform.position = new Vector3 (originPosition.x, originPosition.y, z);
		}
	}
}
