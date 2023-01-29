using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashShadow : MonoBehaviour
{
    static public void CreateDashShadow(Vector2 startPosition, Vector2 endPosition, GameObject spriteGamoObject, Quaternion rotation = default(Quaternion))
    {
        GameObject dashSprite = spriteGamoObject;

        dashSprite.GetComponent<SpriteRenderer>().sortingOrder = -5;
        dashSprite.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 50);

        Instantiate(dashSprite, startPosition, rotation);
    }
}
