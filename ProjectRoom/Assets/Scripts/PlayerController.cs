using UnityEngine;

/**
 * Класс для управления игроком 
 * и камерой по осям X и Y
 * 
 * @author Лисова Анастасия, 17ИТ17
 */
public class PlayerController : MonoBehaviour {
    public GameObject cam;
    Quaternion StartingRotation;
    float Ver, Hor, RotHor, RotVer;
    readonly float Speed = 2;

    private void Start() {
        StartingRotation = transform.rotation;
    }

    void FixedUpdate () {
        RotHor += Input.GetAxis("Mouse X") * Speed;
        RotVer += Input.GetAxis("Mouse Y") * Speed;

        RotVer = Mathf.Clamp(RotVer, -60, 60);

        Quaternion RotY = Quaternion.AngleAxis(RotHor, Vector3.up);
        Quaternion RotX = Quaternion.AngleAxis(-RotVer, Vector3.right);

        transform.rotation = StartingRotation * RotY;
        cam.transform.rotation = StartingRotation * transform.rotation * RotX;

        if (Input.GetKey(KeyCode.LeftShift)) {
            Ver = Input.GetAxis("Vertical") * Time.deltaTime * Speed * 2;
            Hor = Input.GetAxis("Horizontal") * Time.deltaTime * Speed * 2;
        }
        else {
            Ver = Input.GetAxis("Vertical") * Time.deltaTime * Speed;
            Hor = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
        }

        transform.Translate(new Vector3(Hor, 0, Ver));
    }
}
