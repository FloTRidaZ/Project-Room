﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CurrentItem : MonoBehaviour, IPointerClickHandler
{
	public static string currentPath;

    [HideInInspector]
    public int index;
    GameObject inventoryObject;
    Inventory inventory;

    // Use this for initialization
    void Start()
    {
        inventoryObject = GameObject.FindGameObjectWithTag("InventoryManager");
        inventory = inventoryObject.GetComponent<Inventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // GameObject droppedObject = Instantiate(Resources.Load<GameObject>(inventory.items[index].pathPrefab));
            // droppedObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
            // inventory.items[index] = new Item();
            // inventory.DisplayItems();
			currentPath = inventory.items [index].pathPrefab;
			SceneManager.LoadScene ("Rotation");
        }
    }
}