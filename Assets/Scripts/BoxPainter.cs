using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPainter : MonoBehaviour
{
    public Material setColor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("floors"))
        {
            other.gameObject.GetComponent<Renderer>().material = setColor;
        }
    }
}
