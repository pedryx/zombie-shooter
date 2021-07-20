using UnityEngine;


/// <summary>
/// Adds firing capability to game object.
/// </summary>
public class Gunner : MonoBehaviour
{

    /// <summary>
    /// Number of ellapsed seconds from last fire.
    /// </summary>
    private float ellapsed;

    /// <summary>
    /// Maximum number of fires per second.
    /// </summary>
    public float FireRate;
    /// <summary>
    /// Prefab for bullets.
    /// </summary>
    public GameObject AmmoPrefab;
    /// <summary>
    /// (Optional parameter)
    /// If not null then speed of fired bullets will be inceesing each time player pick up a move speed upgrade.
    /// </summary>
    public RewardPicker rewardPicker;
    /// <summary>
    /// Speed of bullets.
    /// </summary>
    public float Speed;

    private void Start()
    {
        if (rewardPicker != null)
            rewardPicker.OnSpeedPicked += RewardPicker_OnSpeedPicked;
    }

    private void Update()
    {
        ellapsed += Time.deltaTime;

        if (Input.GetMouseButton(0) && ellapsed >= 1 / FireRate)
        {
            Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
            Vector2 direction = ((Vector2)Input.mousePosition - center).normalized;

            var ammoObject = Instantiate(AmmoPrefab);
            ammoObject.transform.position = transform.position;

            ammoObject.GetComponent<Ammo>().DeltaMovement = direction * Speed;
            ellapsed = 0;

            Physics2D.IgnoreCollision(
                ammoObject.GetComponent<Collider2D>(),
                gameObject.GetComponent<Collider2D>()
            );
        }
    }
    private void RewardPicker_OnSpeedPicked(object sender, RewardPickedEventArgs e)
    {
        Speed += e.IncAmount;
    }

}
