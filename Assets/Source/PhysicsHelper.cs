using UnityEngine;


public static class PhysicsHelper
{

    /// <summary>
    /// Check if there are any walls between two circles.
    /// </summary>
    /// <param name="position1">Center position of circle1.</param>
    /// <param name="radius1">Radius of circle1.</param>
    /// <param name="position2">Center position of circle2.</param>
    /// <param name="radius2">Radius position of circle2.</param>
    /// <returns>True if there are no walls between the two circles, otherwise false.</returns>
    public static bool CirclesInSight(Vector2 position1, float radius1,
        Vector2 position2, float radius2)
    {
        Vector2[] sides1 = new Vector2[]
        {
            new Vector3(0, -radius1),
            new Vector3(0, +radius1),
            new Vector3(-radius1, 0),
            new Vector3(+radius1, 0),
        };
        Vector2[] sides2 = new Vector2[]
        {
            new Vector3(0, -radius2),
            new Vector3(0, +radius2),
            new Vector3(-radius2, 0),
            new Vector3(+radius2, 0),
        };

        bool inSightLine = true;
        for (int i = 0; i < sides1.Length; i++)
        {
            Vector2 sidePosition1 = position1 + sides1[i];
            Vector2 sidePosition2 = position2 + sides2[i];
            int mask = LayerMask.GetMask("Walls");

            RaycastHit2D hit = Physics2D.Linecast(sidePosition1, sidePosition2, mask);
            if (hit.collider != null)
            {
                inSightLine = false;
                break;
            }
        }

        return inSightLine;
    }

}
