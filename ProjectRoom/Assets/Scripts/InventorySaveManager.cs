using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySaveManager
{
    public SaveData saveData;
	public bool isEmpty;

    [System.Serializable]
    public struct SaveData
    {
        public string itemName, pathToIcon, pathToPrefab;

		public SaveData(CurrentItem.DataHolder data)
        {
			this.itemName = data.itemName;
			this.pathToIcon = data.pathToIcon;
			this.pathToPrefab = data.pathToPrefab;
        }
    }

	public void Save(CurrentItem.DataHolder data)
    {
		if (data == null) {
			isEmpty = true;
			return;
		}
		saveData = new SaveData(data);
		isEmpty = false;
	}
}