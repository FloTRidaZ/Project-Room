using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotationScript : MonoBehaviour {
	GameObject rotatedObj;
	Quaternion origin;
	Quaternion rotationY;
	Quaternion rotationX;
	float speed = 6.5f;
	float mouseX;
	float mouseY;

	void Awake () {
		rotatedObj = Instantiate(Resources.Load<GameObject>(CurrentItem.currentPath));
		rotatedObj.transform.parent = gameObject.transform;
		rotatedObj.transform.position = gameObject.transform.position;
	}

	void Start () {
		origin = transform.rotation;
	}

	void Update () {
		rotatedObj.GetComponent<Rigidbody> ().Sleep ();
		if (Input.GetMouseButtonDown(1)) {
			SceneManager.LoadScene ("Demo");
		}
	}

	void FixedUpdate () {
		mouseX += Input.GetAxis ("Mouse X") * speed;
		mouseY += Input.GetAxis ("Mouse Y") * speed;
		rotationY = Quaternion.AngleAxis (mouseX, Vector3.up);
		rotationX = Quaternion.AngleAxis (mouseY, Vector3.right);
		transform.rotation = origin * rotationX * rotationY;
	}
}
