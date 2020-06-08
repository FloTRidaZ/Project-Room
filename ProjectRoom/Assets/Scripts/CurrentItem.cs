using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
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

    void Start()
    {
        inventoryObject = GameObject.FindGameObjectWithTag("InventoryManager");
        inventory = inventoryObject.GetComponent<Inventory>();
    }

    void Awake()
    {
		imgSprite = GetComponent<Image>();
		savePath = Application.persistentDataPath + "/save" + name + ".gamesave";
    }

    public void AddItem(Item content)
    {
        data = new DataHolder(content);
        Image img = transform.GetChild(0).GetComponent<Image>();
        img.sprite = Resources.Load<Sprite>(content.pathToIcon);
        img.enabled = true;
    }

    /**
     * Метод, в котором осуществляется выделение выбранной ячейки инвентаря, а также проверяется,
     * если нажата левая кнопка мыши, то выбранный предмет кладётся в руку,
     * если нажата правая кнопка мыши, то загружается сцена с вращением выбранного объекта
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        HighlightACell();

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (data == null)
                return;

            TakeInHands();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (data == null)
                return;

            LoadScene();
        }
    }

    /**
     * Отображение/скрытие в руке выбранного в инвентаре объекта
     */
    private void TakeInHands()
    {
        if (!inventory.inHand || inventory.oldPathToPrefab != data.pathToPrefab)
        {
            Destroy(inventory.oldObject);
            GameObject obj = Instantiate(Resources.Load<GameObject>(data.pathToPrefab));
            obj.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            obj.transform.SetPositionAndRotation(inventory.arm.position, inventory.arm.rotation);
            obj.transform.SetParent(inventory.arm);
            obj.GetComponent<Rigidbody>().isKinematic = true;
            obj.GetComponent<Collider>().enabled = false;
            inventory.oldObject = obj;
            inventory.oldPathToPrefab = data.pathToPrefab;
            inventory.inHand = true;
        }
        else
        {
            Destroy(inventory.oldObject);
            inventory.inHand = false;
        }
    }

    /**
     * Загрузка сцены Rotation
     */
    private void LoadScene()
    {
        Buffer.prefPath = data.pathToPrefab;
        SceneManager.LoadScene(2);
    }

    /**
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

	public class DataHolder
    {
		public string itemName, pathToPrefab, pathToIcon;

		public DataHolder (Item item){
			itemName = item.itemName;
			pathToPrefab = item.pathToPrefab;
			pathToIcon = item.pathToIcon;
		}

		public DataHolder (string itemName, string pathToPrefab, string pathToIcon){
			this.itemName = itemName;
			this.pathToPrefab = pathToPrefab;
			this.pathToIcon = pathToIcon;
		}
    }

    /**
     * Метод для сохранения данных о состоянии инвентаря
     */
    public void Save()
	{
        manager = new InventorySaveManager();
		manager.Save (data);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream fs = new FileStream(savePath, FileMode.Create);
        bf.Serialize(fs, manager);
        fs.Close();

	}

    /**
     * Метод для загрузки сохраненных данных о состоянии инвентаря
     */
    public void Load()
    {
        if (!File.Exists(savePath))
            return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(savePath, FileMode.Open);
        manager = (InventorySaveManager) bf.Deserialize(fs);
        fs.Close();
		if (!manager.isEmpty) {
			RestoreData ();
		} else {
			ClearData ();
		}
    }

    /**
     * Восстановление сохраненных данных о состоянии инвентаря
     */
    private void RestoreData()
    {
		data = new DataHolder (manager.saveData.itemName, manager.saveData.pathToPrefab, manager.saveData.pathToIcon);
		Image img = transform.GetChild (0).GetComponent<Image>();
		img.sprite = Resources.Load<Sprite> (data.pathToIcon);
		img.enabled = true;
    }

    /**
     * Очистка данных, если на момент сохранения предметов в инвентаре не было
     */
	private void ClearData ()
    {
		data = null;
		Image img = transform.GetChild (0).GetComponent<Image>();
		img.enabled = false;
	}
}