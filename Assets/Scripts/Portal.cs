using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public bool Active = true;
    public string LoadSceneName = "Not_a_valid_name";

    private void OnMouseUp()
    {
        FindObjectOfType<TouchHandler>().ClearCanTouchObjects(null);

        loadScene();
    }

    private void loadScene()
    {
        SceneManager.LoadScene(LoadSceneName);
    }
}
