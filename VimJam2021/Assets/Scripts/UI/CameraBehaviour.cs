using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour: MonoBehaviour
{
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.05f;
    private float dampingSpeed = 1f;
    
    Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeDuration > 0){
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
   
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else{
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake() {
        shakeDuration = 0.3f;
    }
}
