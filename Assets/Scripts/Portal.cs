﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    public bool Active = true;

    public string LoadSceneName = "Scene";

    private void OnMouseUp()
    {
        if (Active)
        {
            FindObjectOfType<TouchHandler>().ClearCanTouchObjects(null);
            loadScene();
        }
    }

    private void loadScene()
    {
        //SceneManager.LoadScene(LoadSceneName);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
