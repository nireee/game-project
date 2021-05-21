﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoading : MonoBehaviour
{
    public Transform Screen;
    public float LoadTime = 1;
    private float loadingStart;
    public TouchHandler TouchH;
    private bool InventoryLoaded = false;
    // Start is called before the first frame update
    void Start()
    {
        Screen.gameObject.SetActive(true);
        loadingStart = Time.fixedTime;
        TouchH.ClearCanTouchObjects(null);
    }

    // Update is called once per frame
    void Update()
    {
        if(Screen.gameObject.activeSelf && loadingStart + LoadTime < Time.fixedTime)
        {
            Screen.gameObject.SetActive(false);
            FindObjectOfType<TouchHandler>().ClearCanTouchObjects();
        }
        else if (!InventoryLoaded && FindObjectOfType<Inventory>())
        {
            if (!FindObjectOfType<Basement>()) FindObjectOfType<Inventory>().LoadInventory();
            else
            {
                Destroy(FindObjectOfType<Inventory>().gameObject);
                Inventory.StaticInventory = null;
            }
            InventoryLoaded = true;
        }
    }
}
