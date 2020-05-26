using UnityEngine;

/**
 * Класс, реализующий потрясывание 
 * камеры при движении игрока
 * 
 * @author Лисова Анастасия, 17ИТ17
 */
public class CameraShake : MonoBehaviour {
    public float amount = 2;
    public float speed = 2;

    private Vector3 startPos;
    private float distation;
    private Vector3 rotation = Vector3.zero;

	void Start () {
        startPos = transform.position;
	}

	void Update () {
        distation += (transform.position - startPos).magnitude;
        startPos = transform.position;
        rotation.z = Mathf.Sin(distation * speed) * amount;
        transform.localEulerAngles = rotation;
	}
}
