using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Renderer renderer;
    private bool dumpedWaste;
    private bool dumpingWaste;
    private Collider2D ignoreCollider;

    public GameObject waste;
    public float moveSpeed = 2;

    // Use this for initialization
    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        ignoreCollider = GameObject.Find("Map").GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), ignoreCollider);
        dumpedWaste = false;
        dumpingWaste = false;
	}

    void Update()
    {
        if (dumpedWaste)
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
        }
        else if (dumpingWaste)
        {
            rb2d.velocity = new Vector2(0, 0);
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Hit player!");
                    rb2d.velocity = new Vector2(0, 0);
                }
                else rb2d.velocity = new Vector2(moveSpeed, 0);
                if (hit.collider.gameObject.CompareTag("Lake"))
                {
                    Debug.Log("Dumping waste!");
                    dumpingWaste = true;
                    StartCoroutine("DumpWaste");
                }
            }
            else
            {
                rb2d.velocity = new Vector2(moveSpeed, 0);
            }
        }
    }

    IEnumerator DumpWaste()
    {
        float ellapsedTime = 0;
        while (ellapsedTime < 2)
        {
            ellapsedTime += Time.deltaTime;
            yield return null;
        }
        waste.SetActive(true);
        dumpingWaste = false;
        dumpedWaste = true;
        yield return null;
    }
}
