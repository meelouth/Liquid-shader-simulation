using System;
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

    [SerializeField] private float angleToPour = 30;

    [SerializeField] private float speed = 1f;

    [SerializeField] private FluidContainer _fluidContainer;

    private Transform pouringObjectTransform;

    private GameObject currentPourEffect;

    private IPouring pouring;

    [SerializeField] private PouringType type;
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

    private void Initialize()
    {
        switch (type)
        {
            case PouringType.Bottleneck:
                gameObject.AddComponent<Bottleneck>();
                break;
            case PouringType.Hole:
                gameObject.AddComponent<Hole>();
                break;
        }
    }
    
}
