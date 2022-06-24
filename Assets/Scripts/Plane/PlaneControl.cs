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

    public bool canAttack { private set; get; }
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

    private void Awake()
    {
        visualHandle = GetComponent<PlaneVisualHandle>();
        // Event when Die
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
        rigid.velocity = direction * properties.speed;
        if (!canAttack)
        {
            currentTimer += Time.fixedDeltaTime;
            if (currentTimer >= 1)
            {
                canAttack = true;
                currentTimer = 0;
            }
        }
    }
}
