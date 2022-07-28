using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/DatabaseSO_Plane", fileName = "DatabaseSO_Plane")]
public class DatabaseSO_PlaneData : SotongUtils.ScriptableObj_DataBase<DataModelSO_PalneData>
{

}
[System.Serializable]
public struct DataModelSO_PalneData : SotongUtils.DataBaseSO_DataModel
{
    public string key => unitId;
    [SerializeField] string unitId;
    public Sprite unitSprite;
    
    [Header("Combat Properties")]
    public float health;
    public float speed;
    public int power;
    public float shootDelay;
    public CommonVariable.UnityType type;
}
