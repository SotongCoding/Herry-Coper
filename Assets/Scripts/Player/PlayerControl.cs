using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] PlaneControl[] registeredPlane = new PlaneControl[6];
    [SerializeField] Joystick joystick;
    [SerializeField] PlaneControl currentControlledPlane = null;

    public void SetUnitControl(int index)
    {
        currentControlledPlane?.visualHandle.setActive(false);
        if (registeredPlane[index] != null)
        {
            var plane = registeredPlane[index];
            currentControlledPlane = plane;

            joystick.onDragEvent = plane.RotateUnit;
            plane.visualHandle.setActive(true);
        }
    }

    public void RegisterPlane(PlaneControl plane)
    {
        for (int i = 0; i < registeredPlane.Length; i++)
        {
            if (registeredPlane[i] == null)
            {
                registeredPlane[i] = plane;
                if (currentControlledPlane == null)
                {
                    plane.visualHandle.setActive(true);
                    currentControlledPlane = plane;
                    joystick.onDragEvent = plane.RotateUnit;
                    plane.properties.onDeath += () => { DeRegisterPlane(plane); };
                }
                FindObjectOfType<UI_UIControl>().SetImage(i, plane.visualHandle.unitSprite);
                break;
            }
        }
    }
    public void DeRegisterPlane(PlaneControl plane)
    {
        for (int i = 0; i < registeredPlane.Length; i++)
        {
            if (registeredPlane[i] == plane)
            {
                registeredPlane[i] = null;
                FindObjectOfType<UI_UIControl>().SetImage(i, null);
                break;
            }
        }
        if (currentControlledPlane == plane)
        {
            currentControlledPlane = null;
            joystick.onDragEvent = null;
        }
    }

}