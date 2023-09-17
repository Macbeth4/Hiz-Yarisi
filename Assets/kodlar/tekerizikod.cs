using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tekerizikod : MonoBehaviour
{
    araba araba;
    TrailRenderer trailRenderer;

    private void Awake() {
        araba = GetComponentInParent<araba>();
        trailRenderer = GetComponent<TrailRenderer>();

        trailRenderer.emitting = false;
    }


    private void Update() {
        if(araba.IsTireScreeching(out float lateralVelocity,out bool isBraking))
        {
            trailRenderer.emitting = true;
        }
        else
        {
            trailRenderer.emitting = false;
        }

    }
}
