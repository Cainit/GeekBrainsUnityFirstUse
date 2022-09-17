using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    float healthMax;

    public EventHandler OnChange;
    public EventHandler OnDamage;

    void Awake()
    {
        healthMax = health;
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health < 0)
            health = 0;

        FloatTextManager.Instance.ShowDamage(transform.position + new Vector3(0,1,0), damage);

        OnChange?.Invoke(this, EventArgs.Empty);
        OnDamage?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(float heal)
    {
        health += heal;
        if (health > healthMax)
            health = healthMax;

        OnChange?.Invoke(this, EventArgs.Empty);
    }

    public bool IsLive()
    {
        return health > 0;
    }

    public bool IsNeedHeal()
    {
        return health < healthMax;
    }

    public float GetInPercent()
    {
        return health / healthMax;
    }
}
