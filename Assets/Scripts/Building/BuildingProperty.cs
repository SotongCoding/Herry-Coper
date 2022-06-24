using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProperty
{
    #region  Unit Combat Data
    float health;
    float currentHealth { set; get; }
    public float remainingHealth => currentHealth / health;
    public bool IsDestroyed => currentHealth <= 0;

    // Event
    public System.Action onDestroyed;
    #endregion

    internal void ReduceHealth(int value)
    {
        currentHealth -= Mathf.Abs(value);
        if (currentHealth <= 0)
        {
            onDestroyed.Invoke();
        }
    }
    public void Initial()
    {
        this.health = 20;
        currentHealth = health;
    }
}
