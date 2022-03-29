using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    public Transform cam = GameManager.instance.cam.transform;


    private void LateUpdate()
    {

        transform.LookAt(transform.position + cam.forward);
    }
}
