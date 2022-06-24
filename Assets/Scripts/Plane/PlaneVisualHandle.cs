using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneVisualHandle : MonoBehaviour
{

    [SerializeField] SpriteRenderer body;
    public Sprite unitSprite => body.sprite;

    #region UI
    [SerializeField] UnityEngine.UI.Image timerFill;
    #endregion
    public void Initial(DataModelSO_PalneData data)
    {
        body.sprite = data.unitSprite;
    }

    internal void setActive(bool v)
    {
        body.material.SetFloat("turn_on", v ? 1 : 0);
    }
    public void UpdateTime(float remainingTime){
        timerFill.fillAmount = remainingTime;
    }
}
