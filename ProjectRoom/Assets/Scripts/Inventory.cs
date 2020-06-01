using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public List<Item> items;
    public GameObject cellContainer;
    public KeyCode showInventory;
	public Image aim;

    // Use this for initialization
    void Start()
    {
        cellContainer.SetActive(false);
        items = new List<Item>();

        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            cellContainer.transform.GetChild(i).GetComponent<CurrentItem>().index = i;
        }

        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            items.Add(new Item());
        }
    }

    // Update is called once per frame
	void Update(){
        ToggleInventory();
    }

	public void addItem(GameObject obj){
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

    void ToggleInventory()
    {
        if (Input.GetKeyDown(showInventory))
        {
            cellContainer.SetActive(!cellContainer.activeSelf);
			aim.enabled = !aim.enabled;
        }
    }

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
                img.sprite = Resources.Load<Sprite>(items[i].pathIcon);
            }
            else
            {
                img.enabled = false;
                img.sprite = null;
            }
        }
    }
}