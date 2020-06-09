using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
/**
 * Класс, хранящий текущее состояние ящика
 * тумбочки (открыта или закрыта)
 * @author KSO 17ИТ17
 */ 
public class State : MonoBehaviour {
	private string savePath;

	private bool state;

	void Start() {
		state = false;
		savePath = Application.persistentDataPath + "/save" + tag + ".gamesave";
	}
	/**
	 * Открывает ящик тумбочки
	 */ 
	public void ToOpen() {
		this.state = true;
	}
	/**
	 * Закрывает ящик тумбочки
	 */ 
	public void ToClose() {
		this.state = false;
	}
	/**
	 * Возвращает текущее состояние
	 * ящика тумбочки
	 * 
	 * @return true если ящик тумбочи открыт иначе false
	 */ 
	public bool IsOpen () {
		return state;
	}

	/**
	 * Сохраняет данные о текущем объекте
	 */ 
	private void Save (){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fs = new FileStream (savePath, FileMode.Create);

		ObjectSaveManager manager = new ObjectSaveManager ();
		manager.Save (gameObject);
		bf.Serialize (fs, manager);
		fs.Close ();
	}

	/**
	 * Загружает сохраненные данные о текущем объекте
	 */ 
	private void Load () {
		if (!File.Exists (savePath)) {
			return;
		}

		BinaryFormatter bf = new BinaryFormatter ();

		FileStream fs = new FileStream (savePath, FileMode.Open);

		ObjectSaveManager manager = (ObjectSaveManager) bf.Deserialize (fs);
		fs.Close ();

		RestoreData (manager);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.F5)) {
			Save ();
		}

		if (Input.GetKeyDown (KeyCode.F9)) {
			Load ();
		}
	}

	/**
	 * Восстанавливает позицию, состояние и направление
	 * текущего объекта
	 * 
	 * @param manager объект, хранящий сохраненные данные о текущем
	 * объекте
	 */ 
	private void RestoreData (ObjectSaveManager manager){
		float posX = manager.saveData.pos.x;
		float posY = manager.saveData.pos.y;
		float posZ = manager.saveData.pos.z;

		float dirX = manager.saveData.dir.x;
		float dirY = manager.saveData.dir.y;
		float dirZ = manager.saveData.dir.z;

		Vector3 position = new Vector3 (posX, posY, posZ);
		Vector3 forward = new Vector3 (dirX, dirY, dirZ);

		transform.localPosition = position;
		transform.forward = forward;

		state = manager.saveData.isOpen;
	}
}
