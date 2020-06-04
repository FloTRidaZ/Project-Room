using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

/**
 * Класс, реализующий работу выбранного объекта в инвентаре
 *
 * @author Сотников Р. 17ит17
 */
public class CurrentItem : MonoBehaviour, IPointerClickHandler
{
    public static string currentPath;

    [Header("Подсветка")]
    public Sprite activeCell;
    public Sprite highlightedCell;

    [HideInInspector]
    public int index;
    GameObject inventoryObject;
    Inventory inventory;
    Sprite cell;
    Image img;

    void Start()
    {
        inventoryObject = GameObject.FindGameObjectWithTag("InventoryManager");
        inventory = inventoryObject.GetComponent<Inventory>();
    }
		
    void Awake()
    {
        img = GetComponent<Image>();
        cell = img.sprite;
    }

    /**
     * Метод, в котором проверяется, если нажата правая кнопка мыши,
     * то загружается сцена с вращением выбранного в инвентаре объекта
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        HighlightACell();
            //if (inventory.items[index].id != 0)
            //{
              //  currentPath = inventory.items[index].pathToPrefab;
                //SceneManager.LoadScene("Rotation");
            //}
    }

    /**
     * Выделение выбранной ячейки по клику на неё и, 
     * если кликнуть по выбранной ячейке ещё раз, то выделение снимется
     */
    private void HighlightACell()
    {
        if (img.sprite == highlightedCell)
        {
            img.sprite = cell;
        }
        else
        {
            img.sprite = highlightedCell;
        }
    }

    /**
     * После закрытия инвентаря у всех ячеек, с учётом выбранной ячейки, выключается подсветка
     */
    void OnDisable()
    {
        img.sprite = cell;
    }
}