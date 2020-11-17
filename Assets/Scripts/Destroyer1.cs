using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer1 : MonoBehaviour
{
    //private BoxCollider cubeCol;
    private MeshRenderer cubeMesh;
    //private GameObject cubes;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("floors"))
        {
            cubeMesh = other.gameObject.GetComponent<MeshRenderer>();
            cubeMesh.enabled = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("floors"))
        {
            cubeMesh = other.gameObject.GetComponent<MeshRenderer>();
            cubeMesh.enabled = true;
        }
    }
}