using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoadStateManager : MonoBehaviour
{
    public List<GameObject> Roads;
    public int currentline;
    public float sidemovespeed;
    public PathCreation.Examples.PathFollower playerPathFollower;
    private GameObject player;
    private float targetz;
    private float targetchangevalue;
    private bool ismoving;


    private void OnEnable()
    {
        EventManager.onTurnLeft += turnLeftEvent;
        EventManager.onTurnRight += turnRightEvent;
        EventManager.onTurnEnded += turnEndingEvent;
    }

    private void OnDisable()
    {
        EventManager.onTurnLeft -= turnLeftEvent;
        EventManager.onTurnRight -= turnRightEvent;
        EventManager.onTurnEnded -= turnEndingEvent;
    }

    void Start()
    {
        currentline = 2;
        playerPathFollower = GameObject.Find("Player").GetComponent<PathCreation.Examples.PathFollower>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        CurrentLine();
    }


    void CurrentLine()
    {
        switch (currentline)
        {
            case 0:
                playerPathFollower.pathCreator = Roads[0].GetComponent<PathCreation.PathCreator>();
                break;
            case 1:
                playerPathFollower.pathCreator = Roads[1].GetComponent<PathCreation.PathCreator>();
                break;
            case 2:
                playerPathFollower.pathCreator = Roads[2].GetComponent<PathCreation.PathCreator>();
                break;
            case 3:
                playerPathFollower.pathCreator = Roads[3].GetComponent<PathCreation.PathCreator>();
                break;
            case 4:
                playerPathFollower.pathCreator = Roads[4].GetComponent<PathCreation.PathCreator>();
                break;


            default:
                break;
        }
    }

    IEnumerator MoveleftRoutine()
    {
        ismoving = true;
        targetchangevalue = 0.9f;
        yield return new WaitForSecondsRealtime(0.1f);
        if (currentline-- <= -1)
        {
            currentline = 0;
        }
        else
        {
            targetz = player.transform.position.z + targetchangevalue;
            player.transform.DOMoveZ(targetz, sidemovespeed).SetEase(Ease.InOutSine);
            yield return new WaitForSecondsRealtime(sidemovespeed);
            EventManager.Instance.TurnEndedEvent();
            currentline = Mathf.RoundToInt(currentline - Time.deltaTime);
        }
    }

    IEnumerator MoveRightRoutine()
    {
        ismoving = true;
        targetchangevalue = -0.9f;
        yield return new WaitForSecondsRealtime(0.1f);
        if (currentline++ >= Roads.Count)
        {
            currentline = Roads.Count - 1;
        }
        else
        {
            targetz = player.transform.position.z + targetchangevalue;
            player.transform.DOMoveZ(targetz, sidemovespeed).SetEase(Ease.InOutSine);
            yield return new WaitForSecondsRealtime(sidemovespeed);
            EventManager.Instance.TurnEndedEvent();
            currentline = Mathf.RoundToInt(currentline + Time.deltaTime);
        }
    }

    void turnEndingEvent()
    {
        ismoving = false;
        targetchangevalue = 0;
    }

    void turnRightEvent()
    {
        StartCoroutine(MoveRightRoutine());
    }


    void turnLeftEvent()
    {
        StartCoroutine(MoveleftRoutine());
    }



}
