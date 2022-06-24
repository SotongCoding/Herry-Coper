using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UI_UIControl : MonoBehaviour
{
    #region Plane Selector
    [SerializeField] Image[] registeredPlane;
    public void SetImage(int index, Sprite sprite)
    {
        registeredPlane[index].sprite = sprite;
    }
    #endregion
}
