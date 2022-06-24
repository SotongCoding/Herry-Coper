using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SotongUtils
{
    [System.Serializable]
    public class ScriptableObj_DataBase<T> : ScriptableObject where T : DataBaseSO_DataModel
    {
        [SerializeField] List<T> data = new List<T>();
        Dictionary<string, T> allData = new Dictionary<string, T>();
        public int amountData => data.Count;

        public virtual void Intiial()
        {
            if (allData.Count <= 0)
            {
                foreach (var item in data)
                {
                    allData.Add(item.key, item);
                }
            }
        }

        public T GetData(string key)
        {
            Intiial();
            if (allData.ContainsKey(key))
                return allData[key];
            else return default;
        }
        public void GetEveryData(System.Action<T> onGetData)
        {
            for (int i = 0; i < data.Count; i++)
            {
                onGetData?.Invoke(data[i]);
            }
        }
        public T[] GetAllData()
        {
            return data.ToArray();
        }


    }
    public interface DataBaseSO_DataModel
    {
        string key { get; }
    }
}