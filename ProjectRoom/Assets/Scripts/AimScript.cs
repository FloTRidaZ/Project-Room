using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour {
	public Camera camera;
	private Animation mainAnim;
	private bool isPlaying;
	private string tag;
	private State state;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			ObjectControl ();
		}
	}

	void ObjectControl () {
		Ray ray = this.camera.ScreenPointToRay (transform.position);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			GameObject obj = hit.collider.gameObject;
			state = obj.GetComponent<State> ();
			this.tag = obj.tag;
			mainAnim = obj.GetComponent<Animation> ();
			DoAnim ();
		}
	}

	void DoAnim (){
		isPlaying = mainAnim.isPlaying;

		if (!state.IsOpen() && !isPlaying) {
			mainAnim.Play ("open" + tag);
			state.SetOpenState (true);
		} else if (state.IsOpen() && !isPlaying) {
			mainAnim.Play ("close" + tag);
			state.SetOpenState (false);
		}
	}
}
