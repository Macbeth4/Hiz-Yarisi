using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke : MonoBehaviour
{
    float particleEmissionRate = 0;
    araba araba;

    ParticleSystem particleSystemSmoke;
    ParticleSystem.EmissionModule particleSystemEmissionModule;

    private void Awake() {
        araba = GetComponentInParent<araba>();
        particleSystemSmoke = GetComponent<ParticleSystem>();
        particleSystemEmissionModule = particleSystemSmoke.emission;
        particleSystemEmissionModule.rateOverTime = 0;
    }

    private void Update() {
        particleEmissionRate = Mathf.Lerp(particleEmissionRate,0, Time.deltaTime * 5);
        particleSystemEmissionModule.rateOverTime = particleEmissionRate;

        if(araba.IsTireScreeching(out float lateralVelocity,out bool isBraking))
        {
            if(isBraking){particleEmissionRate = 30;}
            else{particleEmissionRate = Mathf.Abs(lateralVelocity) * 2;}


        }
    }
}
