using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Creature : MonoBehaviour, IProjectileInteractable
{
    [Header("Health settings")]
    public int MaxHealth;

    [Range(1, 100)]
    public int LowHealthPercentage = 20;

    [Header("Events")]
    public UnityEvent OnAlive = new UnityEvent();
    public UnityEvent OnLowHealth = new UnityEvent();
    public UnityEvent OnGoodHealth = new UnityEvent();
    public UnityEvent OnDamaged = new UnityEvent();
    public UnityEvent OnDeath = new UnityEvent();

    public int CurrentHealth { get; protected set; } = 1;
    public bool IsDead { get; protected set; }

    public bool IsOnLowHealth { get; protected set; }
    public bool IsOnGoodHealth { get; protected set; } = true;

    protected void Awake()
    {
        CurrentHealth = MaxHealth;
        OnAlive.Invoke();
    }

    protected void Update()
    {
        DealWithHealth();

        if (!IsDead)
        {
            Movement();
        }

        if (HealthPercentage() <= LowHealthPercentage)
        {
            if (IsOnLowHealth == false)
            {
                OnLowHealth.Invoke();
            }

            IsOnLowHealth = true;
            IsOnGoodHealth = false;
        }
        else
        {
            if (IsOnGoodHealth == false)
            {
                OnGoodHealth.Invoke();
            }

            IsOnLowHealth = false;
            IsOnGoodHealth = true;
        }
    }

    /// <summary>
    /// Code for moving the creature within the Game world
    /// </summary>
    /// <b>Authors</b>
    /// <br>Arad Bozorgmehr (Vrglab)</br>
    protected abstract void Movement();

    /// <summary>
    /// Take's damage based on the given damage value
    /// </summary>
    /// <param name="damage">The given damage value</param>
    /// <b>Authors</b>
    /// <br>Arad Bozorgmehr (Vrglab)</br>
    public virtual void TakeDamage(int damage)
    {
        CurrentHealth = (CurrentHealth - damage);
        OnDamaged.Invoke();
    }

    /// <summary>
    /// Heal's damage based on the given damage value
    /// </summary>
    /// <param name="heal">The given heal value</param>
    /// <b>Authors</b>
    /// <br>Arad Bozorgmehr (Vrglab)</br>
    public virtual void HealDamage(int heal)
    {
        if((CurrentHealth + heal) > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        else
        {
            CurrentHealth = (CurrentHealth + heal);
        }
    }

    /// <summary>
    /// Calculates how hurt the creature is and give's out a percentage based on that
    /// </summary>
    /// <returns>The calculated percentage</returns>
    /// <b>Authors</b>
    /// <br>Arad Bozorgmehr (Vrglab)</br>
    public virtual float HealthPercentage()
    {
        return (CurrentHealth / MaxHealth) * 100;
    }

    /// <summary>
    /// Deals with everything HP related
    /// </summary>
    /// <b>Authors</b>
    /// <br>Arad Bozorgmehr (Vrglab)</br>
    public virtual void DealWithHealth()
    {
        if (CurrentHealth <= 0)
        {
            if (!IsDead)
            {
                OnDeath.Invoke();
            }
            IsDead = true;
        }
        else
        {
            if (IsDead)
            {
                OnAlive.Invoke();
            }
            IsDead = false;
        }
    }

    public void OnProjectileHit(Projectiles hitProjectile)
    {
        TakeDamage(hitProjectile.DammageAmnt);
    }
}
