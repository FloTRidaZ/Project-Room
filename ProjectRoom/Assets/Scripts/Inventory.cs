using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Класс, реализующий работу инвентаря
 * 
 * @author Сотников Р. 17ит17
 */
public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    private List<CurrentItem> cells;
    private int counter = 0;

    [Header("Прицел")]
    public Image aim;

    [Header("Рука")]
    public Transform arm;

    [HideInInspector]
    public GameObject oldObject;
    [HideInInspector]
    public string oldPathToPrefab;
    [HideInInspector]
    public bool inHand;

    void Start()
    {
        oldObject = null;
        oldPathToPrefab = null;
        inHand = false;

        inventoryPanel.SetActive(false);
        cells = new List<CurrentItem>();
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            GameObject currentObj = inventoryPanel.transform.GetChild(i).gameObject;
            cells.Add(currentObj.GetComponent<CurrentItem>());
        }
    }

    void Update()
    {
        ToggleInventory();
		if (Input.GetKeyDown (KeyCode.F5)){
			Save();
		}
		if (Input.GetKeyDown (KeyCode.F9)){
			Load();
		}
    }

    /**
     * Метод для отображения инвентаря и скрытия прицела по нажатию на клавишу Tab
     */
    void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            aim.enabled = !aim.enabled;
        }
    }

    /**
     * Метод для добавления в инвентарь объектов и автоматического удаления их со сцены
     */
    public void AddItem(GameObject obj)
    {
        if (counter == cells.Count)
            return;

        Item content = obj.GetComponent<Item>();
        cells[counter].AddItem(content);
        Destroy(obj);
        counter++;
    }

    /**
     * Метод для сохранения данных о состоянии инвентаря
     */
    private void Save()
    {
		foreach (CurrentItem cell in cells) {
			cell.Save ();
		}
	}

    /**
     * Метод для загрузки сохраненных данных о состоянии инвентаря
     */
    private void Load()
    {
		foreach (CurrentItem cell in cells) {
			cell.Load ();
		}
	}
}