using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.onPlayGame += OnGamestarted;
    }

  

    private void OnDisable()
    {
        EventManager.onPlayGame -= OnGamestarted;
    }


    private void OnGamestarted()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

   
}
