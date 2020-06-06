using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/**
 * Класс, позволяющий игроку
 * вращать объект по осям x и y
 * при помощи свайпов
 */ 
public class RotationScript : MonoBehaviour, IDragHandler  {
	public GameObject parent;
	Quaternion origin;
	Quaternion targetX;
	Quaternion targetY;
	float deltaY;
	float deltaX;

	void Awake () {
		GameObject childObj = Instantiate <GameObject> (Resources.Load<GameObject> (Buffer.pathToPrefab));
		childObj.GetComponent<Rigidbody> ().isKinematic = true;
		childObj.transform.SetParent (parent.transform);
		childObj.transform.position = parent.transform.position;
	}

	void Start (){
		origin = parent.transform.rotation;
	}

	public void OnDrag (PointerEventData eventData){
		deltaY += eventData.delta.y;
		deltaX += eventData.delta.x;
		targetY = Quaternion.AngleAxis (deltaX, Vector3.up);
		targetX = Quaternion.AngleAxis (deltaY, Vector3.right);
		parent.transform.rotation = origin * targetY * targetX;
	}
}
