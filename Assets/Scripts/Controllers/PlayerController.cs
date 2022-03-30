using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private RoadStateManager playerRoadStateManager;

    public bool isGameStarted = false;
    public bool isMoving;

    public float swipeRange;
    public float tapRange;
    public Vector2 firstPressPos;
    private Vector2 currentPos;
    private bool stopTouch = false;

    private void OnEnable()
    {
        EventManager.onTurnEnded += TurnEndingEvent;
    }

    private void OnDisable()
    {
        EventManager.onTurnEnded -= TurnEndingEvent;
    }

    private void Awake()
    {
        playerRoadStateManager = GetComponent<RoadStateManager>();
    }
    private void Update()
    {
        MoveCheck();
    }

    public void MoveCheck()
    {

        if (Input.GetMouseButtonDown(0))
        {
            stopTouch = false;
            firstPressPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            currentPos = Input.mousePosition;
            Vector2 Distance = firstPressPos - currentPos;
            if (!stopTouch)
            {
                if (Distance.x < -swipeRange)
                {
                    if (!isMoving)
                    {
                        isMoving = true;
                        EventManager.Instance.TurnRightEvent();
                    }
                }
                else if (Distance.x > swipeRange)
                {
                    if (!isMoving)
                    {
                        isMoving = true;
                        EventManager.Instance.TurnLeftEvent();
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            stopTouch = true;
        }
    }



    void TurnEndingEvent()
    {
        isMoving = false;
    }

}
