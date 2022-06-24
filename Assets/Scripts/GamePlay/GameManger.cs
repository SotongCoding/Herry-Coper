using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static GameManger Instance;

    private void Awake()
    {
        Instance = this;
        avaiableBuilding = new List<BuildingControl>(buildings);
    }

    [SerializeField] List<BuildingControl> buildings;
    List<BuildingControl> avaiableBuilding;

    public bool GetBuildingData(out BuildingControl buildingData)
    {
        buildingData = null;
        if (avaiableBuilding.Count <= 0) return false;
        do { buildingData = avaiableBuilding[Random.Range(0, avaiableBuilding.Count)]; }
        while (buildingData.IsDestroyed);
        return true;
    }
    public void RemoveBuilding(BuildingControl building)
    {
        avaiableBuilding.Remove(building);
    }
}