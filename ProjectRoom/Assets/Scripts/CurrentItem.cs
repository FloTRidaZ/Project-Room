using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
	public DataHolder data;

    string savePath;
    InventorySaveManager manager;

    public void AddItem(Item content)
	{
		data = new DataHolder(content);
        Image img = transform.GetChild (0).GetComponent<Image>();
        img.sprite = Resources.Load<Sprite> (content.pathToIcon);
		img.enabled = true;
    }

    void Start()
    {
        //savePath = Application.persistentDataPath + "/save" + ".gamesave";
        inventoryObject = GameObject.FindGameObjectWithTag("InventoryManager");
        inventory = inventoryObject.GetComponent<Inventory>();
    }

    void Awake()
    {
		imgSprite = GetComponent<Image>();
        savePath = Application.persistentDataPath + "/save" + name + ".gamesave";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Load();
        }
    }

    /**
     * Метод, в котором проверяется, если нажата правая кнопка мыши,
     * то загружается сцена с вращением выбранного в инвентаре объекта
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        HighlightACell();

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameObject droppedObject = Instantiate(Resources.Load<GameObject>(data.pathToPrefab));
            droppedObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
            Image img = transform.GetChild(0).GetComponent<Image>();
            img.enabled = false;
        }
        else
        {

            if (data == null)
                return;
            Buffer.prefPath = data.pathToPrefab;
            SceneManager.LoadScene(2);
        }
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

    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(savePath, FileMode.Create);

        manager = new InventorySaveManager();
        manager.Save(gameObject);
        bf.Serialize(fs, manager);
        fs.Close();
        Debug.Log("Сохранено1");
        }

    private void Load()
    {
        if (!File.Exists(savePath))
        {
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(savePath, FileMode.Open);
        manager = (InventorySaveManager)bf.Deserialize(fs);
        fs.Close();
        Debug.Log("Подготовка1");

        RestoreData();
    }

    private void RestoreData()
    {
        float posX = manager.saveData.pos.x;
        float posY = manager.saveData.pos.y;
        float posZ = manager.saveData.pos.z;

        float dirX = manager.saveData.dir.x;
        float dirY = manager.saveData.dir.y;
        float dirZ = manager.saveData.dir.z;

        Vector3 position = new Vector3(posX, posY, posZ);
        Vector3 forward = new Vector3(dirX, dirY, dirZ);

        string itemName = manager.saveData.itemCell.itemName;
        string pathToIcon = manager.saveData.itemCell.pathToIcon;
        string pathToPrefab = manager.saveData.itemCell.pathToPrefab;

        Item item = new Item
        {
            itemName = itemName,
            pathToIcon = pathToIcon,
            pathToPrefab = pathToPrefab
        };
        DataHolder dataHolder = new DataHolder(item);
        this.data = dataHolder;

        transform.localPosition = position;
        transform.forward = forward;
        Debug.Log("Загружено1");
    }
}