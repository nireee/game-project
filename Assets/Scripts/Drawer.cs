using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public NumberLock nl;
    public GameObject DrawerInside;
    public GameObject LockMesh;
    private void OnMouseDown()
    {
        if (FindObjectOfType<TouchHandler>().CanTouch(gameObject))
        {
            if(nl.Completed == true)
            {
                //open drawer
                DrawerInside.SetActive(true);
                GetComponent<BoxCollider2D>().enabled = false;
                LockMesh.SetActive(false);

            }
        }
    }
}
