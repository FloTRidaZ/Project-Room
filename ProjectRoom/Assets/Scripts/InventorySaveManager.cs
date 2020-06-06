using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySaveManager
{
    public SaveData saveData;

    [System.Serializable]
    public struct Vec3
    {
        public float x, y, z;

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    [System.Serializable]
    public struct ItemCell
    {
        public string itemName, pathToIcon, pathToPrefab;

        public ItemCell(string itemName, string pathToIcon, string pathToPrefab)
        {
            this.itemName = itemName;
            this.pathToIcon = pathToIcon;
            this.pathToPrefab = pathToPrefab;
        }
    }

    [System.Serializable]
    public struct SaveData
    {
        public Vec3 pos, dir;
        //public ItemCell itemName, pathToIcon, pathToPrefab;
        public ItemCell itemCell;

        public SaveData(Vec3 pos, Vec3 dir, ItemCell itemCell)
        {
            this.pos = pos;
            this.dir = dir;
            this.itemCell = itemCell;
            //this.itemName = itemName;
            //this.pathToIcon = pathToIcon;
            //this.pathToPrefab = pathToPrefab;
        }
    }

    public void Save(GameObject obj)
    {
        Vector3 localPos = obj.transform.position;
        Vec3 pos = new Vec3(localPos.x, localPos.y, localPos.z);
        Vector3 forward = obj.transform.forward;
        Vec3 dir = new Vec3(forward.x, forward.y, forward.z);

        Item item = obj.GetComponent<Item>();
        ItemCell itemCell = new ItemCell(item.itemName, item.pathToIcon, item.pathToPrefab);

        saveData = new SaveData(pos, dir, itemCell);
    }
}