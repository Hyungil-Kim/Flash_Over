using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogCoverable : MonoBehaviour
{
    Renderer render;


    void Start()
    {
        render = GetComponent<Renderer>();
        FieldOfView.OnTargetsVisibilityChange += FieldOfViewOnTargetsVisibilityChange;
    }

    void OnDestroy()
    {
        FieldOfView.OnTargetsVisibilityChange -= FieldOfViewOnTargetsVisibilityChange;
    }

    void FieldOfViewOnTargetsVisibilityChange(List<Transform> newTargets)
    {
        render.enabled = newTargets.Contains(transform);
    }
}