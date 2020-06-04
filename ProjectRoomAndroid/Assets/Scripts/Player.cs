using UnityEngine;

/**
 * Класс для управления 
 * игроком и камерой 
 * по осям X и Y 
 * 
 * @author Лисова Анастасия, 17ИТ17
 */
public class Player : MonoBehaviour {
    public TouchCamera touchField;
    public Joystick joystick;
    float Hor, Ver, yRot, xRot;
    readonly float Speed = 2;
    public GameObject cam;
    Quaternion StartingRotation;

    void Start()
    {
        StartingRotation = transform.rotation;
    }

    void FixedUpdate () {
        Hor = joystick.Horizontal() * Time.deltaTime * Speed;
        Ver = joystick.Vertical() * Time.deltaTime * Speed;
        transform.Translate(new Vector3(Hor, 0, Ver));

        xRot += touchField.TouchDist.x / 20;
        yRot += touchField.TouchDist.y / 20;
        yRot = Mathf.Clamp(yRot, -60, 60);
        
        Quaternion RotY = Quaternion.AngleAxis(xRot, Vector3.up);
        Quaternion RotX = Quaternion.AngleAxis(-yRot, Vector3.right);

        transform.rotation = StartingRotation * RotY;
        cam.transform.rotation = StartingRotation * transform.rotation * RotX;
    }
}
