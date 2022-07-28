using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaneControl : SotongUtils.PoolSpawnedObject
{
    public PlaneProperties properties { private set; get; } = new PlaneProperties();

    protected override void StoreToPool()
    {
        gameObject.SetActive(false);
        base.StoreToPool();
    }
    public PlaneVisualHandle visualHandle { private set; get; }
    float currentTimer;

    #region  Moving Control
    [SerializeField] Rigidbody2D rigid;
    Vector2 direction;
    internal void RotateUnit(Vector2 inputDir)
    {
        direction = inputDir;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        rigid.rotation = angle;
    }
    #endregion

    #region  Shoot Properties
    ShootArea shootArea;
    public float shootInteval => properties.shootDelay;
    void GiveDamage(TankControl target)
    {
        if (target.properties.type == properties.type)
        {
            target.TakeDamage(properties.power);
        }
    }
    #endregion

    private void Awake()
    {
        visualHandle = GetComponent<PlaneVisualHandle>();
        shootArea = GetComponentInChildren<ShootArea>();
    }

    internal void Intial(DataModelSO_PalneData data)
    {
        properties.onDeath += StoreToPool;
        properties.onChangeHealth += visualHandle.UpdateTime;

        properties.Initial(data);
        visualHandle.Initial(data);

        gameObject.SetActive(true);

        //firstTime Rotate
        RotateUnit(Vector2.up);

    }

    private void FixedUpdate()
    {
        //Moving
        rigid.velocity = direction * properties.speed;

        //Attack
        if (currentTimer < shootInteval) currentTimer += Time.fixedDeltaTime;

        if (shootArea.currenttarget == null) return;
        if (currentTimer >= shootInteval)
        {
            GiveDamage(shootArea.currenttarget);
            currentTimer = 0;
        }

    }


}
