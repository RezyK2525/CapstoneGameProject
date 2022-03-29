using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    public Transform cam;


    private void LateUpdate()
    {
        cam = GameManager.instance.cam.transform;
        transform.LookAt(transform.position + cam.forward);
    }
}
