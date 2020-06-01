using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * Класс, реализующий подсветку выбранных объектов в инвентаре
 *
 * @author Сотников Р. 17ит17
 */
public class HighlightingACell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Подсветка")]
    public Sprite activeCell;

    Sprite cell;
    Image img;

    // Use this for initialization
    void Start() {
        img = GetComponent<Image>();
        cell = img.sprite;
	}

    /**
     * Если положение курсора совпадает с ячейкой инвентаря, то включается ее подсветка
     */
    public void OnPointerEnter(PointerEventData eventData)
    {
        img.sprite = activeCell;
    }

    /**
     * Если положение курсора не совпадает с ячейкой инвентаря, то подсветка выключается
     */
    public void OnPointerExit(PointerEventData eventData)
    {
        img.sprite = cell;
    }

    /**
     * После закрытия инвентаря у всех ячеек выключается подсветка
     */
    private void OnDisable()
    {
        img.sprite = cell;
    }
}
