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

    private Transform _pouringObjectTransform;

    private PouringEffect _currentPourEffect;

    private IPouring _pouring;

    void Start()
    {
        _pouringObjectTransform = transform;

        _pouring = GetComponent<IPouring>();
        
    }
    
    void Update()
    {
        if (_fluidContainer.CheckWaterLevel(pourPoint.position))
        {
            float count = speed;
            if (speed > _fluidContainer.GetLitersFluid())
            {
                count = _fluidContainer.GetLitersFluid();
            }
            _fluidContainer.Decrease(count);
            TransferFluid(PourOut(), count);
            return;
        }

        StopPouring();
    }

    private RaycastHit PourOut()
    {
        if (!_currentPourEffect)
            _currentPourEffect = Instantiate(pourEffect, pourPoint).GetComponent<PouringEffect>();
        
        RaycastHit hit;
        if (Physics.Raycast(pourPoint.position, Vector3.down, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(pourPoint.position, Vector3.down * hit.distance, Color.yellow);
            _currentPourEffect.SetColor(_fluidContainer.GetColor());
            _currentPourEffect.SetLineRenderPosition(hit.point);
        }

        return hit;
    }

    private void TransferFluid(RaycastHit hit, float count)
    {
        FluidContainer container = hit.transform.GetComponent<FluidContainer>();
        if (container != null && container != _fluidContainer)
        {
            container.Increase(count, _fluidContainer.GetColor());
        }
    }

    private void StopPouring()
    {
        if (_currentPourEffect != null)
        {
            Destroy(_currentPourEffect.gameObject);
        }
    }
}
