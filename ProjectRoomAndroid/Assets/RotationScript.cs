using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationScript : MonoBehaviour, IDragHandler  {
	public GameObject current;
	Quaternion origin;
	Quaternion targetX;
	Quaternion targetY;
	float deltaY;
	float deltaX;

	void Start (){
		origin = current.transform.rotation;
	}
	
	public void OnDrag (PointerEventData eventData){
		deltaY += eventData.delta.y;
		deltaX += eventData.delta.x;
		targetY = Quaternion.AngleAxis (deltaX, Vector3.up);
		targetX = Quaternion.AngleAxis (deltaY, Vector3.right);
		current.transform.rotation = origin * targetX * targetY;
	}
}
