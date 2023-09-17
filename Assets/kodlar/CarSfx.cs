using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSfx : MonoBehaviour
{
    [Header("audio sources")]
    public AudioSource tiresScreeachingAudioSource;
    public AudioSource engineAudioSource;
    public AudioSource carHitAudioSource;

    float desiredEnginePitch = 0.5f;
    float tireScreechPitch = 0.5f;


    araba araba;

    private void Awake() {

        araba = GetComponentInParent<araba>();
    } 

    private void Update() {
        UpdateEngineSFX();
        UpdateTiresScreechingSFX();
    }

    void UpdateEngineSFX(){
        float velocityMagnitude = araba.GetLateralVelocity();

        float desiredEngineVolume = (velocityMagnitude * 0.5f);

        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume,0.2f,1.0f);

        engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume,desiredEngineVolume,Time.deltaTime * 10);

        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch,0.5f,2f);
        engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch,desiredEnginePitch,Time.deltaTime * 1.5f);
    }

    void UpdateTiresScreechingSFX()
    {
        if(araba.IsTireScreeching(out float lateralVelocity,out bool isBraking)){
            if(isBraking){
                tiresScreeachingAudioSource.volume = Mathf.Lerp(tiresScreeachingAudioSource.volume,1.0f,Time.deltaTime * 10);
                tireScreechPitch = Mathf.Lerp(tireScreechPitch,0.5f,Time.deltaTime * 10);
            }
            else{
                tiresScreeachingAudioSource.volume = Mathf.Abs(lateralVelocity) * 0.05f;
                tireScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }
        else{
            tiresScreeachingAudioSource.volume = Mathf.Lerp(tiresScreeachingAudioSource.volume,0,Time.deltaTime * 10);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        float relativeVelocity = other.relativeVelocity.magnitude;
        
        float volume = relativeVelocity * 0.1f;

        carHitAudioSource.pitch = Random.Range(0.95f,1.05f);
        carHitAudioSource.volume = volume;

        if(!carHitAudioSource.isPlaying){carHitAudioSource.Play();}


    }

}
