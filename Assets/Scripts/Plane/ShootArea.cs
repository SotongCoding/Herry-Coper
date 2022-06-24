using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArea : MonoBehaviour
{
    PlaneControl unit;
    private void Awake()
    {
        unit = GetComponentInParent<PlaneControl>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!unit.canAttack) return;
        
        if (other.CompareTag("tank"))
        {
            var tank = other.GetComponent<TankControl>();
            if (tank.properties.type == unit.properties.type)
            {
                tank.TakeDamage(unit.properties.power);
            }
        }
    }
}
