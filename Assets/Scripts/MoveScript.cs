using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    
    private float moveSpeed;
    
    private Rigidbody rb;

    private bool move;

    private Vector3 nextPoss;

    private string caseChanger;

    private BoxCollider boxColl;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxColl = GetComponent<BoxCollider>();
    }

    private void OnCollisionStay(Collision other)
    {
        move = false;
        //rb.velocity = Vector3.zero;
        boxColl.size = Vector3.one;
    }

    private void Update()
    {
        if (move)
        {
            Move();
        }
        else
        {
            SwipeInput();
        }
        moveSpeed = GameManager.moveSpeed;
    }
    
    private void SwipeInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        }
        if(Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
       
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            currentSwipe.Normalize();
 
            //swipe up
            if(currentSwipe.y > 0 && currentSwipe.x > -0.5f &&  currentSwipe.x < 0.5f)
            {
                boxColl.size = new Vector3(0.95f, 0.95f, 0.95f);
                caseChanger = "up";
                move = true;
            }
            //swipe down
            if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                boxColl.size = new Vector3(0.95f, 0.95f, 0.95f);
                caseChanger = "down";
                move = true;
            }
            //swipe left
            if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                boxColl.size = new Vector3(0.95f, 0.95f, 0.9f);
                caseChanger = "left";
                move = true;
            }
            //swipe right
            if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                boxColl.size = new Vector3(0.95f, 0.95f, 0.9f);
                caseChanger = "right";
                move = true;
            }
        }
    }

    private void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            boxColl.size = new Vector3(0.95f, 0.95f, 0.95f);
            caseChanger = "up";
            move = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            boxColl.size = new Vector3(0.95f, 0.95f, 0.95f);
            caseChanger = "down";
            move = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            boxColl.size = new Vector3(0.95f, 0.95f, 0.9f);
            caseChanger = "left";
            move = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            boxColl.size = new Vector3(0.95f, 0.95f, 0.9f);
            caseChanger = "right";
            move = true;
        }
    }

    public void Move()
    {
        switch (caseChanger)
        {
            case "up":
                nextPoss = new Vector3(transform.position.x, 0, transform.position.z + 10f);
                break;
            case "down":
                nextPoss = new Vector3(transform.position.x, 0, transform.position.z - 10f);
                break;
            case "left":
                nextPoss = new Vector3(transform.position.x - 10f, 0, transform.position.z);
                break;
            case "right":
                nextPoss = new Vector3(transform.position.x + 10f, 0, transform.position.z);
                break;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPoss, Time.fixedDeltaTime * moveSpeed);
    }
}