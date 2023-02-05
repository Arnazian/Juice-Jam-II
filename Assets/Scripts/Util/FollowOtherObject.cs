using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FollowOtherObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToFollow;
    [SerializeField] private Slider h_Fill;
    [SerializeField] private Slider s_Fill;

    [Range(0.0f, 1.0f)]
    public float distanceFromObject; 

    void LateUpdate()
    {
        if (objectToFollow == null) { Destroy(gameObject); }
        else { transform.position = Vector3.MoveTowards(transform.position, objectToFollow.transform.position, distanceFromObject); }
        
    }

    public Slider GetHealthImageFill() => h_Fill;
    public Slider GetStaggerImageFill() => s_Fill;
    public void SetObjectToFollow(GameObject newObject) { objectToFollow = newObject; }
}
