using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Класс, хранящий текущее состояние ящика
 * тумбочки (открыта или закрыта)
 * @author KSO 17ИТ17
 */ 
public class State : MonoBehaviour {

	private bool state;

	void Start() {
		state = false;
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
}
