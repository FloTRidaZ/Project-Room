using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/**
 * Класс, определяющий уникальные свойства каждого объекта
 *
 * @author Сотников Р. 17ит17
 */
public class Item : MonoBehaviour
{
    [Header("Описание объекта")]
    public string itemName;

    [Header("Путь к...")]
    public string pathToIcon;
    public string pathToPrefab;

    string savePath;
    InventorySaveManager manager;

    private void Start()
    {
        savePath = Application.persistentDataPath + "/save" + itemName + ".gamesave";
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

    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(savePath, FileMode.Create);

        manager = new InventorySaveManager();
        manager.Save(gameObject);
        bf.Serialize(fs, manager);
        fs.Close();
        Debug.Log("Сохранено");
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
        Debug.Log("Подготовка");

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

        //string pathToIcon = manager.saveData.pathToIcon;
        //string pathToPrefab = manager.saveData.pathToPrefab;

        Vector3 position = new Vector3(posX, posY, posZ);
        Vector3 forward = new Vector3(dirX, dirY, dirZ);

        transform.position = position;
        transform.forward = forward;

        Buffer.iconPath = pathToIcon;
        Buffer.prefPath = pathToPrefab;
        Debug.Log("Загружено");
    }
}