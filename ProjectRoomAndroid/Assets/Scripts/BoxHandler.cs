using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoxHandler : MonoBehaviour, IPointerDownHandler {
	public Camera currentCamera;
	private bool isPlaying;


	public void OnPointerDown (PointerEventData eventData) {
		Vector3 pos = new Vector3 (eventData.position.x, eventData.position.y);
		Ray ray = currentCamera.ScreenPointToRay (pos);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 2f)){
			GameObject hittedObj = hit.collider.gameObject;
			toDetermine (hittedObj);
		}
	}

	private void toDetermine (GameObject obj){
		if (obj.GetComponent <Animation> ()) {
			animHandle (obj.GetComponent<Animation> (), obj.GetComponent<State> (), obj.tag);
		}
	}

	private void animHandle (Animation anim, State state, string objTag){
		isPlaying = anim.isPlaying;
		if (!isPlaying && !state.IsOpen ()) {
			anim.Play ("open" + objTag);
			state.ToOpen ();
		} else if (!isPlaying && state.IsOpen ()) {
			anim.Play ("close" + objTag);
			state.ToClose ();
		}
	}
}