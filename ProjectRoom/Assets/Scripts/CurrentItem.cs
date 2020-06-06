using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

/**
 * Класс, реализующий работу выбранного объекта в инвентаре
 *
 * @author Сотников Р. 17ит17
 */
public class CurrentItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static string currentPath;

    [Header("Подсветка")]
    public Sprite cell;
    public Sprite activeCell;
    public Sprite highlightedCell;
    Image imgSprite;

    private GameObject inventoryObject;
    private Inventory inventory;
	private DataHolder data;


    public void AddItem(Item content)
	{
		data = new DataHolder (content);
		Image img = transform.GetChild (0).GetComponent<Image> ();
		img.sprite = Resources.Load<Sprite> (content.pathToIcon);
		img.enabled = true;
	}

    void Start()
    {
        inventoryObject = GameObject.FindGameObjectWithTag("InventoryManager");
        inventory = inventoryObject.GetComponent<Inventory>();
    }

    void Awake()
    {
		imgSprite = GetComponent<Image>();
    }

    /**
     * Метод, в котором проверяется, если нажата правая кнопка мыши,
     * то загружается сцена с вращением выбранного в инвентаре объекта
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        HighlightACell();

		if (data == null)
			return;
		Buffer.prefPath = data.pathToPrefab;
		SceneManager.LoadScene (2);
    }

    /**
     * После закрытия инвентаря у всех ячеек выключается подсветка
     * Выделение выбранной ячейки по клику на неё и, 
     * если кликнуть по выбранной ячейке ещё раз, то выделение снимется
     */
    private void HighlightACell()
    {
        if (imgSprite.sprite == highlightedCell)
        {
            imgSprite.sprite = cell;
        }
        else
        {
            inventory.inventoryPanel.SetActive(false);
            inventory.inventoryPanel.SetActive(true);
            imgSprite.sprite = highlightedCell;
        }
    }

    /**
     * Если положение курсора совпадает с ячейкой инвентаря и 
     * при этом, курсор не затрагивает выбранную ячейку, то включается ее подсветка
     */
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (imgSprite.sprite != highlightedCell)
        {
            imgSprite.sprite = activeCell;
        }
    }

    /**
     * Если положение курсора не совпадает с ячейкой инвентаря и 
     * при этом, курсор не затрагивает выбранную ячейку, то подсветка выключается
     */
    public void OnPointerExit(PointerEventData eventData)
    {
        if (imgSprite.sprite != highlightedCell)
        {
            imgSprite.sprite = cell;
        }
    }

    /**
     * После закрытия инвентаря у всех ячеек, с учётом выбранной ячейки, выключается подсветка
     */
    private void OnDisable()
    {
        imgSprite.sprite = cell;
    }

	public class DataHolder {
		public string itemName, pathToPrefab, pathToIcon;

		public DataHolder (Item item){
			itemName = item.itemName;
			pathToPrefab = item.pathToPrefab;
			pathToIcon = item.pathToIcon;
		}
	}
}