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
    [HideInInspector]
    public List<Item> items;

    [Header("Контейнер ячеек")]
    public GameObject cellContainer;

    [Header("Прицел")]
	public Image aim;

    [Header("Кнопка взаимодействия")]
    public KeyCode showInventory;

    // Use this for initialization
    void Start()
    {
        cellContainer.SetActive(false);
        items = new List<Item>();

        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            items.Add(new Item());
            cellContainer.transform.GetChild(i).GetComponent<CurrentItem>().index = i;
        }
    }

    // Update is called once per frame
	void Update(){
        ToggleInventory();
    }

    /**
     * Метод для отображения инвентаря и скрытия прицела по нажатию на клавишу <code>showInventory</code>
     */
    void ToggleInventory()
    {
        if (Input.GetKeyDown(showInventory))
        {
            cellContainer.SetActive(!cellContainer.activeSelf);
            aim.enabled = !aim.enabled;
        }
    }

    /**
     * Метод для добавления в инвентарь объектов и автоматического удаления их со сцены
     */
    public void AddItemToInventory(GameObject obj){
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].id == 0)
			{
				items[i] = obj.GetComponent<Item> ();
				DisplayItems();
				Destroy(obj);
				break;
			}
		}
    }

    /**
     * Метод, отображающий добавленные объекты в инвентаре
     */
    public void DisplayItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Transform cell = cellContainer.transform.GetChild(i);
            Transform icon = cell.GetChild(0);
            Image img = icon.GetComponent<Image>();
            if (items[i].id != 0)
            {
                img.enabled = true;
                img.sprite = Resources.Load<Sprite>(items[i].pathToIcon);
            }
            else
            {
                img.enabled = false;
                img.sprite = null;
            }
        }
    }
}