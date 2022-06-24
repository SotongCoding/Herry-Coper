using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/DatabaseSO_Tank", fileName = "DatabaseSO_Tank")]
public class DatabaseSO_TankData : SotongUtils.ScriptableObj_DataBase<DataModelSO_TankData>
{
   
}
[System.Serializable]
public struct DataModelSO_TankData : SotongUtils.DataBaseSO_DataModel
{
    public string key => unitId;
    [SerializeField] string unitId;
    public Sprite bodySprite;
    public Sprite barrelSprite;
    
    [Header("Combat Properties")]
    public int health;
    public float speed;
    public int power;
    public CommonVariable.UnityType type;
}
