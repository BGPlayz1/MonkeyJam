using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void OnDeathDelegate(GameObject g, float t);
public class EnemyRespawnable : MonoBehaviour
{
    [HideInInspector]
    public float RespawnTime;
    public event OnDeathDelegate OnDeathMessage;

    private void Awake()
    {
        Stats stats = GetComponent<Stats>();
        stats.OnDeath += SendOnDeathData;
    }

    public void SendOnDeathData()
    {
        OnDeathMessage?.Invoke(gameObject, RespawnTime);
    }
}
