using System;


/// <summary>
/// Represent a handler for reward picked releated events.
/// </summary>
/// <param name="sender">Event sender.</param>
/// <param name="e">Event arguments.</param>
public delegate void RewardPickedEventHandler(object sender, RewardPickedEventArgs e);

/// <summary>
/// Represent an arguments for reward picked releated events.
/// </summary>
public class RewardPickedEventArgs : EventArgs
{

    /// <summary>
    /// New value of releated stat after reward was picked.
    /// </summary>
    public float NewValue { get; private set; }

    /// <summary>
    /// Increesed amount to the old valie.
    /// </summary>
    public float IncAmount { get; private set; }

    /// <summary>
    /// Create new arguments for reward picked releated events.
    /// </summary>
    /// <param name="newValue">New value of releated stat after reward was picked.</param>
    /// <param name="incAmount">Increesed amount to the old valie.</param>
    public RewardPickedEventArgs(float newValue, float incAmount)
    {
        NewValue = newValue;
    }

}
