using UnityEngine;


class Damager : MonoBehaviour
{

    public ObjectType TargetFilter;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hitable = collision.gameObject.GetComponent<Hitable>();

        if (hitable == null || hitable.Immune || (TargetFilter & hitable.Type) == 0)
            return;

        hitable.GetHit();
    }

}
