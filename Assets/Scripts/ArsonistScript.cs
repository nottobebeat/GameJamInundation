using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArsonistScript : MonoBehaviour
{
    public float moveSpeed;
    public float detectedSpeed;
    public ParticleSystem particleSystem;

    private bool detected;
    private bool startingFire;
    private Rigidbody2D rb2d;

	void Start ()
    {
        detected = false;
        startingFire = false;
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	void Update ()
    {
        if(detected)
        {
            rb2d.velocity = new Vector2(detectedSpeed, 0);
            Quaternion rotation = gameObject.transform.rotation;
            rotation.y = 180;
            gameObject.transform.rotation = rotation;
        }
        else if(startingFire)
        {
            rb2d.velocity = new Vector2(0, 0);
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.left);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Bush2"))
                {
                    StartCoroutine(SetFire(hit.collider.gameObject));
                }
            }
            rb2d.velocity = new Vector2(-moveSpeed, 0);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            detected = true;
        }
    }

    IEnumerator SetFire(GameObject toDestroy)
    {
        float ellapsedTime = 0;
        startingFire = true;
        while (ellapsedTime < 2)
        {
            ellapsedTime += Time.deltaTime;
            yield return null;
        }
        detected = true;
        startingFire = false;
        particleSystem.GetComponent<ParticleSystem>().Play();
        GameObject.Destroy(toDestroy);
        yield return null;
    }
}
