using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoadStateManager : MonoBehaviour
{
    public List<GameObject> Roads;
    public int currentline;
    public float sidemovespeed;
    public float maxLeftSideMovement, maxrightSideMovement;
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
                maxLeftSideMovement = 0f;
                maxrightSideMovement = -3.6f;
                break;
            case 1:
                playerPathFollower.pathCreator = Roads[1].GetComponent<PathCreation.PathCreator>();
                maxLeftSideMovement = 0.9f;
                maxrightSideMovement = -2.7f;
                break;
            case 2:
                playerPathFollower.pathCreator = Roads[2].GetComponent<PathCreation.PathCreator>();
                maxLeftSideMovement = 1.8f;
                maxrightSideMovement = -1.8f;
                break;
            case 3:
                playerPathFollower.pathCreator = Roads[3].GetComponent<PathCreation.PathCreator>();
                maxLeftSideMovement = 2.7f;
                maxrightSideMovement = -0.9f;
                break;
            case 4:
                playerPathFollower.pathCreator = Roads[4].GetComponent<PathCreation.PathCreator>();
                maxLeftSideMovement = 3.6f;
                maxrightSideMovement = 0f;
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
            targetz = player.transform.position.z - targetchangevalue;
            //player.transform.DOMoveZ(targetz, sidemovespeed).SetEase(Ease.InOutSine);
            player.transform.DOBlendableMoveBy(new Vector3(Roads[currentline].GetComponent<PathCreation.Examples.PathFollower>().pathCreator.path.GetClosestPointOnPath(player.transform.position).x,
                                                        Roads[currentline].GetComponent<PathCreation.Examples.PathFollower>().pathCreator.path.GetClosestPointOnPath(player.transform.position).y,
                                                        targetz), sidemovespeed).SetEase(Ease.InOutSine);
            yield return new WaitForSecondsRealtime(sidemovespeed);
            currentline = Mathf.RoundToInt(currentline - Time.deltaTime);
            player.GetComponent<PlayerController>().isMoving = false;
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
            //player.transform.DOMoveZ(targetz, sidemovespeed).SetEase(Ease.InOutSine);
            player.transform.DOBlendableMoveBy(new Vector3(Roads[currentline].GetComponent<PathCreation.PathCreator>().path.GetClosestPointOnPath(player.transform.position).x,
                                                           Roads[currentline].GetComponent<PathCreation.PathCreator>().path.GetClosestPointOnPath(player.transform.position).y,
                                                           targetz), sidemovespeed).SetEase(Ease.InOutSine);
            yield return new WaitForSecondsRealtime(sidemovespeed);
            currentline = Mathf.RoundToInt(currentline + Time.deltaTime);
            player.GetComponentInChildren<PlayerController>().isMoving = false;
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
