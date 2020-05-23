﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour {
	Quaternion origin;
	float speed = 6.5f;
	float mouseX;
	float mouseY;

	void Start () {
		origin = transform.rotation;
	}

	void FixedUpdate () {
		mouseX += Input.GetAxis ("Mouse X") * speed;
		mouseY += Input.GetAxis ("Mouse Y") * speed;
		Quaternion horizontal = Quaternion.AngleAxis (mouseX, Vector3.up);
		Quaternion vertical = Quaternion.AngleAxis (mouseY, Vector3.right);
		transform.rotation = origin * horizontal * vertical;
	}
}
