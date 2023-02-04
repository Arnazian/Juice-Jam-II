using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashShadow : MonoBehaviour
{
    public static void CreateDashShadow(Vector3 position, SpriteRenderer spriteObject, Quaternion rotation, float howLongShouldShadowLast = 1)
    {
        GameObject objToSpawn = new GameObject("DashShadow");

        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        SpriteRenderer spriteRenderer = objToSpawn.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteObject.sprite;

        DestroyDashShadow DDSScript = objToSpawn.AddComponent<DestroyDashShadow>();

        DDSScript.howLongShouldShadowLast = howLongShouldShadowLast;
        spriteRenderer.sortingOrder = 1;
        spriteRenderer.color = new Color(0.5f, 0.25f, 0.25f, howLongShouldShadowLast);
    }
}
