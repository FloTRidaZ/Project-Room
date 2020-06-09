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
    List<CurrentItem> cells;
    int counter = 0;

    [Header("Прицел")]
    public Image aim;

    [Header("Рука")]
    public Transform arm;

    GameObject oldObject;
    string oldPathToPrefab;
    bool inHand;

    void Start()
    {
        inHand = false;

        inventoryPanel.SetActive(false);
        cells = new List<CurrentItem>();
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            cells.Add(inventoryPanel.transform.GetChild(i).gameObject.GetComponent<CurrentItem>());
        }
    }

    void Update()
    {
        ToggleInventory();

		if (Input.GetKeyDown (KeyCode.F5))
        {
			Save();
		}
		if (Input.GetKeyDown (KeyCode.F9))
        {
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
     * Отображение/скрытие в руке выбранного в инвентаре объекта
     */
    public void TakeInHands(string pathToPrefab)
    {
        if (!inHand || oldPathToPrefab != pathToPrefab)
        {
            Destroy(oldObject);
            GameObject obj = Instantiate(Resources.Load<GameObject>(pathToPrefab));
            obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            obj.transform.SetPositionAndRotation(arm.position, arm.rotation);
            obj.transform.SetParent(arm);
            obj.GetComponent<Rigidbody>().isKinematic = true;
            obj.GetComponent<Collider>().enabled = false;
            oldObject = obj;
            oldPathToPrefab = pathToPrefab;
            inHand = true;
        }
        else
        {
            Destroy(oldObject);
            inHand = false;
        }
    }

    /**
     * Метод для сохранения данных о состоянии инвентаря
     */
    private void Save()
    {
		foreach (CurrentItem cell in cells)
        {
			cell.Save ();
		}
	}

    /**
     * Метод для загрузки сохраненных данных о состоянии инвентаря
     */
    private void Load()
    {
		foreach (CurrentItem cell in cells)
        {
			cell.Load ();
		}
	}
}