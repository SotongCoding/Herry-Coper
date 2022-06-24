using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingControl : MonoBehaviour
{
    public BuildingProperty property { private set; get; } = new BuildingProperty();
    public Transform stopPoint;

    #region UI
    [SerializeField] UnityEngine.UI.Image hpImage;
    #endregion

    private void Awake()
    {
        property.Initial();
        property.onDestroyed = () => { GameManger.Instance.RemoveBuilding(this); gameObject.SetActive(false); };
    }
    public bool IsDestroyed => property.IsDestroyed;
    public void TakeDamage(int comingValue)
    {
        property.ReduceHealth(comingValue);
        SotongUtils.UIHelper.ChangeFillAmount(hpImage, property.remainingHealth, 3f);
    }
}
