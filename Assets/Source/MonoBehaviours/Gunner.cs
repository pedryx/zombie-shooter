using UnityEngine;


/// <summary>
/// Adds firing capability to game object.
/// </summary>
class Gunner : MonoBehaviour
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
    /// Speed of bullets.
    /// </summary>
    public float Speed;

    private void Update()
    {
        ellapsed += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && ellapsed >= 1 / FireRate)
        {
            Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
            Vector2 direction = ((Vector2)Input.mousePosition - center).normalized;

            var ammoObject = Instantiate(AmmoPrefab);
            ammoObject.transform.position = (Vector2)transform.position + direction * .6f;

            ammoObject.GetComponent<Ammo>().DeltaMovement = direction * Speed;
            ellapsed = 0;
        }
    }

}
