using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemScript : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private Renderer renderer;
    private bool interactable;
	void Start ()
    {
        particleSystem = GetComponent<ParticleSystem>();
        renderer = particleSystem.GetComponent<Renderer>();
        renderer.sortingLayerName = "Player";
        particleSystem.Pause();
        interactable = false;
	}
	
	void Update ()
    {
		if(interactable)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                particleSystem.Pause();
                particleSystem.Clear();
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        interactable = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        interactable = false;
    }
}
