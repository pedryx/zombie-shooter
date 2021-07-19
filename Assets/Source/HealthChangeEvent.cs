public delegate void HealthChangeEventHandler(object sender, HealthChangeEventArgs e);

public class HealthChangeEventArgs
{

    public int CurrentHealth { get; private set; }

    public HealthChangeEventArgs(int currentHealth)
    {
        CurrentHealth = currentHealth;
    }

}
