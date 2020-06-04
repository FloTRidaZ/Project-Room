using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowInventory : MonoBehaviour, IPointerClickHandler
{
	public GameObject touchManager;
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

    /**
     * Отображение/скрытие инвентаря по клику на панель, расположенную в правом нижнем углу
     */
    public void OnPointerClick(PointerEventData eventData)
    {
		inventory.cellContainer.SetActive(!inventory.cellContainer.activeSelf);
		touchManager.SetActive (!touchManager.activeSelf);
    }
}
