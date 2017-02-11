using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittererScript : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private float spawnTime;

    public float moveSpeed = 4;
    public float junkSpawn = 3;
    public float junkVariance = .5f;
    public GameObject[] junk;

	public void  Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        junkSpawn += Random.Range(-junkVariance, junkVariance);
        spawnTime = 0f;
	}
	
	public void Update ()
    {
        rb2d.velocity = new Vector2(moveSpeed, 0f);
        spawnTime += Time.deltaTime;
        if(spawnTime > junkSpawn)
        {
            DropJunk();
            spawnTime = 0f;
        }
	}

    public void SwapDirection()
    {
        moveSpeed *= -1;
    }

    private void DropJunk()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0f);
        Instantiate(junk[Random.Range(0, junk.Length)], pos, Quaternion.identity);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bound"))
        {
            GameObject.Destroy(gameObject);
        }
    }

}
