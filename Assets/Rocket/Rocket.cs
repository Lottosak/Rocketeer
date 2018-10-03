using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip thrustAudio;
    [SerializeField] ParticleSystem thrustParticles;

    Rigidbody rigidbody;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	    audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (CollisionDetector.state != CollisionDetector.State.Alive) return;
	    Thrust();
	    Rotate();
	}

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(thrustAudio);
            }
            thrustParticles.Play();
        }
        else
        {
            audioSource.Stop();
            thrustParticles.Stop();
        }
    }

    private void Rotate()
    {
        rigidbody.freezeRotation = true;

        float rotationFrameSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationFrameSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationFrameSpeed);
        }

        rigidbody.freezeRotation = false;
    }
}
