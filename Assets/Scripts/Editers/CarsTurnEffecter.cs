using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarsTurnEffecter : MonoBehaviour
{
    public float maxTurnAngle;
    private Quaternion originalrotation;
    RoadStateManager roadStateManager;

    private void OnEnable()
    {
        EventManager.onTurnLeft += TurnLeftEvent;
        EventManager.onTurnRight += TurnRightEvent;
        //EventManager.onTurnEnded += TurnEndingEvent;          
    }

    private void OnDisable()
    {
        EventManager.onTurnLeft -= TurnLeftEvent;
        EventManager.onTurnRight -= TurnRightEvent;
       // EventManager.onTurnEnded -= TurnEndingEvent;
    }

    private void Start()
    {
        originalrotation = gameObject.transform.rotation;
        roadStateManager = FindObjectOfType<RoadStateManager>();
    }



    void TurnLeftEvent()
    {
        Vector3 targetvalue = new Vector3(0, 0-maxTurnAngle,0);
        gameObject.transform.DOBlendableLocalRotateBy(targetvalue , roadStateManager.sidemovespeed).SetEase(Ease.InOutSine);
        StartCoroutine(AutoRotateCorrect());
    }

    void TurnRightEvent()
    {
        Vector3 targetvalue = new Vector3(0, 0+ maxTurnAngle, 0);
        gameObject.transform.DOBlendableLocalRotateBy(targetvalue, roadStateManager.sidemovespeed ).SetEase(Ease.InOutSine);
        StartCoroutine(AutoRotateCorrect());
    }

    IEnumerator AutoRotateCorrect()
    {     
        yield return new WaitForSecondsRealtime(roadStateManager.sidemovespeed*2/3);
        gameObject.transform.DOSmoothRewind();
        yield return new WaitForSecondsRealtime(.2f);
        TurnEndingEvent();
    }
    void TurnEndingEvent()
    {
        Vector3 targetvalue = new Vector3(0, 0, 0);
        gameObject.transform.DOBlendableLocalRotateBy(targetvalue, roadStateManager.sidemovespeed  ).SetEase(Ease.OutSine);
    }
}
