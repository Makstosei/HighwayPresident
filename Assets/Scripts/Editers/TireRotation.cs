using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TireRotation : MonoBehaviour
{
    private void Start()
    {
        gameObject.transform.DOLocalRotate(new Vector3(360, 0, 0), 1f, RotateMode.Fast).SetLoops(-1).SetEase(Ease.Linear).SetRelative();
    }

}
