using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeObject : MonoBehaviour
{
    [Header("Начальный размер объекта")]
    public Vector3 startScale;

    [Header("Рука")]
    public Transform arm;

    [Header("Кнопка взаимодействия")]
    public KeyCode takeAnObject;
    public KeyCode removeObject;

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(takeAnObject))
        {
            transform.localScale = new Vector3(0.4560662f, 0.4560662f, 0.4560672f);
            transform.SetPositionAndRotation(arm.position, arm.rotation);
            transform.SetParent(arm);
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(removeObject))
        {
            GetComponent<Rigidbody>().isKinematic = false;
            transform.parent = null;
            transform.localScale = startScale;
        }
    }
}
