using UnityEngine;
using UnityEngine.EventSystems;

/**
 * Класс для управления камерой,
 * поворачивающейся в зависимости 
 * от того, куда свайпает игрок
 * 
 * @author Лисова Анастасия, 17ИТ17
 */
public class TouchCamera : MonoBehaviour, IDragHandler {
    public GameObject player;
    public GameObject cam;
    private Quaternion origin;
    private float deltaY;
    private float deltaX;

    void Start() {
        cam = player.transform.GetChild(0).gameObject;
        origin = player.transform.rotation;
    }

    public void OnDrag(PointerEventData eventData) {
        deltaY += eventData.delta.y;
        deltaY = Mathf.Clamp(deltaY, -60, 60);
        deltaX += eventData.delta.x;

        Quaternion rotationY = Quaternion.AngleAxis(deltaX / 4f, Vector3.up);
        Quaternion rotationX = Quaternion.AngleAxis(-deltaY / 4f, Vector3.right);

        player.transform.rotation = origin * rotationY;
        cam.transform.rotation = origin * player.transform.rotation * rotationX;
    }
}
