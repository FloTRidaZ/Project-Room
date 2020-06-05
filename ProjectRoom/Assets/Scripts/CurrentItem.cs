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
public class CurrentItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static string currentPath;

    Item content;

    [Header("Подсветка")]
    public Sprite activeCell;
    public Sprite highlightedCell;

    [HideInInspector]
    public int index;
    GameObject inventoryObject;
    Inventory inventory;
    Sprite cell;
    Image img;

    public void AddItem1(Item content)
    {
        Transform cell = inventory.cellContainer.transform.GetChild(index);
        Transform icon = cell.GetChild(0);
        Image img = icon.GetComponent<Image>();
        if (IsEmpty())
        {
            img.enabled = true;
            img.sprite = Resources.Load<Sprite>(content.pathToIcon);
        }
        else
        {
            img.enabled = false;
            img.sprite = null;
        }
    }

    public bool IsEmpty()
    {
        return content == null;
    }

    // Use this for initialization
    void Start()
    {
        inventoryObject = GameObject.FindGameObjectWithTag("InventoryManager");
        inventory = inventoryObject.GetComponent<Inventory>();
    }

    // Use this for initialization
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

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (IsEmpty())
            {
                currentPath = inventory.cells[index].content.pathToPrefab;
                SceneManager.LoadScene("Rotation");
            }
        }
        if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 2)
        {
            GameObject droppedObject = Instantiate(Resources.Load<GameObject>(inventory.cells[index].content.pathToPrefab));
            droppedObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
            inventory.cells[index] = null;
            /*inventory.DisplayItems()*/;
        }
    }

    /**
     * После закрытия инвентаря у всех ячеек выключается подсветка
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
            inventory.cellContainer.SetActive(false);
            inventory.cellContainer.SetActive(true);
            img.sprite = highlightedCell;
        }
    }

    /**
     * Если положение курсора совпадает с ячейкой инвентаря и 
     * при этом, курсор не затрагивает выбранную ячейку, то включается ее подсветка
     */
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (img.sprite != highlightedCell)
        {
            img.sprite = activeCell;
        }
    }

    /**
     * Если положение курсора не совпадает с ячейкой инвентаря и 
     * при этом, курсор не затрагивает выбранную ячейку, то подсветка выключается
     */
    public void OnPointerExit(PointerEventData eventData)
    {
        if (img.sprite != highlightedCell)
        {
            img.sprite = cell;
        }
    }

    /**
     * После закрытия инвентаря у всех ячеек, с учётом выбранной ячейки, выключается подсветка
     */
    private void OnDisable()
    {
        img.sprite = cell;
    }
}