using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FollowOtherObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToFollow;
    [SerializeField] private Slider slider;

    [Range(0.0f, 1.0f)]
    public float distanceFromObject;

    void LateUpdate()
    {
        if (objectToFollow == null) { Destroy(gameObject); }
        transform.position = Vector3.MoveTowards(transform.position, objectToFollow.transform.position, distanceFromObject);
    }

    public Slider GetHealthSlider() => slider;
    public void SetObjectToFollow(GameObject newObject) { objectToFollow = newObject; }
}
