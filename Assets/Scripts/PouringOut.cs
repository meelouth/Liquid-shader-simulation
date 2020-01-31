﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringOut : MonoBehaviour
{
    private enum PouringType
    {
        Bottleneck,
        Hole
    }
    
    [SerializeField] private Transform pourPoint;

    [SerializeField] private GameObject pourEffect;


    private Transform pouringObjectTransform;

    private GameObject currentPourEffect;

    private IPouring pouring;

    [SerializeField] private PouringType type;
    void Start()
    {
        pouringObjectTransform = transform;
        
        Initialize();
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

    private void Initialize()
    {
        switch (type)
        {
            case PouringType.Bottleneck:
                pouring = gameObject.AddComponent<Bottleneck>();
                break;
            case PouringType.Hole:
                pouring = gameObject.AddComponent<Hole>();
                break;
        }
    }
    
    
}
