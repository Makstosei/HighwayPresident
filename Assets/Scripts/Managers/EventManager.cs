using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    #region Singleton

    private static EventManager _instance;

    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<EventManager>();
            return _instance;
        }
    }
    #endregion

    public static Action onTurnRight;
    public static Action onTurnLeft;
    public static Action onTurnEnded;
    public static Action onHitObstacle;

    public void TurnRightEvent()
    {
        onTurnRight.Invoke();
    }

    public void TurnLeftEvent()
    {
        onTurnLeft.Invoke();
    }

    public void TurnEndedEvent()
    {
        onTurnEnded.Invoke();
    }

}
