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
    public float junkPositionVariance = 8;
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
        Quaternion newRotation = gameObject.transform.rotation;
        newRotation.y += 180;
        gameObject.transform.rotation = newRotation;
    }

    private void DropJunk()
    {
        GameObject junkHolder = GameObject.Find("JunkHolder");
        GameObject newJunk;

        float offset = Random.Range(-junkPositionVariance, junkPositionVariance);
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + offset, 0f);
        newJunk = Instantiate(junk[Random.Range(0, junk.Length)], pos, Quaternion.identity) as GameObject;
        if (junkHolder != null) newJunk.transform.SetParent(junkHolder.transform);
        GameManager.instance.AddTrash();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bound"))
        {
            GameObject.Destroy(gameObject);
        }
    }

}
