using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    public Vector3 movementVector;
    float movementFactor;
    public float period = 2f;
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period == 0) { return; }//period <= Mathf.Epsilon if'in içine bunu da yazabiliriz
        float cycles = Time.time / period; // continually growing over time 

        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau);//going from -1 to 1 

        movementFactor = (rawSinWave + 1f) / 2;//recalculated to go from 0 to 1 so its cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
