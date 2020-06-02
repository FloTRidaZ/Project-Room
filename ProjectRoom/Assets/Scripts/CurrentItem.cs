using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

/**
 * Класс, реализующий работу выбранного объекта в инвентаре
 *
 * @author Сотников Р. 17ит17
 */
public class CurrentItem : MonoBehaviour, IPointerClickHandler
{
	public static string currentPath;

    [HideInInspector]
    public int index;
    GameObject inventoryObject;
    Inventory inventory;

    // Use this for initialization
    void Start()
    {
        inventoryObject = GameObject.FindGameObjectWithTag("InventoryManager");
        inventory = inventoryObject.GetComponent<Inventory>();
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
}