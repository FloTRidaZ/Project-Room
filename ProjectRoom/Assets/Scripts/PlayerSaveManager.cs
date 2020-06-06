using UnityEngine;

/**
 * Класс для сохранения расположения
 * игрока в пространстве, его 
 * угол поворота и направление
 * 
 * @author Лисова Анастасия, 17ИТ17
 */
[System.Serializable]
public class PlayerSaveManager {
    public SaveData saveData;

    [System.Serializable]
    public struct Quat {
        public float x, y, z, w;

        public Quat(float x, float y, float z, float w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }

    [System.Serializable]
    public struct Vec3 {
        public float x, y, z;

        public Vec3(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    [System.Serializable]
    public struct SaveData {
        public Quat Rotation;
        public Vec3 position, direction;

        public SaveData(Quat Rot, Vec3 pos, Vec3 dir) {
            Rotation = Rot;
            position = pos;
            direction = dir;
        }
    }

    public void Save(GameObject obj) {
        Quaternion Rot = obj.transform.rotation;
        Quat Rotation = new Quat(Rot.x, Rot.y, Rot.z, Rot.w);

        Vector3 localPos = obj.transform.position;
        Vec3 pos = new Vec3(localPos.x, localPos.y, localPos.z);

        Vector3 forward = obj.transform.forward;
        Vec3 dir = new Vec3(forward.x, forward.y, forward.z);

        saveData = new SaveData(Rotation, pos, dir);
    }
}

