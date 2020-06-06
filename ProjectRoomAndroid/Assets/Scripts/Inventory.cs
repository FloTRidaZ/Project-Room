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
	private int counter = 0;
	private int cellCount;

    // Use this for initialization
    void Start()
    {
		inventoryPanel.SetActive(false);
		cellCount = inventoryPanel.transform.childCount;
    }

    /**
     * Метод для добавления в инвентарь объектов и автоматического удаления их со сцены
     */
    public void AddItem(GameObject obj)
    {
		if (counter == cellCount){
			return;
		}
		Item content = obj.GetComponent<Item>();
		inventoryPanel.transform.GetChild (counter).GetComponent<CurrentItem> ().AddItem (content);
        Destroy(obj);
		counter++;
    }
}