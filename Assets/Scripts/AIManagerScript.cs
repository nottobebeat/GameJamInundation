using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManagerScript : MonoBehaviour
{
    private float littererEllapsedTime;
    private float truckEllapsedTime;
    private float arsonistEllapsedTime;

    public GameObject litterer;
    public Transform littererLeftSpawn;
    public Transform littererRightSpawn;
    public float littererSpawnInterval = 20;

    public GameObject truck;
    public GameObject waste;
    public Transform TruckSpawn;
    public float truckSpawnInterval = 8;

    public GameObject arsonist;
    public Transform arsonistSpawn;
    public ParticleSystem particleSystem;
    public float arsonistSpawnInterval = 5;

	void Start ()
    {
        littererEllapsedTime = 0;
        truckEllapsedTime = 0;
        arsonistEllapsedTime = 0;
	}
	
	void Update ()
    {
        UpdateLitterer();
        UpdateTruck();
        UpdateArsonist();
	}

    private void UpdateLitterer()
    {
        littererEllapsedTime += Time.deltaTime;

        if (littererEllapsedTime > littererSpawnInterval)
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

        
        newLitterer = Instantiate(litterer, spawnPoint, Quaternion.identity) as GameObject;
        if(!movingRight)
            newLitterer.GetComponent<LittererScript>().SwapDirection();

    }

    private void UpdateTruck()
    {
        if(!waste.activeInHierarchy)
        {
            truckEllapsedTime += Time.deltaTime;

            if(truckEllapsedTime > truckSpawnInterval)
            {
                SpawnTruck();
                truckEllapsedTime = 0;
            }
        }
    }

    private void SpawnTruck()
    {
        GameObject newTruck;
        Vector3 spawnPoint = TruckSpawn.position;
        newTruck = Instantiate(truck, spawnPoint, Quaternion.identity) as GameObject;
        newTruck.GetComponent<TruckScript>().waste = waste;
    }

    private void UpdateArsonist()
    {
        if (particleSystem.isPaused)
        {
            arsonistEllapsedTime += Time.deltaTime;
            if (arsonistEllapsedTime > arsonistSpawnInterval)
            {
                SpawnArsonist();
                arsonistEllapsedTime = 0;
            }
        }
    }

    private void SpawnArsonist()
    {
        GameObject newArsonist;
        newArsonist = Instantiate(arsonist, arsonistSpawn.transform.position, Quaternion.identity) as GameObject;
        newArsonist.GetComponent<ArsonistScript>().particleSystem = particleSystem;
    }
}
