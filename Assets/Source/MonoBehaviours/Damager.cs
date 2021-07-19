using UnityEngine;


class Damager : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hitable = collision.gameObject.GetComponent<Hitable>();
        if (hitable == null || hitable.Immune)
            return;

        hitable.GetHit();
    }

}
