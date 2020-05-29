using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * Класс, представляющий собой 
 * джойстик для управления игроком
 * 
 * @author Лисова Анастасия, 17ИТ17
 */
public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {
    Image joystickBig;
    Image joystickSmall;
    Vector2 inputVector;

    void Start() {
        joystickBig = GetComponent<Image>();
        joystickSmall = transform.GetChild(0).GetComponent<Image>();
    }

    /**
     * Реагирует на нажатия игрока по джойстику
     * и соответственно перемещает внутренний 
     * джойстик относительно внешнего
     */
    public void OnDrag(PointerEventData eventData) {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBig.rectTransform, 
            eventData.position, eventData.pressEventCamera, out pos)) {
            pos.x = (pos.x / joystickBig.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystickBig.rectTransform.sizeDelta.y);
        }

        inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
        inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

        joystickSmall.rectTransform.anchoredPosition = 
            new Vector2(inputVector.x * (joystickBig.rectTransform.sizeDelta.x / 2), 
            inputVector.y * (joystickBig.rectTransform.sizeDelta.y / 2));
    }

    /**
     * Запускает метод OnDrag()
     * при нажатии на джойстик 
     */
    public void OnPointerDown(PointerEventData eventData) {
        OnDrag(eventData);
    }

    /**
     * Возвращает внутренний джойстик 
     * в центр внешнего, если игрок 
     * отпустил джойстик 
     */
    public void OnPointerUp(PointerEventData eventData) {
        inputVector = Vector2.zero;
        joystickSmall.rectTransform.anchoredPosition = Vector2.zero;
    }

    /**
     * Возвращает координату X 
     * перемещения игрока при управлении 
     * через джойстик, иначе через 
     * клавиатуру (клавиши A D)
     */
    public float Horizontal() {
        if(inputVector.x != 0) {
            return inputVector.x;
        } else {
            return Input.GetAxis("Horizontal");
        }
    }

    /**
     * Возвращает координату Y
     * перемещения игрока при управлении 
     * через джойстик, иначе через 
     * клавиатуру (клавиши W S)
     */
    public float Vertical() {
        if (inputVector.y != 0) {
            return inputVector.y;
        }
        else {
            return Input.GetAxis("Vertical");
        }
    }
}
