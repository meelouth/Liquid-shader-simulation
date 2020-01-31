using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringOut : MonoBehaviour
{
    [SerializeField] private Transform pourPoint;

    [SerializeField] private GameObject pourEffect;


    private Transform pouringObjectTransform;

    private GameObject currentPourEffect;

    private IPouring pouring;

    void Start()
    {
        pouringObjectTransform = transform;

        pouring = GetComponent<IPouring>();
        
    }
    
    void Update()
    {
        if (pouring.IsPouring())
        {
            PourOut();
            return;
        }

        StopPouring();
    }

    private void PourOut()
    {
        if (!currentPourEffect)
            currentPourEffect = Instantiate(pourEffect, pourPoint);
    }

    private void StopPouring()
    {
        Destroy(currentPourEffect);
    }
}
