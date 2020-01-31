using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringOut : MonoBehaviour
{
    [SerializeField] private Transform pourPoint;

    [SerializeField] private GameObject pourEffect;

    [SerializeField] private float angleToPour = 30;

    [SerializeField] private float speed = 1f;


    private Transform pouringObjectTransform;

    private GameObject currentPourEffect;
    void Start()
    {
        pouringObjectTransform = transform;
    }
    
    void Update()
    {
        if (CalculateAngle() < angleToPour)
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

    private float CalculateAngle()
    {
        return pouringObjectTransform.up.y * Mathf.Rad2Deg;
    }
}
