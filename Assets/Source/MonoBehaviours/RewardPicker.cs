using UnityEngine;


public class RewardPicker : MonoBehaviour
{

    private const float SpeedInc = 1.5f;
    private const float FireRateInc = 1.0f;
    private const int HealInc = 1;

    public GameObject Player;

    public void PickSpeed()
    {
        Player.GetComponent<PlayerMovement>().speed += SpeedInc;
        HideReward();
    }

    public void PickFireRate()
    {
        Player.GetComponent<Gunner>().FireRate += FireRateInc;
        HideReward();
    }

    public void PickHeal()
    {
        Player.GetComponent<Hitable>().Heal(HealInc);
        HideReward();
    }

    private void HideReward()
    {
        Player.GetComponent<Gunner>().enabled = true;
        gameObject.SetActive(false);
    }

}
