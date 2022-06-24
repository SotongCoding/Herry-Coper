using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SotongUtils
{
    public class PoolingSystem
    {
        Queue<PoolSpawnedObject> spawnedObject = new Queue<PoolSpawnedObject>();
        Queue<PoolSpawnedObject> storedObject = new Queue<PoolSpawnedObject>();
        int currentSpawnnedAmount;
        int poolLimit;

        public PoolingSystem(int poolLimit = 100)
        {
            this.poolLimit = poolLimit;
            currentSpawnnedAmount = 0;
        }

        public PoolSpawnedObject PickPool()
        {
            if (storedObject.Count <= 0) return null;
            var callObject = storedObject.Dequeue();
            spawnedObject.Enqueue(callObject);

            return callObject;
        }

        public void StoreData(PoolSpawnedObject storeData)
        {
            storedObject.Enqueue(storeData);
        }

        public virtual bool SpawnObject
        (PoolSpawnedObject basePrefab, Vector3 position, out PoolSpawnedObject createdObject,
        Transform parent = null,
        bool instantiateInWolrdSpace = true)
        {
            if (storedObject.Count <= 0)
            {
                if (currentSpawnnedAmount > poolLimit)
                {
                    createdObject = null;
                    return false;
                }
                createdObject = MonoBehaviour.Instantiate(basePrefab);
                createdObject.SetCurrentPool(this);
                currentSpawnnedAmount++;
                spawnedObject.Enqueue(createdObject);
            }
            else
            {
                createdObject = PickPool();
            }

            createdObject.transform.parent = parent;
            if (instantiateInWolrdSpace) createdObject.transform.position = position;
            else createdObject.transform.localPosition = position;

            return true;

        }

    }
    public class PoolSpawnedObject : MonoBehaviour
    {
        private PoolingSystem currentPool;

        protected virtual void StoreToPool()
        {
            currentPool.StoreData(this);
        }

        internal void SetCurrentPool(PoolingSystem poolingSystem)
        {
            currentPool = poolingSystem;
        }
    }
}