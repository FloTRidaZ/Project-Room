using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}