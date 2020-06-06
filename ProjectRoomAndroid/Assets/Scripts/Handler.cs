using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Handler : MonoBehaviour {
	public void toBack (){
		SceneManager.LoadScene (0);
	}
}
