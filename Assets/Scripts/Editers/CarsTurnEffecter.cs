using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarsTurnEffecter : MonoBehaviour
{
    public bool turnleft, turnright, turning;
    public float maxTurnAngle = 10;
    private Quaternion originalrotation;
    RoadStateManager roadStateManager;

    private void OnEnable()
    {
        EventManager.onTurnLeft += TurnLeftEvent;
        EventManager.onTurnRight += TurnRightEvent;
        EventManager.onTurnEnded += TurnEndingEvent;
    }

    private void OnDisable()
    {
        EventManager.onTurnLeft -= TurnLeftEvent;
        EventManager.onTurnRight -= TurnRightEvent;
        EventManager.onTurnEnded -= TurnEndingEvent;
    }

    private void Start()
    {
        originalrotation = gameObject.transform.rotation;
        roadStateManager = FindObjectOfType<RoadStateManager>();
    }

    private void Update()
    {
        if (turnleft && !turnright && !turning)
        {
            float randommize = Random.Range(-2, +2);
            float time = Random.Range(.1f, .3f);
            turning = true;
            Vector3 turnleft = new Vector3(0, -maxTurnAngle + randommize, 0);
            gameObject.transform.DOLocalRotate(turnleft, .3f).SetEase(Ease.Linear);
        }
        else if (!turnleft && turnright && !turning)
        {
            float randommize = Random.Range(-2, +2);
            float time = Random.Range(.1f, .3f);
            turning = true;
            Vector3 turnright = new Vector3(0, maxTurnAngle + randommize, 0);
            gameObject.transform.DOLocalRotate(turnright, .3f).SetEase(Ease.Linear);
        }

    }



    void TurnLeftEvent()
    {
        if (roadStateManager.maxLeftSideMovement != 0)
        {
            turnleft = true;
            turnright = false;
        }

    }

    void TurnRightEvent()
    {
        if (roadStateManager.maxrightSideMovement! != 0)
        {
            turnleft = false;
            turnright = true;
        }
    }

    void TurnEndingEvent()
    {
        if (turnleft || turnright)
        {
            turning = false;
            turnleft = false;
            turnright = false;
            float time = Random.Range(.1f, .3f);
            Vector3 targetvalue = new Vector3(0, 0, 0);
            gameObject.transform.DOLocalRotate(targetvalue, .3f).SetEase(Ease.Linear);
        }
      
    }
}
