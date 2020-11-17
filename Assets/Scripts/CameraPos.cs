using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    private Camera cam;

    private int zPos;
    private int xPos;

    private float camXPos = 0f;
    private float camYPos;
    private float camZPos;
    
    private bool isMapArray;

    private void Awake()
    {
        cam = Camera.main;
        xPos = FindObjectOfType<GameManager>().xLine;
        zPos = FindObjectOfType<GameManager>().zLine;
        isMapArray = FindObjectOfType<GameManager>().doMapArray;
        if (zPos > xPos)
        {
            camYPos = zPos * 2f;
            camZPos = -camYPos * 0.5f ;
        }
        else
        {
            camYPos = xPos + zPos;
            camZPos = (xPos + zPos) * -0.5f ;
        }
        
    }

    void Start()
    {
        if (!isMapArray)
        {
            if (zPos < 9)
            {
                for (int i = 0; i < 9 - zPos; i++)
                {
                    camXPos -= 0.5f;
                }
            }

            if (zPos > 9)
            {
                for (int i = zPos - 9; i > 0; i--)
                {
                    camXPos += 0.5f;
                }
            }

            cam.transform.position = new Vector3(camXPos, camYPos, camZPos);
        }
    }
}