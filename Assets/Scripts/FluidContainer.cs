using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidContainer : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private MeshRenderer _mesh;
    [Range(0.0f,1.0f)]
    [SerializeField] private float _percentFluid;

    public void Start()
    {
        Filling();
    }
    public void Update()
    {
        Filling();
    }

    private void Filling()
    {
        float height = CalculateHeight();
        float level = height * _percentFluid;
        float middle = height / 2;
        _renderer.material.SetFloat("_FillAmount", middle - level + 0.5f);
    }

    private float CalculateHeight()
    {
        var bounds = _mesh.bounds;
        return bounds.max.y - bounds.min.y;
    }

    public bool CheckWaterLevel(Vector3 positionBottleneck)
    {
        float height = CalculateHeight();
        float level = _mesh.bounds.min.y + height * _percentFluid;
        return positionBottleneck.y <= level && _gameObject.activeSelf;
    }

    public float GetPercentFluid()
    {
        return _percentFluid;
    }

    public void Decrease(float percent)
    {
        _percentFluid -= percent;
        if (_percentFluid < 0.001f)
        {
            _gameObject.SetActive(false);
            _percentFluid = 0;
        }
    }

    public void Increase(float percent)
    {
        if (_percentFluid <= 0.001f)
        {
            _gameObject.SetActive(true);
        }
        
        _percentFluid += percent;
        if (_percentFluid > 1)
            _percentFluid = 1;
    }
}
