using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectingSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite activeCell;
    Sprite cell;
    Image img;

    // Use this for initialization
    void Start () {
        img = GetComponent<Image>();
        cell = img.sprite;
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.sprite = activeCell;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.sprite = cell;
    }

    private void OnDisable()
    {
        img.sprite = cell;
    }
}
