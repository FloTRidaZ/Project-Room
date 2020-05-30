using UnityEngine;
using UnityEngine.EventSystems;

/**
 * Класс для управления камерой,
 * поворачивающейся в зависимости 
 * от того, куда свайпает игрок
 * 
 * @author Лисова Анастасия, 17ИТ17
 */
public class TouchCamera : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public Vector2 TouchDist;
    Vector2 PointerOld;
    int PointerId;
    public bool Pressed;

    public void OnPointerDown(PointerEventData eventData) {
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;
    }

    private void Update() {
        if (Pressed)  {
            if(PointerId >= 0 && PointerId < Input.touches.Length) {
                TouchDist = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;
            }
            else {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;
            }
        }
        else {
            TouchDist = new Vector2();
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        Pressed = false;
    }
}
