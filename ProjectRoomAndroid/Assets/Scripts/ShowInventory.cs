using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowInventory : MonoBehaviour, IPointerClickHandler
{
	public GameObject touchManager;
	public GameObject inventoryPanel;

    /**
     * Отображение/скрытие инвентаря по клику на панель, расположенную в правом нижнем углу
     */
    public void OnPointerClick(PointerEventData eventData)
    {
		inventoryPanel.SetActive(!inventoryPanel.activeSelf);
		touchManager.SetActive (!touchManager.activeSelf);
    }
}
