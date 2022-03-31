using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideRoadCorrection : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.position = new Vector3(0, -0.01f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
