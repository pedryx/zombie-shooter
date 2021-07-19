using System;


public delegate void HealthChangeEventHandler(object sender, HealthChangeEventArgs e);

public class HealthChangeEventArgs : EventArgs
{

    public int CurrentHealth { get; private set; }

    public HealthChangeEventArgs(int currentHealth)
    {
        CurrentHealth = currentHealth;
    }

}
