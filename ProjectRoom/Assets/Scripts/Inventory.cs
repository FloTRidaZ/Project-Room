using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/**
 * Класс, реализующий работу инвентаря
 * 
 * @author Сотников Р. 17ит17
 */
public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    private List<CurrentItem> cells;
    private int counter = 0;

    [Header("Прицел")]
    public Image aim;

    //string savePath;
    //InventorySaveManager manager;

    // Use this for initialization
    void Start()
    {
        //savePath = Application.persistentDataPath + "/save" + name + ".gamesave";

        cells = new List<CurrentItem>();
        inventoryPanel.SetActive(false);
        int count = inventoryPanel.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            GameObject currentObj = inventoryPanel.transform.GetChild(i).gameObject;
            cells.Add(currentObj.GetComponent<CurrentItem>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        ToggleInventory();

        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    Save();
        //}

        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    Load();
        //}
    }

    /**
     * Метод для отображения инвентаря и скрытия прицела по нажатию на клавишу <code>showInventory</code>
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
        {
            return;
        }
        Item content = obj.GetComponent<Item>();
        cells[counter].AddItem(content);
        Destroy(obj);
        counter++;
    }

    //private void Save()
    //{
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream fs = new FileStream(savePath, FileMode.Create);

    //    manager = new InventorySaveManager();
    //    manager.Save(gameObject);
    //    bf.Serialize(fs, manager);
    //    fs.Close();
    //    Debug.Log("Сохранено1");
    //}

    //private void Load()
    //{
    //    if (!File.Exists(savePath))
    //    {
    //        return;
    //    }

    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream fs = new FileStream(savePath, FileMode.Open);
    //    manager = (InventorySaveManager)bf.Deserialize(fs);
    //    fs.Close();
    //    Debug.Log("Подготовка1");

    //    RestoreData();
    //}

    //private void RestoreData()
    //{
    //    float posX = manager.saveData.pos.x;
    //    float posY = manager.saveData.pos.y;
    //    float posZ = manager.saveData.pos.z;

    //    float dirX = manager.saveData.dir.x;
    //    float dirY = manager.saveData.dir.y;
    //    float dirZ = manager.saveData.dir.z;

    //    Vector3 position = new Vector3(posX, posY, posZ);
    //    Vector3 forward = new Vector3(dirX, dirY, dirZ);

    //    Data
    //    //cells[counter] = manager.saveData.

    //    transform.localPosition = position;
    //    transform.forward = forward;
    //    Debug.Log("Загружено1");
    //}
}