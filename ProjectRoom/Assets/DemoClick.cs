using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoClick : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject item = hit.collider.gameObject;
                if (item != null)
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}