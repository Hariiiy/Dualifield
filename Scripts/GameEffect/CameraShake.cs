using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform ShakeTransform;
    //stop the camera when shaking active
    public bool IsShaking;

    //camera shake trigger
    public Weapon weapon;
    private bool weaponTrigger;


    //values of shaking
    [SerializeField] private float shakeAmount;
    [SerializeField] private float decreaseValue;
    [SerializeField] private float shakeDuration;

    Vector3 originalPos;

    private void Start()
    {
        shakeAmount = 0.2f;
        decreaseValue = 1.0f;
        shakeDuration = 0.05f;


            

    }

    void OnEnable()
    {
        originalPos = ShakeTransform.localPosition;
    }

    private void Update()
    {


    }



}
