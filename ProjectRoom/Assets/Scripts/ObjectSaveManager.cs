using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/**
 * Класс, предназначенный для
 * сохранения необходимых данных
 * игрового объекта, имеющий
 * компонент State
 * 
 * @author KSO 17ИТ17
 */ 
[System.Serializable]
public class ObjectSaveManager {

	public SaveData saveData;

	[System.Serializable]
	public struct Vec3 {
		public float x, y, z;

		public Vec3 (float x, float y, float z){
			this.x = x;
			this.y = y;
			this.z = z;
		}
	}
	[System.Serializable]
	public struct SaveData {

		public Vec3 pos, dir;
		public bool isOpen;

		public SaveData (Vec3 pos, Vec3 dir, bool isOpen){
			this.pos = pos;
			this.dir = dir;
			this.isOpen = isOpen;
		}
	}

	/**
	 * Сохраняет данные об объекте,
	 * имеющий компонент State
	 * 
	 * @param obj - игровой объект
	 */
	public void Save (GameObject obj){
		State state = obj.GetComponent<State> ();
		Vector3 localPos = state.transform.localPosition;
		Vector3 forward = state.transform.forward;
		Vec3 pos = new Vec3 (localPos.x, localPos.y, localPos.z);
		Vec3 dir = new Vec3 (forward.x, forward.y, forward.z);
		saveData = new SaveData (pos, dir, state.IsOpen ());
	}

}
