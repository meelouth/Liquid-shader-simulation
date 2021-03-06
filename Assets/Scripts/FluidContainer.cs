﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidContainer : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private float _maxLiters;
    [SerializeField] private float _countLiters;
    [SerializeField] private float _percentFluid;
    [SerializeField] private Color _color;
    public void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        if(_countLiters == 0)
            _gameObject.SetActive(false);
        SetColor(_color);
        Filling();
    }
    public void Update()
    {
        Filling();
    }

    private void Filling()
    {
        CalculatePercent();
        float height = CalculateHeight();
        float level = height * _percentFluid;
        float middle = height / 2;
        _renderer.material.SetFloat("_FillAmount", middle - level + 0.5f);
    }

    private float CalculatePercent()
    { 
        _percentFluid = _countLiters / _maxLiters;
        return _percentFluid;
    }

    private float CalculateHeight()
    {
        var bounds = _mesh.bounds;
        return bounds.max.y - bounds.min.y;
    }

    public bool CheckWaterLevel(Vector3 positionBottleneck)
    {
        CalculatePercent();
        float height = CalculateHeight();
        float level = _mesh.bounds.min.y + height * _percentFluid;
        return positionBottleneck.y <= level && _percentFluid > 0;
    }

    public float GetLitersFluid()
    {
        return _countLiters;
    }

    public Color GetColor()
    {
        return _color;
    }

    private void SetColor(Color color)
    {
        _color = color;
        _renderer.material.SetColor("_Colour", _color);
    }

    public void Decrease(float count)
    {
        float diff = _countLiters - count;
        if (diff <= 0)
        {
            _countLiters = 0;
            _gameObject.SetActive(false);
        }
        else
        {
            _countLiters = diff;
        }
    }

    private Color CalculateColor(Color color, float count)
    {
        Color newColor = (color * count + _color * (_countLiters - count)) / _countLiters;
        return newColor;
    }

    public void Increase(float count, Color color)
    {
        float sum = _countLiters + count;
        if (_countLiters <= 0f)
        {
            SetColor(color);
            _gameObject.SetActive(true);
        }
        
        if (sum >= _maxLiters)
        {
            _countLiters = _maxLiters;
        }
        else
        {
            _countLiters = sum;
        }

        if (_countLiters - count > 0)
        {
            SetColor(CalculateColor(color, count));
        }
    }
}
