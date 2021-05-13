using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basement : MonoBehaviour
{
    public float time = 2;
    private float start;
    // Start is called before the first frame update
    void Start()
    {
        start = Time.fixedTime;
        print("Welcome to the end");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime > time + start) UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
