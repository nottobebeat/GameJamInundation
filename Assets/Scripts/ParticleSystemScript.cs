using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemScript : MonoBehaviour
{
    ParticleSystem particleSystem;
    Renderer renderer;
	void Start ()
    {
        particleSystem = GetComponent<ParticleSystem>();
        renderer = particleSystem.GetComponent<Renderer>();
        renderer.sortingLayerName = "Player";
        particleSystem.Pause();
	}
	
	void Update ()
    {
		
	}
}
