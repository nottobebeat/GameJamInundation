using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteScript : MonoBehaviour
{
    private Renderer renderer;
    private bool interactable;

    public Material highlight;
    public Material defaultMaterial;

	void Start ()
    {
        gameObject.SetActive(false);
        renderer = GetComponent<Renderer>();
        interactable = false;
	}
	
	void Update ()
    {
		if(interactable)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                renderer.material = defaultMaterial;
                interactable = false;
                gameObject.SetActive(false);
                GameManager.instance.StopPondWaste();
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            renderer.material = highlight;
            interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            renderer.material = defaultMaterial;
            interactable = false;
        }
    }
}
