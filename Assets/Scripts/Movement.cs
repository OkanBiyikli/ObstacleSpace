using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float mainThrust = 100f;
    public float rotationThrust = 100f;
    public AudioClip engineSFX;
    public ParticleSystem engineParticle;
    public ParticleSystem rightParticle;
    public ParticleSystem leftParticle;
    
    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessRotation();
        ProcessThrust();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft(); 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
    
    void StartThrusting()
    {
        rb.AddRelativeForce(0, 1, 0 * mainThrust * Time.deltaTime);//up
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(engineSFX);
            }
            if(!engineParticle.isPlaying)
            {
                engineParticle.Play();
            }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        engineParticle.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if(!rightParticle.isPlaying)
        {
            rightParticle.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if(!leftParticle.isPlaying)
        {
            leftParticle.Play();
        }
    }

    void StopRotating()
    {
        rightParticle.Stop();
        leftParticle.Stop();
    }

}
