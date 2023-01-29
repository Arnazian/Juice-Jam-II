using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private GameObject player;

    public bool startShake = false;
    private float shakeStrengthModifier = 0.1f;
    private float duration = 0.1f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (startShake)
        {
            StartCoroutine(Shaking(shakeStrengthModifier));
        }
    }

    public void DoScreenShake(float newDuration, float newShakeStrengthModifier)
    {
        duration = newDuration;
        shakeStrengthModifier = newShakeStrengthModifier;
        startShake = true;
    }

    IEnumerator Shaking(float strengthModifier)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            startShake = false;

            elapsedTime += Time.deltaTime;

            transform.position += Random.insideUnitSphere * 0.1f * strengthModifier;
            yield return null;
        }
    }
}
