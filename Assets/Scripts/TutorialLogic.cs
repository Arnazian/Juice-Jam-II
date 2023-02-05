using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLogic : MonoBehaviour
{
    //[SerializeField] GameObject[] guidelines;
    [SerializeField] GameObject[] enemies1;
    [SerializeField] GameObject[] enemies2;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "KeyPoint2")
        {
            foreach (GameObject enemy in enemies1)
            {
                Debug.Log("enemy spawn");
                enemy.SetActive(true);
            }
        }

        if (other.gameObject.name == "KeyPoint3")
        {
            foreach (GameObject enemy in enemies2)
            {
                enemy.SetActive(true);
            }
        }
    }
}
