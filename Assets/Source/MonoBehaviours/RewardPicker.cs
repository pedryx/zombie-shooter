using UnityEngine;


/// <summary>
/// Represent a UI handler for reward picker.
/// </summary>
public class RewardPicker : MonoBehaviour
{

    /// <summary>
    /// Amouns of speed that would be added to player
    /// if he pick speed reward.
    /// </summary>
    private const float SpeedInc = 1.5f;
    /// <summary>
    /// Amount of fire rate that would be added to player
    /// if he pick fire rate reward.
    /// </summary>
    private const float FireRateInc = 1.0f;
    /// <summary>
    /// Amount of health that would be added to player
    /// if he pick heal reward,
    /// </summary>
    private const int HealInc = 1;

    public GameObject Player;

    /// <summary>
    /// Occur after player picked speed reward.
    /// </summary>
    public event RewardPickedEventHandler OnSpeedPicked;
    /// <summary>
    /// Occur after player picked fire rate reward.
    /// </summary>
    public event RewardPickedEventHandler OnFireRatePicked;
    /// <summary>
    /// Occur after player picked heal reward.
    /// </summary>
    public event RewardPickedEventHandler OnHealPicked;

    public void PickSpeed()
    {
        Player.GetComponent<PlayerMovement>().Speed += SpeedInc;
        HideReward();
        OnSpeedPicked?.Invoke(this, new RewardPickedEventArgs(
            Player.GetComponent<PlayerMovement>().Speed, SpeedInc));
    }

    public void PickFireRate()
    {
        Player.GetComponent<Gunner>().FireRate += FireRateInc;
        HideReward();
        OnFireRatePicked?.Invoke(this, new RewardPickedEventArgs(
            Player.GetComponent<Gunner>().FireRate, FireRateInc));
    }

    public void PickHeal()
    {
        Player.GetComponent<Hitable>().Heal(HealInc);
        HideReward();
        OnHealPicked?.Invoke(this, new RewardPickedEventArgs(
            Player.GetComponent<Hitable>().CurrentHealth, HealInc));
    }

    private void HideReward()
    {
        Player.GetComponent<Gunner>().enabled = true;
        gameObject.SetActive(false);
    }

}
