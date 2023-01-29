using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashShadow : MonoBehaviour
{
    public static void CreateDashShadow(Vector3 position, GameObject spriteObject, Quaternion rotation, float howLongShouldShadowLast = 1)
    {
        GameObject objToSpawn = Instantiate(spriteObject, position, rotation);

        DestroyDashShadow DDSScript = objToSpawn.AddComponent<DestroyDashShadow>();
        SpriteRenderer spriteRenderer = objToSpawn.GetComponent<SpriteRenderer>();

        DDSScript.howLongShouldShadowLast = howLongShouldShadowLast;
        spriteRenderer.sortingOrder = -5;
        spriteRenderer.color = new Color(0.5f, 0.25f, 0.25f, howLongShouldShadowLast);
    }
}
