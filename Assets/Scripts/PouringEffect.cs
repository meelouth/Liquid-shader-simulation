using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringEffect : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private Transform effectTransform;

    public void SetColor(Color color)
    {
        lineRenderer.material.SetColor("_Color", color);
    }
    
    public void SetLineRenderPosition(Vector3 position)
    {
        lineRenderer.SetPosition(0, effectTransform.position);
        lineRenderer.SetPosition(1, position);
    }
}
