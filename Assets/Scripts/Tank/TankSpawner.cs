using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    public SotongUtils.PoolingSystem poolingSystem { private set; get; } = new SotongUtils.PoolingSystem(10);

    [SerializeField] TankControl tankUnitPrefab;
    [SerializeField] Transform spawnPlace;
    [SerializeField] string[] variationTanksId;
    [SerializeField] Transform[] spawnPoints;

    private void Awake()
    {
        InvokeRepeating("SpawnTank", 3, 3);
    }

    void SpawnTank()
    {
        var spawn = poolingSystem.SpawnObject(
        tankUnitPrefab, //Prefab
        spawnPoints[Random.Range(0, spawnPoints.Length)].position, //Loation to spawn
        out SotongUtils.PoolSpawnedObject spawnedTank, // object that spawned
        spawnPlace); // Parent

        if (spawn && GameManger.Instance.GetBuildingData(out BuildingControl building))
        {
            ((TankControl)spawnedTank).Initial(
            variationTanksId[Random.Range(0, variationTanksId.Length)],
            building);
        }
    }


}
