using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;

[System.Serializable]
public class PlaneProperties
{
    #region  Unit Combat Data
    public float health { private set; get; }
    public float speed { private set; get; }
    public int power { private set; get; }
    public CommonVariable.UnityType type { private set; get; }

    // Event
    public System.Action onDeath;
    public System.Action<float> onChangeHealth;
    async void StartTimer()
    {
        var end = Time.time + health;
        while (Time.time < end)
        {
            onChangeHealth?.Invoke((end - Time.time) / health);
            //Reducing Health
            await Task.Yield();
        }
        onDeath?.Invoke();
        onDeath = null;
        onChangeHealth = null;
    }
    #endregion

    public void Initial(DataModelSO_PalneData data)
    {
        this.health = data.health;
        this.speed = data.speed;
        this.power = data.power;
        this.type = data.type;

        StartTimer();
    }
}
