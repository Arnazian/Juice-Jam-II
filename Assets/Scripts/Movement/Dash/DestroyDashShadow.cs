using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDashShadow : MonoBehaviour
{
    [HideInInspector] public float howLongShouldShadowLast = 1f;
    private float timeLeft;


    void Start()
    {
        timeLeft = howLongShouldShadowLast;
    }

    void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;

        GetComponent<SpriteRenderer>().color = new Color(0.25f, 0.25f, 0.25f, timeLeft);

        if (timeLeft <= 0)
        {
            Destroy(gameObject);
        }
    }
}
