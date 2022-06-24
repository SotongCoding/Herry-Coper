using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    SotongUtils.PoolingSystem poolingSystem = new SotongUtils.PoolingSystem(6);
    [SerializeField] DatabaseSO_PlaneData dataPlane;

    [SerializeField] PlayerControl playerControl;
    [SerializeField] PlaneControl planePrefab;
    [SerializeField] Transform spawnPlace;
    [SerializeField] Transform[] spawnPoints;

    private void Awake()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }

    public void SpawnPlane(string planeId)
    {
        var IsSpawn = poolingSystem.SpawnObject(
       planePrefab, //Prefab
       spawnPoints[Random.Range(0, spawnPoints.Length)].position, //Loation to spawn
       out SotongUtils.PoolSpawnedObject spawnedPlane, // object that spawned
       spawnPlace); // Parent

        if (IsSpawn)
        {
            var plane = ((PlaneControl)spawnedPlane);
            plane.Intial(dataPlane.GetData(planeId));
            playerControl.RegisterPlane(plane);
        }
    }
}
