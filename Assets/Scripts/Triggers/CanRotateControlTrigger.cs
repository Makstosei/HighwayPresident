using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanRotateControlTrigger : MonoBehaviour
{
    public GameObject Path;
    public bool canTurnLeft, canTurnRight;
    public bool isThisEnterTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Guard" || other.tag == "Player" && isThisEnterTrigger)
        {
            Path.GetComponent<RoadCanRotateController>().canRotateLeft = canTurnLeft;
            Path.GetComponent<RoadCanRotateController>().canRotateRight = canTurnRight;

        }
        else if(!isThisEnterTrigger && other.tag == "Player")
        {
            Path.GetComponent<RoadCanRotateController>().canRotateLeft = canTurnLeft;
            Path.GetComponent<RoadCanRotateController>().canRotateRight = canTurnRight;
        }
    }

}
