using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManagerScript : MonoBehaviour
{
    private float littererEllapsedTime;

    public GameObject litterer;
    public Transform littererLeftSpawn;
    public Transform littererRightSpawn;
    public float littererSpawnInterval = 20;
    

	void Start ()
    {
        littererEllapsedTime = 0;
	}
	
	void Update ()
    {
        littererEllapsedTime += Time.deltaTime;
        if(littererEllapsedTime > littererSpawnInterval)
        {
            SpawnLitterer();
            littererEllapsedTime = 0;
        }
	}

    private void SpawnLitterer()
    {
        Vector3 spawnPoint;
        GameObject newLitterer;
        bool movingRight;
        if (Random.Range(0f, 1f) > .5f)
        {
            spawnPoint = littererLeftSpawn.position;
            movingRight = true;
        }
        else
        {
            spawnPoint = littererRightSpawn.position;
            movingRight = false;
        }

        
        newLitterer = Instantiate(litterer, new Vector3(spawnPoint.x, spawnPoint.y, 0f), Quaternion.identity) as GameObject;
        if(!movingRight)
            newLitterer.GetComponent<LittererScript>().SwapDirection();

    }

    public void KillAI(GameObject toKill)
    {
        
    }
}
