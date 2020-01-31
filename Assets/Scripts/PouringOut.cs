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

    [SerializeField] private FluidContainer _fluidContainer;

    private Transform pouringObjectTransform;

    private GameObject currentPourEffect;
    void Start()
    {
        pouringObjectTransform = transform;
    }
    
    void Update()
    {
        if (_fluidContainer.CheckWaterLevel(pourPoint.position))
        {
            PourOut();
            return;
        }
        
        StopPouring();
    }

    private void PourOut()
    {
        _fluidContainer.Decrease(0.001f);
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
