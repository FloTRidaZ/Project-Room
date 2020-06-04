using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/**
 * Класс для управления игроком 
 * и камерой по осям X и Y
 * 
 * @author Лисова Анастасия, 17ИТ17
 */
public class PlayerController : MonoBehaviour {
    public GameObject cam;
    Quaternion StartingRotation;
    float Ver, Hor, RotHor, RotVer;
    readonly float Speed = 2;
    string savePath;
    PlayerSaveManager manager;

    private void Start() {
        StartingRotation = transform.rotation;
        savePath = Application.persistentDataPath + "/save" + ".gamesave";
    }

    private void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(savePath, FileMode.Create);

        manager = new PlayerSaveManager();
        manager.Save(gameObject);
        bf.Serialize(fs, manager);
        fs.Close();
    }

    private void Load() {
        if (!File.Exists(savePath)) {
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(savePath, FileMode.Open);

        manager = (PlayerSaveManager)bf.Deserialize(fs);
        fs.Close();

        RestoreData();
    }

    private void RestoreData() {
        float quatX = manager.saveData.Rotation.x;
        float quatY = manager.saveData.Rotation.y;
        float quatZ = manager.saveData.Rotation.z;
        float quatW = manager.saveData.Rotation.w;

        float posX = manager.saveData.position.x;
        float posY = manager.saveData.position.y;
        float posZ = manager.saveData.position.z;

        float dirX = manager.saveData.direction.x;
        float dirY = manager.saveData.direction.y;
        float dirZ = manager.saveData.direction.z;

        Quaternion Rot = new Quaternion(quatX, quatY, quatZ, quatW);
        Vector3 position = new Vector3(posX, posY, posZ);
        Vector3 forward = new Vector3(dirX, dirY, dirZ);

        transform.rotation = Rot;
        transform.position = position;
        transform.forward = forward;
    }

    public void FixedUpdate() {
        RotHor += Input.GetAxis("Mouse X") * Speed;
        RotVer += Input.GetAxis("Mouse Y") * Speed;

        RotVer = Mathf.Clamp(RotVer, -60, 60);

        Quaternion RotY = Quaternion.AngleAxis(RotHor, Vector3.up);
        Quaternion RotX = Quaternion.AngleAxis(-RotVer, Vector3.right);

        transform.rotation = StartingRotation * RotY;
        cam.transform.rotation = StartingRotation * transform.rotation * RotX;

        if (Input.GetKey(KeyCode.LeftShift)) {
            Ver = Input.GetAxis("Vertical") * Time.deltaTime * Speed * 2;
            Hor = Input.GetAxis("Horizontal") * Time.deltaTime * Speed * 2;
        }
        else {
            Ver = Input.GetAxis("Vertical") * Time.deltaTime * Speed;
            Hor = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
        }

        transform.Translate(new Vector3(Hor, 0, Ver));

        if (Input.GetKey(KeyCode.F5)) {
            Save();
        }

        if (Input.GetKey(KeyCode.F9)) {
            Load();
        }
    }
}
