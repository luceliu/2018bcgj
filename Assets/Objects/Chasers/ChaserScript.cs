using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Overworld;

public class ChaserScript : MonoBehaviour
{
    public float Velocity = 10.0f;
    public float BangFactor = 5.0f;
    public float BangError = 5.0f;

    private Transform PlayerTransform;

	// Use this for initialization
	void Start ()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
