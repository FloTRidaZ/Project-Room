using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
    public string oldPath;
    [HideInInspector]
    public bool inHand;

    void Start()
    {
        oldObject = null;
        oldPath = null;
        inHand = false;

        cells = new List<CurrentItem>();
        inventoryPanel.SetActive(false);
        int count = inventoryPanel.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            GameObject currentObj = inventoryPanel.transform.GetChild(i).gameObject;
            cells.Add(currentObj.GetComponent<CurrentItem>());
        }
    }

    // Update is called once per frame
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
     * Метод для отображения инвентаря и скрытия прицела по нажатию на клавишу <code>showInventory</code>
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
        {
            return;
        }
        Item content = obj.GetComponent<Item>();
        cells[counter].AddItem(content);
        Destroy(obj);
        counter++;
    }

    private void Save (){
		foreach (CurrentItem cell in cells) {
			cell.Save ();
		}
	}

	private void Load (){
		foreach (CurrentItem cell in cells) {
			cell.Load ();
		}
	}
}