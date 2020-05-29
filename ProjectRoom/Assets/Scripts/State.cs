using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour {

	private bool state;

	void Start() {
		state = false;
	}

	public void SetOpenState(bool state) {
		this.state = state;
	}

	public bool IsOpen () {
		return state;
	}
}
