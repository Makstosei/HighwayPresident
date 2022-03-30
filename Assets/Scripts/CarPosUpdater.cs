using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPosUpdater : MonoBehaviour
{
    public List<Transform> actualChicksList = new List<Transform>();
    public Transform PlayerRef;
    public float chickenSideMovementSpeed;
    private Vector3 chickPosition;
    private bool isPosUpdaterWorking = false;
    private int PosCorrectionCounter;
    private float directionofsetRef;

   

    private void Update()
    {
        if (isPosUpdaterWorking)
        {
            
            ChickenPosUpdate();
        }
    }


    public void ChickenPosUpdate()
    {

        if (actualChicksList.Count != 0)
        {
            for (int i = 1; i <= actualChicksList.Count; i++)
            {
                if (i == 1)
                {
                    chickPosition = actualChicksList[0].localPosition;
                    chickPosition.x = Mathf.Lerp(chickPosition.x, PlayerRef.position.x, chickenSideMovementSpeed * Time.deltaTime);
                    chickPosition.y = Mathf.Lerp(chickPosition.y, PlayerRef.localPosition.y,1);
                    chickPosition.z = Mathf.Lerp(chickPosition.z, directionofsetRef * i, chickenSideMovementSpeed / 2 * Time.deltaTime);
                    if (Mathf.Abs(chickPosition.x - PlayerRef.localPosition.x) <= 0.001)
                    {
                        chickPosition.x = PlayerRef.localPosition.x;
                        
                    }
                    if (Mathf.Abs(chickPosition.z - directionofsetRef * i) <= 0.001)
                    {
                        chickPosition.z = directionofsetRef * i;
                    }

                    actualChicksList[0].localPosition = chickPosition;
                }
                else
                {
                    chickPosition = actualChicksList[i - 1].localPosition;
                    chickPosition.x = Mathf.Lerp(chickPosition.x, actualChicksList[i - 2].position.x, chickenSideMovementSpeed * Time.deltaTime);
                    chickPosition.y = Mathf.Lerp(chickPosition.y, actualChicksList[i - 2].localPosition.y,13);
                    chickPosition.z = Mathf.Lerp(chickPosition.z, directionofsetRef * i, chickenSideMovementSpeed / 4 * Time.deltaTime);
                    if (Mathf.Abs(chickPosition.x - PlayerRef.localPosition.x) <= 0.001)
                    {
                        chickPosition.x = PlayerRef.localPosition.x;
                    }
                    if (Mathf.Abs(chickPosition.z - directionofsetRef * i) <= 0.001)
                    {
                        chickPosition.z = directionofsetRef * i;
                    }

                    actualChicksList[i - 1].localPosition = chickPosition;
                }
            }
        }
        ChickPositionCorrectionChecker();

    }

    public void ChickPositionCorrectionChecker()
    {
        if (actualChicksList.Count != 0 && isPosUpdaterWorking)
        {
            for (int i = 0; i < actualChicksList.Count; i++)
            {
                if (PlayerRef.localPosition.x == actualChicksList[i].localPosition.x
                    && directionofsetRef * actualChicksList.Count == actualChicksList[i].localPosition.z)
                {
                    PosCorrectionCounter++;
                }

                if (PosCorrectionCounter == actualChicksList.Count)
                {
                    isPosUpdaterWorking = false;
                  
                    PosCorrectionCounter = 0;
                }
            }
        }
    }

    public void UpdateChickenPosStarter()
    {
        if (actualChicksList.Count != 0)
        {
            isPosUpdaterWorking = true;
        }
    }



}


