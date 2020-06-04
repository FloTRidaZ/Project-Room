using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoxHandler : MonoBehaviour, IPointerDownHandler {
	private Animation anim;
	private string tag;
	private bool isPlaying;
	private State state;

	void Start () {
		anim = GetComponent<Animation> ();
		tag = gameObject.tag;
		state = GetComponent<State> ();
	}


	void Update () {

	}

	public void OnPointerDown (PointerEventData eventData) {
		if (eventData.pointerCurrentRaycast.distance <= 1f) {
			animHandle ();
		}
	}

	private void animHandle (){
		isPlaying = anim.isPlaying;
		if (!isPlaying && !state.IsOpen ()) {
			anim.Play ("open" + tag);
			state.ToOpen ();
		} else if (!isPlaying && state.IsOpen ()) {
			anim.Play ("close" + tag);
			state.ToClose ();
		}
	}
}