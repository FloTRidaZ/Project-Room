using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScript : MonoBehaviour {
	private bool open;
	private Animation mainAnim;
	private bool isPlaying;

	void Start () {
		open = false;
		mainAnim = gameObject.GetComponent<Animation> ();
	}


	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			DoAnim ();
		}
	}

	void DoAnim (){
		isPlaying = mainAnim.isPlaying;
		if (!open && !isPlaying) {
			mainAnim.Play ("New Animation");
			open = true;
		} else if (open && !isPlaying) {
			mainAnim.Play ("New Animation 2");
			open = false;
		}
	}
		
}
