using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float Ver, Hor;
    float Speed = 5;
	
	void Update () {
        Ver = Input.GetAxis("Vertical") * Time.deltaTime * Speed;
        Hor = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;

        transform.Translate(new Vector3(Hor, 0, Ver));
	}
}
