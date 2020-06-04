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

    [Header("Подсветка")]
    public Sprite activeCell;

    [HideInInspector]
    public int index;
    GameObject inventoryObject;
    Inventory inventory;
    Sprite cell;
    Image img;

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
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // GameObject droppedObject = Instantiate(Resources.Load<GameObject>(inventory.items[index].pathToPrefab));
            // droppedObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
            // inventory.items[index] = new Item();
            // inventory.DisplayItems();
            if (inventory.items[index].id != 0)
            {
                //inventory.cellContainer.SetActive(!inventory.cellContainer.activeSelf);
                currentPath = inventory.items[index].pathToPrefab;
                SceneManager.LoadScene("Rotation");
            }
        }
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