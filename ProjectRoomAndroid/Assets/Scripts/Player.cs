using UnityEngine;

/**
* Класс для управления
* игроком по осям X и Y
*
* @author Лисова Анастасия, 17ИТ17
*/
public class Player : MonoBehaviour {
	public Joystick joystick;
	private float Hor, Ver;
	private readonly float Speed = 2;

	void FixedUpdate () {
		Hor = joystick.Horizontal() * Time.deltaTime * Speed;
		Ver = joystick.Vertical() * Time.deltaTime * Speed;
		transform.Translate(new Vector3(Hor, 0, Ver));
	}
}