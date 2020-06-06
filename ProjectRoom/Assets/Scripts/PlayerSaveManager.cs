using UnityEngine;

/**
 * Класс для сохранения расположения
 * игрока в пространстве, его 
 * угол поворота и направление 
 * игрока и камеры
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
        public Quat Rotation, camera;
        public Vec3 position, direction, directionCam;

        public SaveData(Quat Rot, Quat cam, Vec3 pos, Vec3 dir, Vec3 dirCam) {
            Rotation = Rot;
            camera = cam;
            position = pos;
            direction = dir;
            directionCam = dirCam;
        }
    }

    public void Save(GameObject obj, GameObject cam) {
        Quaternion Rot = obj.transform.rotation;
        Quat Rotation = new Quat(Rot.x, Rot.y, Rot.z, Rot.w);

        Quaternion Cam = cam.transform.rotation;
        Quat camera = new Quat(Cam.x, Cam.y, Cam.z, Cam.w);

        Vector3 localPos = obj.transform.position;
        Vec3 pos = new Vec3(localPos.x, localPos.y, localPos.z);

        Vector3 forward = obj.transform.forward;
        Vec3 dir = new Vec3(forward.x, forward.y, forward.z);

        Vector3 forwardCam = cam.transform.forward;
        Vec3 dirCam = new Vec3(forwardCam.x, forwardCam.y, forwardCam.z);

        saveData = new SaveData(Rotation, camera, pos, dir, dirCam);
    }
}

