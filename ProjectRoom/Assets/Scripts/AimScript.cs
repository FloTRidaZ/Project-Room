using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Класс, предназначенный для взаимодействия
 * между игроком и окружающими его объектами
 * при помощи прицела
 * 
 * @author KSO 17ИТ17
 */ 
public class AimScript : MonoBehaviour {
	private const string ANIM_OPEN_NAME = "open";
	private const string ANIM_CLOSE_NAME = "close";
	public Camera cam;
	private bool isPlaying;
	Inventory inventory;

	void Awake () {
		GameObject inventoryObject = GameObject.FindGameObjectWithTag("InventoryManager");
		inventory = inventoryObject.GetComponent<Inventory>();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			ObjectControl ();
		}
	}
	/**
	 * Определяет, находится ли объект
	 * в области прицеливания. Если объект не равен null - будет
	 * вызвана функция для определения и выполнения действия над ним.
	 */ 
	private void ObjectControl () {
		Ray ray = this.cam.ScreenPointToRay (transform.position);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1f)) {
			GameObject obj = hit.collider.gameObject;
			ToDetermine (obj);
		}
	}
	/**
	 * Определяет выполнимое действие над
	 * объектом в зависимости от имеющихся у него
	 * компонентов и запускает определенные функции
	 * для их выполнения
	 * 
	 * @param obj объект, размещенный на сцене
	 */
	private void ToDetermine (GameObject obj){
		if (obj.GetComponent<Animation> ()) {
			DoAnim (obj.GetComponent<Animation> (), obj.GetComponent<State> (), obj.tag);
		}

		if (obj.GetComponent<Item> ()) {
			inventory.AddItem (obj);
		}
	}

	/**
	 * Проигрывает анимацию целевого объекта в зависимости от
	 * его состояния
	 * 
	 * @param anim - компонент анимация текущего объекта
	 * @param state - компонент состояния текущего объекта
	 * @param objTag - тег текущего объекта
	 */
	private void DoAnim (Animation anim, State state, string objTag){
		isPlaying = anim.isPlaying;

		if (!state.IsOpen() && !isPlaying) {
			anim.Play (ANIM_OPEN_NAME + objTag);
			state.ToOpen ();
		} else if (state.IsOpen() && !isPlaying) {
			anim.Play (ANIM_CLOSE_NAME + objTag);
			state.ToClose ();
		}
	}
}
