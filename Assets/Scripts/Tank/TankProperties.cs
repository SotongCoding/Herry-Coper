using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TankProperties
{
    #region  Unit Combat Data
    public float health { private set; get; }
    public float speed { private set; get; }
    public int power { private set; get; }
    public bool IsDestroyed { private set; get; }
    public CommonVariable.UnityType type { private set; get; }

    // Event
    public System.Action onDeath;

    internal void ReduceHealth(int value)
    {
        health -= Mathf.Abs(value);
        if (health <= 0)
        {
            onDeath.Invoke();
        }
    }
    #endregion

    public void Initial(DataModelSO_TankData data)
    {

        this.health = data.health;
        this.speed = data.speed;
        this.power = data.power;
        this.type = data.type;
    }

}
