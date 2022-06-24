using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class TankControl : SotongUtils.PoolSpawnedObject
{
    [SerializeField] DatabaseSO_TankData baseDataRef;
    public TankProperties properties { private set; get; } = new TankProperties();
    TankVisualHandle visualHandle;

    #region Ai Logic
    Seeker seeker;
    BuildingControl currentTarget;
    void SetDestination(Vector3 destination, System.Action onReachPos = null)
    {
        seeker.StartPath(transform.position, destination, (path) => { OnPathComlpete(path, onReachPos); });

        void OnPathComlpete(Path path, System.Action onReachDestiantion)
        {
            if (path == null) return;
            StartCoroutine(MovingOnPath());

            IEnumerator MovingOnPath()
            {
                foreach (var curDestination in path.vectorPath)
                {
                    while (Vector2.Distance(transform.position, curDestination) > 0.1)
                    {
                        RotateUnit(transform.position - curDestination);
                        transform.position = Vector2.MoveTowards(transform.position,
                        curDestination,
                        Time.deltaTime * properties.speed);
                        yield return null;
                    }
                }
                onReachDestiantion?.Invoke();
            }

            void RotateUnit(Vector2 faceDirection)
            {
                float angle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg - 90;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100 * Time.deltaTime);
                // transform.Rotate(Vector3.forward, angle);
            }
        }

    }
    #endregion


    protected override void StoreToPool()
    {
        base.StoreToPool();
        this.gameObject.SetActive(false);
    }
    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        visualHandle = GetComponent<TankVisualHandle>();
        properties.onDeath += StoreToPool;
    }

    public void Initial(string unitId, BuildingControl targetBuilding)
    {
        gameObject.SetActive(true);

        SetDestination(targetBuilding.stopPoint.position, Attack);
        currentTarget = targetBuilding;

        var data = baseDataRef.GetData(unitId);

        properties.Initial(data);
        visualHandle.Initial(data);
    }
    public void TakeDamage(int value)
    {
        properties.ReduceHealth(value);
    }


    void Attack()
    {
        StartCoroutine(AttackLogic());

        IEnumerator AttackLogic()
        {
            float currentTimer = 0;
            while (!currentTarget.IsDestroyed)
            {
                if (currentTimer >= 3)
                {
                    currentTarget.TakeDamage(properties.power);
                    currentTimer = 0;
                }
                currentTimer += Time.deltaTime;
                yield return null;
            }
            if (!GameManger.Instance.GetBuildingData(out BuildingControl building)) yield break;
            currentTarget = building;
            SetDestination(currentTarget.stopPoint.position, Attack);
        }

    }
}
