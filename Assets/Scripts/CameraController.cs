using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public const float iPad = 1.333f;
    public const float iPhoneX = 2.16f;
    public const float iPhone = 1.777f;

    public float iPadCS = 19.16f;
    public float iPhoneXCS = 12.84f;
    public float iPhoneCS = 15.64f;
    // Start is called before the first frame update
    void Start()
    {
        float aspect = Camera.main.aspect;
        if (aspect > iPhoneX) Camera.main.orthographicSize = iPhoneXCS;
        else if (aspect > iPhone) Camera.main.orthographicSize = iPhoneCS;
        else if (aspect > iPad) Camera.main.orthographicSize = iPadCS;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
