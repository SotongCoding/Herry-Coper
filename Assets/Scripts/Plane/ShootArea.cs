using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArea : MonoBehaviour
{
    PlaneControl unit;
    public TankControl currenttarget { private set; get; }
    private void Awake()
    {
        unit = GetComponentInParent<PlaneControl>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("tank"))
        {
            var tank = other.GetComponent<TankControl>();
            currenttarget = tank;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("tank"))
        {
            var tank = other.GetComponent<TankControl>();
            if (currenttarget == tank)
            {
                currenttarget = null;
            }
        }
    }
}
