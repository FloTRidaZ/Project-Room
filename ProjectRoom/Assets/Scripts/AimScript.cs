using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour {
	public Camera camera;
	private const string ANIM_OPEN_NAME = "open";
	private const string ANIM_CLOSE_NAME = "close";
	private bool isPlaying;
	private State state;

	void Start () {
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			ObjectControl ();
		}
	}
	/**
	 * Метод, который запускает анимацию открытия
	 * ящика тумбочки, если игрок находится на расстоянии
	 * 1 метра от ящика тумбочки
	 */ 
	private void ObjectControl () {
		Ray ray = this.camera.ScreenPointToRay (transform.position);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1f)) {
			GameObject obj = hit.collider.gameObject;
			DoAnim (obj.GetComponent<Animation> (), obj.GetComponent<State> (), obj.tag);
		}
	}

	/**
	 * Проигрывает анимацию открытия или
	 * закрытия ящика тумбочки в зависимости
	 * от ее состояния
	 */
	private void DoAnim (Animation anim, State state, string objTag){
		if (anim == null) {
			return;
		}
		isPlaying = anim.isPlaying;

		if (!state.IsOpen() && !isPlaying) {
			anim.Play (ANIM_OPEN_NAME + objTag);
			state.SetOpenState (true);
		} else if (state.IsOpen() && !isPlaying) {
			anim.Play (ANIM_CLOSE_NAME + objTag);
			state.SetOpenState (false);
		}
	}
}
