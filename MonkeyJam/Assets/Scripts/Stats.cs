using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stats : MonoBehaviour
{

    public event Action OnDeath;
    
    [SerializeField]
    private int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            if (currentHealth <= 0)
            {
                
                gameObject.SetActive(false);
                OnDeath?.Invoke();
            }
        }
    }

    [SerializeField]
    private int maxHealth;
    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value;
            currentHealth = value;
        }
    }

    [SerializeField]
    private float speed;
    public float Speed
    {
        get { return speed; }
        set
        {
            speed = value;
        }
    }

    [SerializeField]
    private int damage;
    public int Damage
    {
        get { return damage; }
        set
        {
            damage = value;
        }
    }

    [SerializeField]
    private int maxPoints;
    public int MaxPoints
    {
        get { return maxPoints; }
        set
        {
            maxPoints = value;
        }
    }

    [SerializeField]
    private int givenPoints;
    public int GivenPoints
    {
        get { return givenPoints; }
        set
        {
            givenPoints = value;
        }
    }
    [SerializeField]
    private int pointsMultiplier;
    public int PointsMultiplier
    {
        get { return pointsMultiplier; }
        set
        {
            pointsMultiplier = value;
        }
    }
}