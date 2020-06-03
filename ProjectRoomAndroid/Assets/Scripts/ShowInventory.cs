using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowInventory : MonoBehaviour, IPointerClickHandler
{
    [Header("Прицел")]
    public Image aim;

    GameObject inventoryObject;
    Inventory inventory;

    // Use this for initialization
    void Start () {
        inventoryObject = GameObject.FindGameObjectWithTag("InventoryManager");
        inventory = inventoryObject.GetComponent<Inventory>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        inventory.cellContainer.SetActive(!inventory.cellContainer.activeSelf);
        aim.enabled = !aim.enabled;
    }
}
