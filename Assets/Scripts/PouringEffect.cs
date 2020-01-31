using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringEffect : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private Transform effectTransform;
    void Start()
    {
        effectTransform = transform;
        lineRenderer = GetComponent<LineRenderer>();
        
     //   SetLineRenderPosition(Vector3.zero);
    }
    

    private void SetLineRenderPosition(Vector3 position)
    {
        lineRenderer.SetPosition(0, effectTransform.position);
        lineRenderer.SetPosition(1, position);
    }
    
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(effectTransform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(effectTransform.position, Vector3.down * hit.distance, Color.yellow);
            SetLineRenderPosition(hit.point);
        }
    }
}
