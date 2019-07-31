using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeDuration = 0f;

    private float shakeMagnitude = 0.20f;

    private float dampingSpeed = 1.0f;

    Vector3 initialPosition;
    private bool _shakeDone;

    private void Start(){
        _shakeDone = true;
    }

    void Update(){
        if (shakeDuration > 0){
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else{
            if(!_shakeDone)
            transform.localPosition = initialPosition;
            _shakeDone = true;
            shakeDuration = 0f;
        }
    }

    public void TriggerShake(float DurationShake){
        initialPosition = transform.localPosition;
        shakeDuration = DurationShake;
        _shakeDone = false;
    }

}
