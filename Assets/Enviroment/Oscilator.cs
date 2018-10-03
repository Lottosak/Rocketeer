using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(2f,2f,2f);
    [SerializeField] float delay = 0f;
    private Vector3 position;
    [SerializeField] float period = 2f;


	// Use this for initialization
	void Start ()
	{
	    position = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (period <= Mathf.Epsilon) return;
	    float cycles = Time.time / period;

	    const float tau = Mathf.PI * 2;
	    float sinFactor = Mathf.Sin(cycles * tau) / 2f + 0.5f;

	    Vector3 offset = sinFactor * movementVector;
	    transform.position = position + offset;
	}
}
