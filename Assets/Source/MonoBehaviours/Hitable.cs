using System.Collections;
using System.Collections.Generic;

using UnityEngine;


/// <summary>
/// Represent a lifes for hitable target.
/// </summary>
public class Hitable : MonoBehaviour
{

    /// <summary>
    /// Number of ellapsed seconds from last hit.
    /// </summary>
    private float ellapsed;

    /// <summary>
    /// Maximum amount of health.
    /// </summary>
    public int MaxHealth;
    /// <summary>
    /// Number of seconds for which is target immune to damage after geting hit.
    /// </summary>
    public float ProtectionCooldown;

    /// <summary>
    /// Current amount of health.
    /// </summary>
    public int CurrentHealth { get; private set; }

    /// <summary>
    /// Determine if player is immune to damage.
    /// </summary>
    public bool Immune { get; private set; }

    /// <summary>
    /// Occur when current amount of health change.
    /// </summary>
    public event HealthChangeEventHandler OnHealthChange;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        OnHealthChange?.Invoke(this, new HealthChangeEventArgs(CurrentHealth));
        //StartCoroutine(nameof(UpdateHealthCorutine));
    }

    private IEnumerable UpdateHealthCorutine()
    {
        yield return new WaitForEndOfFrame();

        OnHealthChange?.Invoke(this, new HealthChangeEventArgs(CurrentHealth));
    }

    private void Update()
    {
        if (Immune)
        {
            ellapsed += Time.deltaTime;
            if (ellapsed >= ProtectionCooldown)
                Immune = false;
        }
    }

    public void GetHit()
    {
        if (Immune)
            return;

        CurrentHealth--;
        OnHealthChange?.Invoke(this, new HealthChangeEventArgs(CurrentHealth));

        if (CurrentHealth == 0)
        {
            //todo: game exit
            //UnityEngine.Application.Quit();
        }
        else
        {
            Immune = true;
            ellapsed = 0;
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
        OnHealthChange?.Invoke(this, new HealthChangeEventArgs(CurrentHealth));
    }

}
