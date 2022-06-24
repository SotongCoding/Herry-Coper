using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankVisualHandle : MonoBehaviour
{
   [SerializeField] SpriteRenderer body;
   [SerializeField] SpriteRenderer barrel;
    public void Initial(DataModelSO_TankData data)
    {
        body.sprite = data.bodySprite;
        barrel.sprite = data.barrelSprite;
    }
}
